using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static YoloDetection.Marker.RectController;

namespace YoloDetection.Marker
{
    public interface IRectController
    {
        PictureBox PictureBox { get; set; }
        event FrameObjectEvent OnNewFrameObject;
        void Draw();
        void Draw(IFrameObject frameObject);
        void SetFrameObjectList(List<IFrameObject> list);
    }
    public class RectController : IRectController
    {
        public delegate void FrameObjectEvent(IFrameObject frameObject);
        public event FrameObjectEvent OnNewFrameObject;
        public PictureBox PictureBox
        {
            get
            {
                return ViewBoxController.PictureBox;
            }
            set
            {
                ViewBoxController.PictureBox = value;
                ViewBoxController.PictureBox.MouseDown += MouseDown;
                ViewBoxController.PictureBox.MouseUp += MouseUp;
                ViewBoxController.PictureBox.MouseMove += Move;
                ViewBoxController.PictureBox.Resize += Resize;
                ViewBoxController.PictureBox.MouseWheel += MouseWheel;
                
                imageSize = ViewBoxController.PictureBox.Size;
            }
        }
        private float ImageScale = 1;
        private List<IFrameObject> FrameObjectList { get; set; }
        private Image originImage;
        private Size imageSize = new Size();
        private PointF startPoint = new PointF();
        private PointF endPoint = new PointF();
        private bool Drawing = false;
        private IViewBoxControler ViewBoxController { get; set; }
        public RectController(IViewBoxControler viewBoxController) : this(viewBoxController, new List<IFrameObject>()) { }
        public RectController(IViewBoxControler viewBoxController, List<IFrameObject> frameObjectList)
        {
            ViewBoxController = viewBoxController;
            ViewBoxController.OnChangeImage += (Image image) =>
            {
                originImage = (Image)image?.Clone();
            };
            PictureBox = ViewBoxController.PictureBox;
            SetFrameObjectList(frameObjectList);
        }
        private void MouseDown(object sender, MouseEventArgs e)
        {
           if (e.Button == MouseButtons.Left) MouseLeftDown(sender, e);
        }
        private void MouseLeftDown(object sender, MouseEventArgs e)
        {
            Clear();
            startPoint = ConverPointToPointF(e.Location);
            Drawing = true;
        }
        private void Clear()
        {
            PictureBox.Image = (Image)originImage?.Clone();
        }
        private void MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) MouseLeftUp(sender, e);
        }
        private void MouseLeftUp(object sender, MouseEventArgs e)
        {
            Drawing = false;
            endPoint = ConverPointToPointF(e.Location);
            IFrameObject frameObject = new FrameObject();
            frameObject.Rect = new RectNormalized(startPoint, endPoint, new List<IControlPoint>());
            OnNewFrameObject?.Invoke(frameObject);
            Draw();
        }
        private void Move(object sender, MouseEventArgs e)
        {
            endPoint = ConverPointToPointF(e.Location);
            if (Drawing)
            {
                Draw();
            }
        }
        private void MouseWheel(object sender, MouseEventArgs e)
        {

            // The amount by which we adjust scale per wheel click.
            const float scale_per_delta = 0.02F;
            float direct = e.Delta >= 0 ? 1 : -1;
            // Update the drawing based upon the mouse wheel scrolling.
            ImageScale += scale_per_delta * direct;
            if (ImageScale < 0) ImageScale = 0;

            // Size the image.
            PictureBox.Size = new Size(
                (int)(PictureBox.Image.Size.Width * ImageScale),
                (int)(PictureBox.Image.Size.Height * ImageScale));

            /*if (e.Delta > 0 ) 
                PictureBox.Size = new Size((int)(PictureBox.Size.Width + (10 * coef)), PictureBox.Size.Height + 10);
            if (e.Delta < 0 && PictureBox.Size.Width > 200 && PictureBox.Size.Height > 200) 
                PictureBox.Size = new Size((int)(PictureBox.Size.Width - (10 * coef)), PictureBox.Size.Height - 10);*/

        }
        private void Resize(object sender, EventArgs e)
        {
            imageSize = PictureBox.Size;
            if (Drawing) Draw();
        }
        public void Draw()
        {
            if (PictureBox.Image == null) return;
            Clear();
            if (Drawing)
            {
                FrameObject obj = new FrameObject();
                obj.Rect = new RectNormalized() { Start = startPoint, End = endPoint };
                Draw(obj);
            } else
            {
                foreach (IFrameObject fo in FrameObjectList) Draw(fo);
            }
        }
        public void Draw(IFrameObject frameObject)
        {
            using (Pen pen = new Pen(Color.Red, 1))
            using (SolidBrush brushRect = new SolidBrush(Color.FromArgb(100, Color.Red)))
            using (SolidBrush brushElipse = new SolidBrush(Color.FromArgb(200, Color.White)))
            using (Graphics G = Graphics.FromImage(PictureBox.Image))
            {
                Point leftTop = ConverPointFToPoint(frameObject.Rect.LeftTop);
                Point rightBottom = ConverPointFToPoint(frameObject.Rect.RightBottom);
                Point leftBotton = ConverPointFToPoint(frameObject.Rect.LeftBottom);
                Point rightTop = ConverPointFToPoint(frameObject.Rect.RightTop);
                Rectangle rect = new Rectangle().FromPoints(leftTop, rightBottom);
                rect = GetStrictedRectangle(rect);
                G.DrawRectangle(pen, rect);
                G.FillRectangle(brushRect, rect);
                int radius = 6;
                G.FillCircle(brushElipse, leftTop, radius);
                G.FillCircle(brushElipse, rightBottom, radius);
                G.FillCircle(brushElipse, leftBotton, radius);
                G.FillCircle(brushElipse, rightTop, radius);
                PictureBox.Refresh();
            }
        }
        private PointF ConverPointToPointF(Point point)
        {
            PointF tmp = new PointF();
            tmp.X = point.X>0? (float)point.X / (float)imageSize.Width: 0;
            tmp.Y = point.Y>0? (float)point.Y / (float)imageSize.Height: 0;
            return tmp;
        }
        private Point ConverPointFToPoint(PointF vector)
        {
            Point tmp = new Point();
            tmp.X = (int)(vector.X * imageSize.Width);
            tmp.Y = (int)(vector.Y * imageSize.Height);
            return tmp;
        }
        private Rectangle GetStrictedRectangle(Rectangle rect)
        {
            if ((rect.X + rect.Width) >= imageSize.Width) rect.Width = imageSize.Width - rect.X;
            if ((rect.Y + rect.Height) >= imageSize.Height) rect.Height = imageSize.Height - rect.Y;
            return rect;
        }
        public void SetFrameObjectList(List<IFrameObject> list)
        {
            FrameObjectList = list;
        }
    }
}
