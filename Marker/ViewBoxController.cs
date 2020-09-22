using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static YoloDetection.Marker.ViewBoxController;

namespace YoloDetection.Marker
{
    public interface IViewBoxControler: IMediatorSetter
    {
        event ImageHandler OnChangeImage;
        bool ImageNotExists { get; }
        Size ImageSize { get; set; }
        float ImageScale { get; set; }
        PictureBox PictureBox { get; set; }
        IRectController RectController { get; set; }
        Image Image { get; set; }
        void SetImage(Image image);
        void Clear();
        bool IsImageExists();
    }
    public class ViewBoxController: IViewBoxControler
    {
        public delegate void ImageHandler(Image image);
        public event ImageHandler OnChangeImage;
        public IRectController RectController { get; set; }
        public IMediator Mediator { get; set; }
        public PictureBox PictureBox { get; set; }
        public Image Image { get { return PictureBox.Image; } set { PictureBox.Image = value; } }
        public bool ImageNotExists { get { return PictureBox.Image == null; } }
        public Size ImageSize { get; set; } = new Size();
        public float ImageScale { get; set; } = 1;
        public ViewBoxController(PictureBox pictureBox)
        {
            PictureBox = pictureBox;
            PictureBox.MouseDown += MouseDown;
            PictureBox.MouseUp += MouseUp;
            PictureBox.MouseMove += Move;
            PictureBox.MouseWheel += MouseWheel;
            ImageSize = PictureBox.Size;
            RectController = new RectController(this);
            RectController.OnNewFrameObject += (IFrameObject frameObject) =>
            {
                Mediator?.AddFrameObjectToCurrentFrame(frameObject);
            };
        }

        public void SetImage(Image image)
        {
            PictureBox.Image = image;
            PictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            OnChangeImage?.Invoke(PictureBox.Image);
        }

        public void SetMediator(IMediator mediator)
        {
            Mediator = mediator;
        }
        private void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) MouseLeftDown(sender, e);
        }
        private void MouseLeftDown(object sender, MouseEventArgs e)
        {
            Clear();
            RectController.MouseLeftDown(sender, e);
        }
        public void Clear()
        {
            PictureBox.Image = (Image)originImage?.Clone();
        }
        public bool IsImageExists()
        {

        }
        private void MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) MouseLeftUp(sender, e);
        }
        private void MouseLeftUp(object sender, MouseEventArgs e)
        {
            Drawing = false;
            endPoint = ConverPointToPointF(e.Location.Divide(ImageScale));
            IFrameObject frameObject = new FrameObject();
            frameObject.Rect = new RectNormalized(startPoint, endPoint, new List<IControlPoint>());
            OnNewFrameObject?.Invoke(frameObject);
            Draw();
        }
        private void Move(object sender, MouseEventArgs e)
        {
            endPoint = ConverPointToPointF(e.Location.Divide(ImageScale));
            if (Drawing)
            {
                Draw();
            }
        }
        private void MouseWheel(object sender, MouseEventArgs e)
        {
            RectController.MouseWheel(sender, e);
            Resize();
        }
        private void Resize()
        {
            PictureBox.Size = new Size(
                (int)(PictureBox.Image.Size.Width * ImageScale),
                (int)(PictureBox.Image.Size.Height * ImageScale));
        }
    }
}
