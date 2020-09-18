using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoloDetection.Marker
{
    interface IMediator
    {
        RectController rectController { get; set; }

        IFrame GetCurrentFrame();
        IElementController GetElementController(ElementControllerType type);
        IMarker GetMarker();
        void ShowFrame(IFrame frame);
        void ChangePlaySpeed(int spped);
        void SetPictureBoxToRectController(PictureBox pictureBox);
    }
    interface IMediatorSetter
    {
        IMediator Mediator { get; set; }
        void SetMediator(IMediator mediator);
    }
    class MarkerMediator : IMediator
    {
        public RectController rectController { get; set; }
        private IMarker Marker;
        private Dictionary<ElementControllerType, IElementController> ElementControllers;
        public MarkerMediator(IMarker marker, Dictionary<ElementControllerType, IElementController> elementControllers)
        {
            Marker = marker;
            Marker.SetMediator(this);
            ElementControllers = elementControllers;
            foreach (var entry in ElementControllers)
            {
                entry.Value.SetMediator(this);
            }
            SetPictureBoxToRectController(GetViewBox());
        }
        public IMarker GetMarker()
        {
            return Marker;
        }
        public IFrame GetCurrentFrame()
        {
            return Marker.CurrentFrame;
        }
        public IElementController GetElementController(ElementControllerType type)
        {
            return ElementControllers[type];
        }
        public void ShowFrame(IFrame frame)
        {
            
            Marker.CurrentFrame = frame;
            GetViewBox().Image = Marker.CurrentFrame.Image;
                
            ((ElementControllerFrame)GetElementController(ElementControllerType.Frame))
            .SetFrameState(Marker.CurrentFrame.State);
                        
        }
        public void ChangePlaySpeed(int speed)
        {
            GetPlayTimer().Interval = speed;
        }
        public void SetPictureBoxToRectController(PictureBox pictureBox)
        {
            if (pictureBox == null) return;

            rectController = new RectController(pictureBox);
        }
        private Timer GetPlayTimer ()
        {
            return ((ElementControllerTimer)GetElementController(ElementControllerType.Timer))
                   .GetTimer(ElementName.playTimer);
        }
        private PictureBox GetViewBox()
        {
            return ((ElementControllerImage)GetElementController(ElementControllerType.Window))
                   .GetPictureBox(ElementName.ViewBox);
        }

        
    }
}
