using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoloDetection.Marker
{
    public enum ElementEvenType
    {
        Click,
        Scroll,
        CheckStateChanged
    }
    public interface IElementEvent
    {
        EventArgs Args { get; set; }
        ElementName Name { get; set; }
        object Sender { get; set; }
        ElementEvenType Type { get; set; }
    }
    public class ElementEvent : IElementEvent
    {
        public ElementName Name { get; set; }
        public ElementEvenType Type { get; set; }
        public object Sender { get; set; }
        public EventArgs Args { get; set; }
    }
    public delegate void EventElentHandler(ElementName element, object sender, EventArgs e);
    public enum ElementName
    {
        FrameId,
        InBookmarks,
        Hided,
        Removed,
        TimeLineBar,
        PlayRepeat,
        PlaySpeeed,
        ViewBox,
        playTimer,
        createFrameObejct
    }
    public enum ElementValueTypes
    {
        Boolean,
        String,
        Int,
        Float,
        Image,
        Button
    }
    public interface IElementController : IMediatorSetter
    {
        event Action<IElementEvent> Event;

        IElementController Add(ElementName name, CheckBox val);
        IElementController Add(ElementName name, Button val);
        IElementController Add(ElementName name, Label val);
        IElementController Add(ElementName name, TrackBar val);
        IElementController Add(ElementName name, PictureBox val);
        IElementController Add(ElementName name, Timer val);
        CheckBox    GetCheckbox(ElementName name);
        Button    GetButton(ElementName name);
        Label       GetLabel(ElementName name);
        TrackBar    GetTrackBar(ElementName name);
        PictureBox    GetPictureBox(ElementName name);
        Timer    GetTimer(ElementName name);
    }
    
    class ElementController: IElementController
    {
        public event Action<IElementEvent> Event;
        /*public event Action<ElementEvenType, ElementName>*/
        protected Dictionary<ElementName, CheckBox> Checkboxes = new Dictionary<ElementName, CheckBox>();
        protected Dictionary<ElementName, Button> Buttons = new Dictionary<ElementName, Button>();
        protected Dictionary<ElementName, Label> Labels = new Dictionary<ElementName, Label>();
        protected Dictionary<ElementName, TrackBar> Trackbars = new Dictionary<ElementName, TrackBar>();
        protected Dictionary<ElementName, PictureBox> PictureBoxes = new Dictionary<ElementName, PictureBox>();
        protected Dictionary<ElementName, Timer> Timers = new Dictionary<ElementName, Timer>();
        public IMediator Mediator { get; set; }
        protected Dictionary<ElementName, ElementValueTypes> ValueTypes = new Dictionary<ElementName, ElementValueTypes>();
        public void SetMediator(IMediator mediator ) {
            Mediator = mediator;
        }
        public CheckBox GetCheckbox(ElementName name)
        {
            return Checkboxes[name];
        }
        public Button GetButton(ElementName name)
        {
            return Buttons[name];
        }
        public Label GetLabel(ElementName name)
        {
            return Labels[name];
        }
        public TrackBar GetTrackBar(ElementName name)
        {
            return Trackbars[name];
        }
        public PictureBox GetPictureBox(ElementName name)
        {
            return PictureBoxes[name];
        }
        public Timer GetTimer(ElementName name)
        {
            return Timers[name];
        }
        public virtual IElementController Add(ElementName name, CheckBox val)
        {
            Checkboxes.Add(name, val);
            ValueTypes.TryAdd(name, ElementValueTypes.Boolean);
            val.CheckStateChanged += (object sender, EventArgs e) => Event?.Invoke(new ElementEvent() { Name = name, Type = ElementEvenType.Click, Sender = sender, Args = e });
            return this;
        }
        public virtual IElementController Add(ElementName name, Button val)
        {
            Buttons.Add(name, val);
            ValueTypes.TryAdd(name, ElementValueTypes.Button);
            val.Click += (object sender, EventArgs e) => Event?.Invoke(new ElementEvent() { Name = name, Type = ElementEvenType.Click, Sender = sender, Args = e });
            return this;
        }
        public virtual IElementController Add(ElementName name, Label val)
        {
            Labels.Add(name, val);
            ValueTypes.TryAdd(name, ElementValueTypes.String);
            return this;
        }
        public virtual IElementController Add(ElementName name, TrackBar val)
        {
            Trackbars.Add(name, val);
            ValueTypes.TryAdd(name, ElementValueTypes.Int);
            val.Scroll += (object sender, EventArgs e) => Event?.Invoke(new ElementEvent() { Name = name, Type = ElementEvenType.Click, Sender = sender, Args = e });
            return this;
        }
        public virtual IElementController Add(ElementName name, PictureBox val)
        {
            PictureBoxes.Add(name, val);
            ValueTypes.TryAdd(name, ElementValueTypes.Image);
            return this;
        }
        public virtual IElementController Add(ElementName name, Timer val)
        {
            Timers.Add(name, val);
            ValueTypes.TryAdd(name, ElementValueTypes.Int);
            return this;
        }
    }
    class ElementControllerCommon : ElementController
    {
        public ElementControllerCommon(): base() { }
    }
    class ElementControllerButton : ElementController, IElementController
    {
        public ElementControllerButton() : base() {}
    }
    class ElementControllerPlaySpped : ElementController
    {
        public ElementControllerPlaySpped() : base() { }
    }

    class ElementControllerImage : ElementController
    {
        public ElementControllerImage() : base() { }
    }
    class ElementControllerTimer : ElementController
    {
        public ElementControllerTimer() : base() { }
    }
    class ElementControllerFrame : ElementController
    {
        public ElementControllerFrame() : base() { }
        public override IElementController Add(ElementName name, CheckBox val)
        {
            Checkboxes.Add(name, val);
            ValueTypes.TryAdd(name, ElementValueTypes.Boolean);
            val.CheckStateChanged += (object sender, EventArgs e) =>
            {
                Mediator.GetCurrentFrame()?.State.SetBoolState(name, val.Checked);
            };
            return this;
        }
        public Dictionary<ElementName, IStateElement> GetNewDefaultStates()
        {
            Dictionary<ElementName, IStateElement> data = new Dictionary<ElementName, IStateElement>();
            foreach (var entry in ValueTypes)
            {
                switch (entry.Value)
                {
                    case (ElementValueTypes.Boolean):
                        data.Add(entry.Key, new StateElementBool());
                        break;
                    case (ElementValueTypes.String):
                        data.Add(entry.Key, new StateElementText());
                        break;
                    case (ElementValueTypes.Int):
                        data.Add(entry.Key, new StateElementInt());
                        break;
                }
            }
            return data;
        }
        public void SetFrameState(FrameState state)
        {
            foreach (KeyValuePair<ElementName, IStateElement> entry in state.States)
            {
                SetState(entry);
            }
        }
        private void SetState(KeyValuePair<ElementName, IStateElement> state)
        {
            ElementValueTypes type = ValueTypes[state.Key];
            if (type == ElementValueTypes.Boolean && Checkboxes.ContainsKey(state.Key))
            {
                Checkboxes[state.Key].Checked = state.Value.Checked;
            }
            if (type == ElementValueTypes.String && Labels.ContainsKey(state.Key))
            {
                Labels[state.Key].Text = state.Value.Text;
            }
            if (type == ElementValueTypes.Int && Trackbars.ContainsKey(state.Key))
            {
                Trackbars[state.Key].Value = state.Value.Value > 0 ? state.Value.Value : 1;
            }
        }
    }
}
