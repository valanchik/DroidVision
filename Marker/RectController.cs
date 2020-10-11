using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using YoloDetection.Marker.Interfaces;
namespace YoloDetection.Marker
{
    public class RectController : IRectController
    {
        public event Action<IFrameObject> OnNewFrameObject;
        public List<IControlRect> ControlRects { get; set; } = new List<IControlRect>();
        public IControlRect SelectedControlRect { get; set; }
        public bool CreatingFrameObjec { get; set; }
        private IFrameObejctContainer FrameObejctContainer { get; set; } = new FrameObejctContainer();
        private Point<double> startPoint = new Point<double>();
        private Point<double> endPoint = new Point<double>();
        private bool Drawing = false;
        private bool MovingSelectedFrameObject = false;
        private bool EditSelectedFrameObject = false;
        private IViewBoxControler ViewBoxController { get; set; }
        public RectController(IViewBoxControler viewBoxController) : this(viewBoxController, new List<IFrameObject>()) { }
        public RectController(IViewBoxControler viewBoxController, List<IFrameObject> frameObjectList)
        {
            ViewBoxController = viewBoxController;
            SetFrameObjectList(frameObjectList);
        }
        private int BorderSize = 1;
        public void MouseLeftDown(object sender, MouseEventArgs e)
        {
            Point newPoint = e.Location.Divide(ViewBoxController.ImageScale);
            startPoint = ConverPointToPointF(newPoint);
            if (CreatingFrameObjec)
            {
                Drawing = true;
                return;
            }

            if (SelectedControlRect != null)
            {
                EditSelectedFrameObject = true;
                return;
            }
            IFrameObject fo = GetFrameObjectByPointF(startPoint);
            FrameObejctContainer.SetSelectedObject(fo, true);
            if (fo != null)
            {
                MovingSelectedFrameObject = true;
            } else
            {
                ViewBoxController.StartMoving(e.Location);
            }
        }
        public void MouseLeftUp(object sender, MouseEventArgs e)
        {
            if (MovingSelectedFrameObject)
            {
                MovingSelectedFrameObject = false;
                DrawAll();
            }
            if (EditSelectedFrameObject)
            {
                EditSelectedFrameObject = false;
                DrawAll();
            }
            if (CreatingFrameObjec)
            {
                Drawing = false;
                endPoint = ConverPointToPointF(e.Location.Divide(ViewBoxController.ImageScale));
                IFrameObject frameObject = new FrameObject(0, "", new RectNormalized(startPoint, endPoint), ConverSizeToSizeF(new Size(6,6)));
                CorrectFrameObjectByBorder(frameObject);
                OnNewFrameObject?.Invoke(frameObject);
                DrawAll();
            }
        }
        public void Move(object sender, MouseEventArgs e)
        {
            if (ViewBoxController.Moving) return;
            endPoint = ConverPointToPointF(e.Location.Divide(ViewBoxController.ImageScale));
            if (Drawing || MovingSelectedFrameObject || EditSelectedFrameObject)
            {
                if (MovingSelectedFrameObject)
                {
                    if (FrameObejctContainer.Selected != null)
                    {
                        IFrameObject sfo = FrameObejctContainer.Selected;
                        Point<double> delta = endPoint - startPoint;
                        sfo.Move(delta);
                        startPoint = endPoint;
                        Draw(sfo);
                    }
                }
                if (EditSelectedFrameObject)
                {
                    IFrameObject sfo = SelectedControlRect.FrameObject;
                    Point<double> delta = endPoint - startPoint;
                    sfo.Edit(SelectedControlRect.Type, delta);
                    startPoint = endPoint;
                    Draw(sfo);
                }
                DrawAll();
            } else
            {
                IControlRect cr = GetControlRect(endPoint);
                if (cr != null)
                {
                    SetSeletedControlRects(cr, true);
                } else if (SelectedControlRect != null)
                {
                    SetSeletedControlRects(cr, false);
                }
            }
        }
        public void MouseWheel(object sender, MouseEventArgs e)
        {
            const float scale_per_delta = 0.05F;
            float direct = e.Delta >= 0 ? 1 : -1;
            double deltaScale = ViewBoxController.ImageScale;
            ViewBoxController.ImageScale += scale_per_delta * direct;
            deltaScale = ViewBoxController.ImageScale - deltaScale;
            if (ViewBoxController.ImageScale < 0) ViewBoxController.ImageScale = 0;
            Point nMP = new Point((int)(ViewBoxController.MousePosition.X * deltaScale), (int)(ViewBoxController.MousePosition.Y * deltaScale));
            Rectangle r = new Rectangle(ViewBoxController.MousePosition,
                new Size((int)((nMP.X - ViewBoxController.MousePosition.X)), (int)((nMP.Y - ViewBoxController.MousePosition.Y))));
            Size delta = new Size(nMP.X, nMP.Y);
            Console.WriteLine(ViewBoxController.MousePosition);
            Console.WriteLine(delta);
            ViewBoxController.Resize();
        }
        public void DrawAll()
        {
            if (ViewBoxController.ImageNotExists) return;
            ViewBoxController.Clear();
            if (CreatingFrameObjec)
            {
                FrameObject obj = new FrameObject();
                obj.Rect = new RectNormalized() { Start = startPoint, End = endPoint };
                Draw(obj);
            } else if (MovingSelectedFrameObject)
            {
                if (FrameObejctContainer.Selected != null)
                {
                    Draw(FrameObejctContainer.Selected);
                }
            }
            else
            {
                ControlRects.Clear();
                foreach (IFrameObject fo in FrameObejctContainer.FrameObjectList) {
                    ControlRects.AddRange(fo.GetControlRects());
                    Draw(fo);
                };
            }
            ViewBoxController.Refresh();
        }
        public void Draw(IFrameObject frameObject)
        {
            using (Pen pen = new Pen(Color.Red, BorderSize))
            using (SolidBrush brushRect = new SolidBrush(Color.FromArgb(100, Color.Red)))
            using (SolidBrush brushElipse = new SolidBrush(Color.FromArgb(200, Color.White)))
            using (SolidBrush brushControls = new SolidBrush(Color.FromArgb(200, frameObject.PointColor)))
            using (SolidBrush brushSelectedControls = new SolidBrush(Color.FromArgb(200, frameObject.SelectedPointColor)))
            using (Graphics G = Graphics.FromImage(ViewBoxController.Image))
            {
                Point leftTop = ConverPointFToPoint(frameObject.Rect.LeftTop);
                Point rightBottom = ConverPointFToPoint(frameObject.Rect.RightBottom);
                Rectangle rect = new Rectangle().FromPoints(leftTop, rightBottom);
                G.DrawRectangle(pen, rect);
                G.FillRectangle(brushRect, rect);
                if (!MovingSelectedFrameObject)
                {
                    foreach (KeyValuePair< RectNormalizesPointType, IControlRect > val in frameObject.ControlRects)
                    {
                        if (val.Value.Selected)
                        {
                            G.FillRectangle(brushSelectedControls, val.Value.Rect.Rectangle.UnNormalize(ViewBoxController.ImageSize));
                        } else
                        {
                            G.FillRectangle(brushControls, val.Value.Rect.Rectangle.UnNormalize(ViewBoxController.ImageSize));
                        }
                    }
                }
            }
        }
        public void SetFrameObjectList(List<IFrameObject> list)
        {
            FrameObejctContainer.Set(list);
        }
        private Size<double> ConverSizeToSizeF(Size size)
        {
            Size<double> tmp = new Size<double>();
            tmp.Width = size.Width > 0 ? (size.Width / (double)ViewBoxController.ImageSize.Width) : 0;
            tmp.Height = size.Height > 0 ? (size.Height / (double)ViewBoxController.ImageSize.Height) : 0;
            return tmp;
        }
        private Point<double> ConverPointToPointF(Point point)
        {
            Point<double> tmp = new Point<double>();
            tmp.X = point.X>0? (point.X / (double)ViewBoxController.ImageSize.Width): 0;
            tmp.Y = point.Y>0? (point.Y / (double)ViewBoxController.ImageSize.Height): 0;
            return tmp;
        }
        private IFrameObject GetFrameObjectByPointF(Point<double> point)
        {
            return FrameObejctContainer.FrameObjectList.Find(fo => fo.Rect.Contains(point));
        }
        private Point ConverPointFToPoint(Point<double> point)
        {
            Point tmp = new Point();
            tmp.X = (int)(point.X * ViewBoxController.ImageSize.Width);
            tmp.Y = (int)(point.Y * ViewBoxController.ImageSize.Height);
            return tmp;
        }
        private IControlRect GetControlRect(Point<double> pos)
        {
            foreach (var elm in ControlRects)
            {
                if (elm.Contains(pos))
                {
                    return elm;
                }
            }
            return null;
        }
        private void CorrectFrameObjectByBorder(IFrameObject frameObejct)
        {
            Size<double> minSize = 1F/new Size<double>(ViewBoxController.ImageSize.Width, ViewBoxController.ImageSize.Height);
            Point<double> offset = new Point<double>(minSize)*(BorderSize>1?BorderSize-1:1);
            frameObejct.ControlRects[RectNormalizesPointType.RightBottomPoint].Move(offset);
            //frameObejct.ControlRects[RectNormalizesPointType.LeftTopPoint].Move(new Point<double>(offset.X*-1, offset.Y * -1));
            frameObejct.ControlRects[RectNormalizesPointType.RightTopPoint].Move(new Point<double>(offset.X, offset.Y*-1));
            frameObejct.ControlRects[RectNormalizesPointType.LeftBottomPoint].Move(new Point<double>(0, offset.Y));
        }
        private void SetSeletedControlRects(IControlRect rect, bool selected)
        {
            if (rect != null)
            {
                rect.Selected = selected;
                SelectedControlRect = rect;
                ControlRects.FindAll(fo =>
                {
                    if (fo.Id != rect.Id)
                    {
                        fo.Selected = false;
                    };
                    return false;
                });
            } else
            {
                ControlRects.FindAll(fo => fo.Selected = false);
                SelectedControlRect = null;
            }
            DrawAll();
        }
    }
}
