using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoloDetection.Marker
{
    public interface IMediator
    {
        RectController rectController { get; set; }
        IViewBoxControler ViewBoxController { get; set; }
        IFrame GetCurrentFrame();
        IElementController GetElementController(ElementControllerType type);
        IMarker GetMarker();
        void ShowFrame(IFrame frame);
        void ChangePlaySpeed(int spped);
        void AddFrameObjectToCurrentFrame(IFrameObject frameObject);
    }
    public interface IMediatorSetter
    {
        IMediator Mediator { get; set; }
        void SetMediator(IMediator mediator);
    }
    public class MarkerMediator : IMediator
    {
        public RectController rectController { get; set; }
        private IMarker Marker;
        private Dictionary<ElementControllerType, IElementController> ElementControllers;
        public IViewBoxControler ViewBoxController { get; set; }
        public MarkerMediator(IMarker marker, Dictionary<ElementControllerType, IElementController> elementControllers)
        {
            Marker = marker;
            Marker.SetMediator(this);
            ElementControllers = elementControllers;
            foreach (var entry in ElementControllers)
            {
                entry.Value.SetMediator(this);
            }
            ViewBoxController = new ViewBoxController(GetViewBox());
            ViewBoxController.SetMediator(this);
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
            
            ViewBoxController.SetImage(Marker.CurrentFrame.Image);
            
            if (Marker.CurrentFrame.Objects.Count>0)
            {
                ViewBoxController.RectController.SetFrameObjectList(Marker.CurrentFrame.Objects);
                ViewBoxController.RectController.Draw();
            }
            ((ElementControllerFrame)GetElementController(ElementControllerType.Frame))
            .SetFrameState(Marker.CurrentFrame.State);
                        
        }
        public void ChangePlaySpeed(int speed)
        {
            GetPlayTimer().Interval = speed;
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
        public void AddFrameObjectToCurrentFrame(IFrameObject frameObject)
        {
            
            if (Marker.CurrentFrame != null)
            {
                Marker.CurrentFrame.Objects.Add(frameObject);
                ViewBoxController.RectController.SetFrameObjectList(Marker.CurrentFrame.Objects);
            }
            
        }
    }
}
