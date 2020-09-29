using System.Drawing;
using System.Windows.Forms;

namespace YoloDetection.Marker
{
    public interface IViewBoxControler: IMediatorSetter
    {
        bool ImageNotExists { get; }
        Size Size { get; set; }
        float ImageScale { get; set; }
        PictureBox PictureBox { get; set; }
        IRectController RectController { get; set; }
        Image Image { get; set; }
        Size ImageSize { get; set; }
        bool Moving { get; set; }

        void SetImage(Image image);
        void Clear();
        void Refresh();
        Rectangle GetImageRectangle();
        void StartMoving(Point point);
    }
    public class ViewBoxController : IViewBoxControler
    {
        public IRectController RectController { get; set; }
        public IMediator Mediator { get; set; }
        public PictureBox PictureBox { get; set; }
        public Image Image { get { return PictureBox.Image; } set { PictureBox.Image = value; } }
        public bool ImageNotExists { get { return PictureBox.Image == null; } }
        public Size Size { get; set; } = new Size();
        public Size ImageSize { get; set; } = new Size();
        public bool Moving { get; set; }
        public float ImageScale
        {
            get => imageScale;
            set
            {
                imageScale = value;
                Resize();
            }
        }
        protected Image originImage;
        protected Rectangle ImageRectangle;
        protected float imageScale = 1;
        protected Point movingStart = new Point();

        public ViewBoxController(PictureBox pictureBox)
        {
            PictureBox = pictureBox;
            PictureBox.MouseDown += MouseDown;
            PictureBox.MouseUp += MouseUp;
            PictureBox.MouseMove += Move;
            PictureBox.MouseWheel += MouseWheel;
            Size = PictureBox.Size;
            RectController = new RectController(this);
            RectController.OnNewFrameObject += (IFrameObject frameObject) =>
            {
                Mediator?.AddFrameObjectToCurrentFrame(frameObject);
                Mediator?.EndEditViewBox();
            };
        }
        public void SetImage(Image image)
        {
            PictureBox.Image = image;
            originImage = (Image)PictureBox.Image.Clone();
            ImageSize = PictureBox.Image.Size;
            ImageRectangle = new Rectangle(new Point(), PictureBox.Image.Size);
        }
        public void SetMediator(IMediator mediator)
        {
            Mediator = mediator;
        }
        public void StartMoving(Point point)
        {
            Moving = true;
            movingStart = point; 
        }
        private void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) MouseLeftDown(sender, e);
        }
        private void MouseLeftDown(object sender, MouseEventArgs e)
        {
            RectController.MouseLeftDown(sender, e);
        }
        private void MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) MouseLeftUp(sender, e);
        }
        private void MouseLeftUp(object sender, MouseEventArgs e)
        {
            if (Moving) Moving = !Moving;
            RectController.MouseLeftUp(sender, e);
        }
        private void Move(object sender, MouseEventArgs e)
        {
            if (Moving)
            {
                Point newlocation = PictureBox.Location;
                newlocation.X += e.X - movingStart.X;
                newlocation.Y += e.Y - movingStart.Y;
                PictureBox.Location = newlocation;
            }
            else
            {
                RectController.Move(sender, e);
            }
        }
        private void MouseWheel(object sender, MouseEventArgs e)
        {
            RectController.MouseWheel(sender, e);
            Resize();
        }
        public void Clear()
        {
            PictureBox.Image = (Image)originImage?.Clone();
        }
        public void Refresh()
        {
            PictureBox.Refresh();
        }
        private void Resize()
        {
            if (PictureBox.Image == null) return;

            PictureBox.Size = new Size(
                (int)(PictureBox.Image.Size.Width * ImageScale),
                (int)(PictureBox.Image.Size.Height * ImageScale));
        }
        public Rectangle GetImageRectangle()
        {
            return ImageRectangle;
        }
    }
}
