using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoloDetection.Marker
{
    enum ElementName
    {
        FrameId,
        InBookmarks,
        Hided,
        Removed,
        TimeLineBar,
        playRepeat
    }
    public enum ElementValueTypes
    {
        Boolean,
        String,
        Int,
        Float
    }
    interface IElementController: IMediatorSetter
    {
        
        IElementController Add(ElementName name, CheckBox val);
        IElementController Add(ElementName name, Label val);
        IElementController Add(ElementName name, TrackBar val);
        CheckBox    GetCheckbox(ElementName name);
        Label       GetLabel(ElementName name);
        TrackBar    GetTrackBar(ElementName name);
    }
    class ElementController: IElementController
    {
        protected Dictionary<ElementName, CheckBox> Checkboxes = new Dictionary<ElementName, CheckBox>();
        protected Dictionary<ElementName, Label> Labels = new Dictionary<ElementName, Label>();
        protected Dictionary<ElementName, TrackBar> Trackbars = new Dictionary<ElementName, TrackBar>();
        public IMediator Mediator { get; set; }
        protected Dictionary<ElementName, ElementValueTypes> ValueTypes = new Dictionary<ElementName, ElementValueTypes>();
        public ElementController()
        {
            
        }
        public void SetMediator(IMediator mediator ) {
            Mediator = mediator;
        }
        public CheckBox GetCheckbox(ElementName name)
        {
            return Checkboxes[name];
        }
        public Label GetLabel(ElementName name)
        {
            return Labels[name];
        }
        public TrackBar GetTrackBar(ElementName name)
        {
            return Trackbars[name];
        }
        public virtual IElementController Add(ElementName name, CheckBox val)
        {
            Checkboxes.Add(name, val);
            ValueTypes.TryAdd(name, ElementValueTypes.Boolean);
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
            return this;
        }
        
    }
    class ElementControllerCommon : ElementController
    {
        public ElementControllerCommon(): base() { }

        public override IElementController Add(ElementName name, CheckBox val)
        {
            return base.Add(name, val);
        }

        public override IElementController Add(ElementName name, Label val)
        {
            return base.Add(name, val);
        }

        public override IElementController Add(ElementName name, TrackBar val)
        {
            return base.Add(name, val);
        }
    }
    class ElementControllerFrame : ElementController
    {
        public ElementControllerFrame() : base() { }
        public override IElementController Add(ElementName name, CheckBox val)
        {
            base.Add(name, val);
            val.CheckStateChanged += (object sender, EventArgs e) =>
            {
                Mediator.GetCurrentFrame()?.State.SetBoolState(name, val.Checked);
            };
            return this;
        }
        public override IElementController Add(ElementName name, Label val)
        {
            base.Add(name, val);
            return this;
        }
        public override IElementController Add(ElementName name, TrackBar val)
        {
            base.Add(name, val);
            val.Scroll += (object sender, EventArgs e) =>
            {
                Mediator.GetMarker().ShowFrame(val.Value);
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
