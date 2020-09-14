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


    }
}
