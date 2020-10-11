using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoloDetection.Marker.Interfaces;

namespace YoloDetection.Marker
{
    public struct Element { }
    
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
            Marker.Loaded += MarkerLoadedData;
            ElementControllers = elementControllers;
            foreach (var entry in ElementControllers)
            {
                entry.Value.SetMediator(this);
                entry.Value.Event += EmitElementEvent;
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
                ViewBoxController.RectController.DrawAll();
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
                   .GetTimer(ElementName.PlayTimer);
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
        public void EndEditViewBox()
        {
            Button cfoBtn = GetElementController(ElementControllerType.Button).GetButton(ElementName.CreateFrameObejct);
            cfoBtn.Enabled = true;
            ViewBoxController.RectController.CreatingFrameObjec = false;
        }
        protected void EmitElementEvent(IElementEvent elementEvent)
        {
            switch (elementEvent.Type)
            {
                case ElementEvenType.Click:
                    EmitClick(elementEvent.Name, elementEvent.Sender, elementEvent.Args);
                    break;
                case ElementEvenType.Scroll:
                    EmitScroll(elementEvent.Name, elementEvent.Sender, elementEvent.Args);
                    break;
                case ElementEvenType.CheckStateChanged:
                    EmitCheckStateChanged(elementEvent.Name, elementEvent.Sender, elementEvent.Args);
                    break;
            }
        }
        protected void EmitClick(ElementName element, object sender, EventArgs e)
        {
            switch (element)
            {
                case ElementName.CreateFrameObejct:
                    ViewBoxController.RectController.CreatingFrameObjec = !ViewBoxController.RectController.CreatingFrameObjec;
                    Button btn = (Button)sender;
                    btn.Enabled = !ViewBoxController.RectController.CreatingFrameObjec;
                    break;
            }
        }
        protected void EmitScroll(ElementName element, object sender, EventArgs e)
        {
            switch (element)
            {
                case ElementName.PlaySpeeed:
                    TrackBar tb = (TrackBar)sender;
                    ChangePlaySpeed(tb.Value);
                    break;
                case ElementName.TimeLineBar:
                    TrackBar tlb = (TrackBar)sender;
                    Marker.ShowFrame(tlb.Value);
                    break;
            }
        }
        protected void EmitCheckStateChanged(ElementName element, object sender, EventArgs e)
        {
        }
        protected void MarkerLoadedData()
        {
            if (Marker.Data.Count == 0) return;
            TrackBar timeline = GetElementController(ElementControllerType.Common).GetTrackBar(ElementName.TimeLineBar);
            timeline.Maximum = Marker.Data.Count;
            Marker.ShowFrame(Marker.Data[0]);
            ViewBoxController.ImageScale = 1;
            ViewBoxController.Resize();
        }
    }
}
