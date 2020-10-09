using System.Windows.Forms;
using Drawing = System.Drawing;
namespace YoloDetection.Marker
{
    public interface IViewBoxControler: IMediatorSetter
    {
        bool ImageNotExists { get; }
        Drawing.Size Size { get; set; }
        double ImageScale { get; set; }
        PictureBox PictureBox { get; set; }
        IRectController RectController { get; set; }
        Drawing.Image Image { get; set; }
        Drawing.Size ImageSize { get; set; }
        bool Moving { get; set; }
        Drawing.Point Location { get; set; }
        Drawing.Point MousePosition { get; set; }
        void SetImage(Drawing.Image image);
        void Clear();
        void Refresh();
        Drawing.Rectangle GetImageRectangle();
        void StartMoving(Drawing.Point point);
        void Resize();
        Label MousePositionLabel { get; }
    }
    public class ViewBoxController : IViewBoxControler
    {
        public IRectController RectController { get; set; }
        public IMediator Mediator { get; set; }
        public PictureBox PictureBox { get; set; }
        public Drawing.Image Image { get { return PictureBox.Image; } set { PictureBox.Image = value; } }
        public bool ImageNotExists { get { return PictureBox.Image == null; } }
        public Drawing.Size Size { get; set; } = new Drawing.Size();
        public Drawing.Size ImageSize { get; set; } = new Drawing.Size();
        public bool Moving { get; set; }
        public Drawing.Point Location { get => PictureBox.Location; set => PictureBox.Location = value; }
        public Drawing.Point MousePosition
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
                Resize();
            }
        }
        protected Drawing.Image originImage;
        protected Drawing.Rectangle ImageRectangle;
        protected double imageScale = 1;
        protected Drawing.Point movingStart = new Drawing.Point();
        private Drawing.Point _MousePosition { get; set; }
        public ViewBoxController(PictureBox pictureBox)
        {
            PictureBox = pictureBox;
            PictureBox.MouseDown += MouseDown;
            PictureBox.MouseUp += MouseUp;
            PictureBox.MouseMove += Move;
            PictureBox.MouseWheel += MouseWheel;
/*            PictureBox.MouseLeave += MouseLeave;
            PictureBox.MouseEnter += MouseEnter;*/
            Size = PictureBox.Size;
            RectController = new RectController(this);
            RectController.OnNewFrameObject += (IFrameObject frameObject) =>
            {
                Mediator?.AddFrameObjectToCurrentFrame(frameObject);
                Mediator?.EndEditViewBox();
            };
        }
        public void SetImage(Drawing.Image image)
        {
            PictureBox.Image = image;
            originImage = (Drawing.Image)PictureBox.Image.Clone();
            ImageSize = PictureBox.Image.Size;
            ImageRectangle = new Drawing.Rectangle(new Drawing.Point(), PictureBox.Image.Size);
        }
        public void SetMediator(IMediator mediator)
        {
            Mediator = mediator;
        }
        public void StartMoving(Drawing.Point point)
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
                Drawing.Point newlocation = PictureBox.Location;
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
            PictureBox.Image = (Drawing.Image)originImage?.Clone();
        }
        public void Refresh()
        {
            PictureBox.Refresh();
        }
        public void Resize()
        {
            if (PictureBox.Image == null) return;
            int w = (int)(PictureBox.Image.Size.Width * ImageScale);
            int h = (int)(PictureBox.Image.Size.Height * ImageScale);
            Drawing.Size nSize = new Drawing.Size(w, h);
            Drawing.Size delta = nSize - PictureBox.Size;
            delta.Width /= 2;
            delta.Height /= 2;
            if (nSize.Width>100 || nSize.Height>100)
            {
                PictureBox.Size = nSize;
                PictureBox.Location -= delta;
            }
            
        }
        public Drawing.Rectangle GetImageRectangle()
        {
            return ImageRectangle;
        }
        public Panel GetParentElement()
        {
            return (Panel)PictureBox.Parent;
        }
    }
}
