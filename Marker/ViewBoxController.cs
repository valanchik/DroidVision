using System.Drawing;
using System.Windows.Forms;
using Marker.Interfaces;
namespace Marker
{
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
        public Point Location { get => PictureBox.Location; set => PictureBox.Location = value; }
        public Point MousePosition
        {
            get => _MousePosition; set
            {
                _MousePosition = value;
                MousePositionLabel.Text = _MousePosition.ToString();
            } }
        public Label MousePositionLabel => Mediator.GetElementController(ElementControllerType.Common).GetLabel(ElementName.MousePosition);
        public double ImageScale
        {
            get => imageScale;
            set
            {
                imageScale = value;
            }
        }
        private Point _MousePosition { get; set; }
        protected Image originImage;
        protected Rectangle ImageRectangle;
        protected double imageScale = 1;
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
            MousePosition = e.Location;
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
        }
        public void Clear()
        {
            PictureBox.Image = (Image)originImage?.Clone();
        }
        public void Refresh()
        {
            PictureBox.Refresh();
        }
        public void Resize()
        {
            Resize(Point.Empty);
        }
        public void Resize(Point mousePosition)
        {
            if (PictureBox.Image == null) return;
            int w = (int)(PictureBox.Image.Size.Width * ImageScale);
            int h = (int)(PictureBox.Image.Size.Height * ImageScale);
            int mW = (int)(mousePosition.X * ImageScale);
            int mH = (int)(mousePosition.Y * ImageScale);
            Size nSize = new Size(w, h);
            Size<double> coef = new Size<double>();
            coef.Width = (double)nSize.Width / (double)PictureBox.Size.Width;
            coef.Height = (double)nSize.Height / (double)PictureBox.Size.Height;
            Size newMousePos = new Size((int)(mousePosition.X*coef.Width), (int)(mousePosition.Y*coef.Height));
            Size offset = new Size(newMousePos.Width-mousePosition.X, newMousePos.Height - mousePosition.Y);

            if (nSize.Width>100 || nSize.Height>100)
            {
                PictureBox.Size = nSize;
                PictureBox.Location -= offset;
            }
        }
        public Rectangle GetImageRectangle()
        {
            return ImageRectangle;
        }
        public Panel GetParentElement()
        {
            return (Panel)PictureBox.Parent;
        }
    }
}
