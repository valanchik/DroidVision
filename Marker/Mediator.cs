using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection.Marker
{
    interface IMediator
    {
        IFrame GetCurrentFrame();
        IElementController GetElementController(ElementControllerType type);
        IMarker GetMarker();
        void ShowFrame(IFrame frame);
        void ChangePlaySpeed(int spped);
    }
    interface IMediatorSetter
    {
        IMediator Mediator { get; set; }
        void SetMediator(IMediator mediator);
    }
    class MarkerMediator : IMediator
    {
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
        }
        public void AddElementController()
        {

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
            Marker.Window.Image = Marker.CurrentFrame.Image;
            ((ElementControllerFrame)GetElementController(ElementControllerType.Frame)).SetFrameState(Marker.CurrentFrame.State);
        }

        public void ChangePlaySpeed(int speed)
        {
            Marker.Timer.Interval = speed;
        }
    }
}
