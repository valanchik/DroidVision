using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoloDetection.Marker
{
    public enum ElementValueTypes
    {
        Boolean,
        String,
        Int,
        Float
    }
    interface IElementController
    {
        void SetMarker(MarkerFasad marker);
        IElementController Add(StateElementName name, CheckBox val);
        IElementController Add(StateElementName name, Label val);
        IElementController Add(StateElementName name, TrackBar val);
        CheckBox    GetCheckbox(StateElementName name);
        Label       GetLabel(StateElementName name);
        TrackBar    GetTrackBar(StateElementName name);
    }
    class ElementController: IElementController
    {
        protected Dictionary<StateElementName, CheckBox> Checkboxes = new Dictionary<StateElementName, CheckBox>();
        protected Dictionary<StateElementName, Label> Labels = new Dictionary<StateElementName, Label>();
        protected Dictionary<StateElementName, TrackBar> Trackbars = new Dictionary<StateElementName, TrackBar>();
        protected MarkerFasad Marker { get; set; }
        protected Dictionary<StateElementName, ElementValueTypes> ValueTypes = new Dictionary<StateElementName, ElementValueTypes>();
       
        public CheckBox GetCheckbox(StateElementName name)
        {
            return Checkboxes[name];
        }
        public Label GetLabel(StateElementName name)
        {
            return Labels[name];
        }
        public TrackBar GetTrackBar(StateElementName name)
        {
            return Trackbars[name];
        }
        public virtual IElementController Add(StateElementName name, CheckBox val)
        {
            Checkboxes.Add(name, val);
            ValueTypes.TryAdd(name, ElementValueTypes.Boolean);
            return this;
        }
        public virtual IElementController Add(StateElementName name, Label val)
        {
            Labels.Add(name, val);
            ValueTypes.TryAdd(name, ElementValueTypes.String);
            return this;
        }
        public virtual IElementController Add(StateElementName name, TrackBar val)
        {
            Trackbars.Add(name, val);
            ValueTypes.TryAdd(name, ElementValueTypes.Int);
            return this;
        }
        
        public virtual void SetMarker(MarkerFasad marker)
        {
            Marker = marker;
        }
        


    }
    class ElementControllerCommon : ElementController, IElementController
    {
        public ElementControllerCommon() : base() { }

        public override IElementController Add(StateElementName name, CheckBox val)
        {
            return base.Add(name, val);
        }

        public override IElementController Add(StateElementName name, Label val)
        {
            return base.Add(name, val);
        }

        public override IElementController Add(StateElementName name, TrackBar val)
        {
            return base.Add(name, val);
        }
        
        
    }
    class ElementControllerFrame : ElementController, IElementController
    {
        public ElementControllerFrame() : base() { }
        public override IElementController Add(StateElementName name, CheckBox val)
        {
            
            val.CheckStateChanged += (object sender, EventArgs e) =>
            {
                if (Marker != null && Marker.CurrentFrame != null)
                {
                    Marker.CurrentFrame.State.SetBoolState(name, val.Checked);
                }
            };
            return this;
        }
        public override IElementController Add(StateElementName name, Label val)
        {
            
            return this;
        }
        public override IElementController Add(StateElementName name, TrackBar val)
        {
            
            val.Scroll += (object sender, EventArgs e) =>
            {
                Marker.ShowFrame(val.Value);
            };
            return this;
        }
        public Dictionary<StateElementName, IStateElement> GetNewDefaultStates()
        {
            Dictionary<StateElementName, IStateElement> data = new Dictionary<StateElementName, IStateElement>();
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
            foreach (KeyValuePair<StateElementName, IStateElement> entry in state.States)
            {
                SetState(entry);
            }
        }
        private void SetState(KeyValuePair<StateElementName, IStateElement> state)
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
