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
        PictureBox PictureBox { get; set; }
        IRectController RectController { get; set; }
        void SetImage(Image image);
        
    }
    public class ViewBoxController: IViewBoxControler
    {
        public delegate void ImageHandler(Image image);
        public event ImageHandler OnChangeImage;
        public PictureBox PictureBox { get; set; }
        public IRectController RectController { get; set; }
        public IMediator Mediator { get; set; }

        public ViewBoxController(PictureBox pictureBox)
        {
            PictureBox = pictureBox;
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
    }
}
