using System;
using System.Collections.Generic;

using System.Windows.Forms;
using static YoloDetection.Marker.RectController;
namespace YoloDetection.Marker
{
    public interface IRectController
    {
        bool CreatingFrameObjec { get; set; }
        List<IControlRect> ControlRects { get; set; }
        event Action<IFrameObject> OnNewFrameObject;
        void DrawAll();
        void Draw(IFrameObject frameObject);
        void MouseLeftDown(object sender, MouseEventArgs e);
        void MouseLeftUp(object sender, MouseEventArgs e);
        void MouseWheel(object sender, MouseEventArgs e);
        void Move(object sender, MouseEventArgs e);
        void SetFrameObjectList(List<IFrameObject> list);
    }
    public class RectController : IRectController
    {
        public event Action<IFrameObject> OnNewFrameObject;
        public List<IControlRect> ControlRects { get; set; } = new List<IControlRect>();
        public bool CreatingFrameObjec { get; set; }
        private IFrameObejctContainer FrameObejctContainer { get; set; } = new FrameObejctContainer();
        private PointF startPoint = new PointF();
        private PointF endPoint = new PointF();
        private bool Drawing = false;
        private bool MovingSelectedFrameObject = false;
        private IViewBoxControler ViewBoxController { get; set; }
        public RectController(IViewBoxControler viewBoxController) : this(viewBoxController, new List<IFrameObject>()) { }
        public RectController(IViewBoxControler viewBoxController, List<IFrameObject> frameObjectList)
        {
            ViewBoxController = viewBoxController;
            SetFrameObjectList(frameObjectList);
        }
        public void MouseLeftDown(object sender, MouseEventArgs e)
        {
            Point newPoint = e.Location.Divide(ViewBoxController.ImageScale);
            startPoint = ConverPointToPointF(newPoint);
            if (CreatingFrameObjec)
            {
                Drawing = true;
                return;
            }
            IFrameObject fo = GetFrameObjectByPointF(startPoint);
            FrameObejctContainer.SetSelectedObject(fo, true);
            if (fo == null)
            {
                ViewBoxController.StartMoving(e.Location);
            } else
            {
                MovingSelectedFrameObject = true;
            }
        }
        public void MouseLeftUp(object sender, MouseEventArgs e)
        {
            if (MovingSelectedFrameObject)
            {
                MovingSelectedFrameObject = false;
                DrawAll();
            }
            if (CreatingFrameObjec)
            {
                Drawing = false;
                endPoint = ConverPointToPointF(e.Location.Divide(ViewBoxController.ImageScale));
                IFrameObject frameObject = new FrameObject(0, "", new RectNormalized(startPoint, endPoint), ConverSizeToSizeF(new Size(6,6)));
                OnNewFrameObject?.Invoke(frameObject);
                DrawAll();
            }
        }
        public void Move(object sender, MouseEventArgs e)
        {
            if (ViewBoxController.Moving) return;
            endPoint = ConverPointToPointF(e.Location.Divide(ViewBoxController.ImageScale));
            if (Drawing || MovingSelectedFrameObject)
            {
                if (MovingSelectedFrameObject)
                {
                    if (FrameObejctContainer.Selected != null)
                    {
                        IFrameObject sfo = FrameObejctContainer.Selected;
                        PointF delta = new PointF(endPoint.X - startPoint.X, endPoint.Y - startPoint.Y);
                        sfo.Move(delta);
                        startPoint = endPoint;
                        Draw(sfo);
                    }
                }
                DrawAll();
            } else
            {
                IControlRect cr = GetControlRect(endPoint);
                if (cr != null)
                {
                    Console.WriteLine(cr.FrameObject.GetPointType(endPoint));
                }
            }
        }
        public void MouseWheel(object sender, MouseEventArgs e)
        {
            const float scale_per_delta = 0.05F;
            float direct = e.Delta >= 0 ? 1 : -1;
            float deltaScale = ViewBoxController.ImageScale;
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
            using (Pen pen = new Pen(Color.Red, 2))
            using (SolidBrush brushRect = new SolidBrush(Color.FromArgb(100, Color.Red)))
            using (SolidBrush brushElipse = new SolidBrush(Color.FromArgb(200, Color.White)))
            using (SolidBrush brushControls = new SolidBrush(Color.FromArgb(200, Color.Black)))
            using (Graphics G = Graphics.FromImage(ViewBoxController.Image))
            {
                Point leftTop = ConverPointFToPoint(frameObject.Rect.LeftTop);
                Point rightBottom = ConverPointFToPoint(frameObject.Rect.RightBottom);
                Point leftBotton = ConverPointFToPoint(frameObject.Rect.LeftBottom);
                Point rightTop = ConverPointFToPoint(frameObject.Rect.RightTop);
                Rectangle rect = new Rectangle().FromPoints(leftTop, rightBottom);
                G.DrawRectangle(pen, rect);
                G.FillRectangle(brushRect, rect);
                if (!MovingSelectedFrameObject)
                {
                    foreach (var elm in frameObject.ControlRects)
                    {
                        G.FillRectangle(brushControls, elm.Value.Rect.UnNormalize(ViewBoxController.ImageSize));
                    }
                }
            }
        }
        public void SetFrameObjectList(List<IFrameObject> list)
        {
            FrameObejctContainer.Set(list);
        }
        private SizeF ConverSizeToSizeF(Size size)
        {
            SizeF tmp = new SizeF();
            tmp.Width = size.Width > 0 ? (size.Width / (float)ViewBoxController.ImageSize.Width) : 0;
            tmp.Height = size.Height > 0 ? (size.Height / (float)ViewBoxController.ImageSize.Height) : 0;
            return tmp;
        }
        private PointF ConverPointToPointF(Point point)
        {
            PointF tmp = new PointF();

            tmp.X = point.X>0? (point.X / (double)ViewBoxController.ImageSize.Width): 0;
            tmp.Y = point.Y>0? (point.Y / (double)ViewBoxController.ImageSize.Height): 0;
            return tmp;
        }
        private IFrameObject GetFrameObjectByPointF(PointF point)
        {
            return FrameObejctContainer.FrameObjectList.Find(fo => fo.Rect.Contains(point));
        }
        private Point ConverPointFToPoint(PointF vector)
        {
            Point tmp = new Point();
            tmp.X = (int)(vector.X * ViewBoxController.ImageSize.Width);
            tmp.Y = (int)(vector.Y * ViewBoxController.ImageSize.Height);
            return tmp;
        }
        private IControlRect GetControlRect(PointF pos)
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
    }
}
