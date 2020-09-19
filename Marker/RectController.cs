using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoloDetection.Marker
{
    
    interface IRectController
    {
        
        PictureBox PictureBox { get; set; }

        void Draw(IFrameObject frameObject);
        void SetFrameObjectList(List<IFrameObject> list);
    }
    class RectController : IRectController
    {
        
        private PictureBox _pictureBox { get; set; }
        private List<IFrameObject> FrameObjectList { get; set; }
        private Image originImage;
        private Size imageSize = new Size();
        private Vector2 startPoint = new Vector2();
        private Vector2 endPoint = new Vector2();
        private bool Drawing = false;
        public PictureBox PictureBox { 
            get {
                return _pictureBox;
            }
            set {
                _pictureBox = value;
                _pictureBox.MouseDown += MouseDown;
                _pictureBox.MouseUp +=  MouseUp;
                _pictureBox.MouseMove +=  Move;
                _pictureBox.Resize += Resize;
                _pictureBox.MouseWheel += Resize;
                imageSize = _pictureBox.Size;
            } }
        public RectController(PictureBox pictureBox) : this(pictureBox, new List<IFrameObject>()) { }
        public RectController(PictureBox pictureBox, List<IFrameObject> frameObjectList)
        {
            PictureBox = pictureBox;
            SetFrameObjectList(frameObjectList);
        }
        private void MouseDown(object sender, MouseEventArgs e)
        {
            startPoint = ConverPointToVector2(e.Location);
            if (originImage != null)
            {
                PictureBox.Image = (Image)originImage.Clone();
            } else
            {
                originImage = (Image)PictureBox.Image.Clone();
            }
            Drawing = true;
        }
        private void MouseUp(object sender, MouseEventArgs e)
        {
            Drawing = false;
            endPoint = ConverPointToVector2(e.Location);
            Draw();
        }
        private void Move(object sender, MouseEventArgs e)
        {
            if (Drawing)
            {
                endPoint = ConverPointToVector2(e.Location);
                Draw();
            }
        }
        private void MouseWheel(object sender, MouseEventArgs e)
        {
            
        }
        private void Resize(object sender, EventArgs e)
        {
            
            imageSize = PictureBox.ClientSize;
            
            if (Drawing) Draw();
        }
        private void Draw()
        {
            if (PictureBox.Image == null) return;
            if (originImage != null) PictureBox.Image = (Image)originImage.Clone();
            foreach(IFrameObject fo in FrameObjectList)
            {
                Draw(fo);
            }
            FrameObject obj = new FrameObject();
            obj.Rect = new RectNormalized() { Start = startPoint, End = endPoint };
            Draw(obj);
            
        }
        public void Draw(IFrameObject frameObject)
        {
            using (Pen pen = new Pen(Color.Red, 2))
            using (Graphics G = Graphics.FromImage(PictureBox.Image))
            {
                Point start = ConverVector2ToPoint(frameObject.Rect.Start);
                Point end = ConverVector2ToPoint(frameObject.Rect.End);
                Rectangle rect = GetRectangleFromPoints(start, end);
                rect = GetStrictedRectangle(rect);
                G.DrawRectangle(pen, rect);
                PictureBox.Refresh();
            }
        }
        private Vector2 ConverPointToVector2(Point point)
        {
            Vector2 tmp = new Vector2();
            tmp.X = point.X>0? (float)point.X / (float)imageSize.Width: 0;
            tmp.Y = point.Y>0? (float)point.Y / (float)imageSize.Height: 0;
            return tmp;
        }
        private Point ConverVector2ToPoint(Vector2 vector)
        {
            Point tmp = new Point();
            tmp.X = (int)(vector.X * imageSize.Width);
            tmp.Y = (int)(vector.Y * imageSize.Height);
            return tmp;
        }
        private Rectangle GetStrictedRectangle(Rectangle rect)
        {
            if ((rect.X + rect.Width) >= imageSize.Width)
            {
                rect.Width = imageSize.Width - rect.X;
            }
            if ((rect.Y + rect.Height) >= imageSize.Height)
            {
                rect.Height = imageSize.Height - rect.Y;
            }
            return rect;
        }
        private Rectangle GetRectangleFromRectNormalized(RectNormalized rect) => GetRectangleFromPoints(ConverVector2ToPoint(rect.Start), ConverVector2ToPoint(rect.End));
        private Rectangle GetRectangleFromPoints(Point start, Point end)
        {
            int X = start.X, 
                Y = start.Y,
                Width = Math.Abs(start.X-end.X),
                Height = Math.Abs(start.Y-end.Y);

            if (start.X > end.X) X = end.X;
            if (start.Y > end.Y) Y = end.Y;
            Rectangle rect = new Rectangle(X,Y,Width,Height);
            return rect;
        }

        public void SetFrameObjectList(List<IFrameObject> list)
        {
            FrameObjectList = list;
        }
    }
}
