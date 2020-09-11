using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoloDetection.Marker
{
    class ElementController
    {
        public enum ElementValueTypes
        {
            Boolean,
            String,
            Int,
            Float
        }

        private Dictionary<StateElementName, CheckBox> Checkboxes = new Dictionary<StateElementName, CheckBox>();
        private Dictionary<StateElementName, Label> Labels = new Dictionary<StateElementName, Label>();
        public Dictionary<StateElementName, ElementValueTypes> ValueTypes = new Dictionary<StateElementName, ElementController.ElementValueTypes>();
        private MarkerFasad Marker;
        public ElementController()
        {
            
        }
        public void SetMarker(MarkerFasad marker)
        {
            Marker = marker;
        }
        public ElementController Add(StateElementName name, CheckBox val)
        {
            Checkboxes.Add(name, val);
            ValueTypes.TryAdd(name, ElementValueTypes.Boolean);
            val.CheckStateChanged += (object sender, EventArgs e) =>
            {
                if (Marker!=null && Marker.CurrentFrame != null)
                {
                    FrameState state = Marker.CurrentFrame.GetState();

                    state.SetBoolState(name, val.Checked);
                }
            };
            return this;
        }
        public ElementController Add(StateElementName name, Label val)
        {
            Labels.Add(name, val);
            ValueTypes.TryAdd(name, ElementValueTypes.String);
            return this;
        }
        public void SetFrameState(FrameState state)
        {
            foreach (KeyValuePair<StateElementName, IStateElement> entry in state.States)
            {
                SetState(entry);
            }
        }
        public Dictionary<StateElementName, IStateElement> GetNewDefaultStates()
        {
            Dictionary<StateElementName, IStateElement> data = new Dictionary<StateElementName, IStateElement>();
            foreach (var entry in ValueTypes)
            {
                switch(entry.Value)
                {
                    case (ElementValueTypes.Boolean):
                        data.Add(entry.Key, new StateElementCheckbox());
                        break;
                    case (ElementValueTypes.String):
                        data.Add(entry.Key, new StateElementText());
                        break;
                }
            }
            return data;
        }
        private void SetState(KeyValuePair<StateElementName, IStateElement> state)
        {
            ElementValueTypes type = ValueTypes[state.Key];
            if (type == ElementValueTypes.Boolean && Checkboxes.ContainsKey(state.Key))
            {
                Checkboxes[state.Key].Checked = state.Value.GetBool();
            }
            if (type == ElementValueTypes.String && Labels.ContainsKey(state.Key))
            {
                Labels[state.Key].Text = state.Value.GetText();
            }
        }
    }
}
