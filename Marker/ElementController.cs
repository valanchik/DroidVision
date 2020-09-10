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
        private enum ElementValueTypes
        {
            Boolean,
            String,
            Int,
            Float
        }

        private Dictionary<StateElementName, CheckBox> Checkboxes;
        private Dictionary<StateElementName, Label> Labels;
        private Dictionary<StateElementName, ElementValueTypes> ValueTypes;
        public ElementController()
        {
            Checkboxes = new Dictionary<StateElementName, CheckBox>();
        }
        public ElementController Add(StateElementName type, CheckBox val)
        {
            Checkboxes.Add(type, val);
            if (!ValueTypes.ContainsKey(type))
            {
                ValueTypes.TryAdd(type, ElementValueTypes.Boolean);
            }
            return this;
        }
        public ElementController Add(StateElementName type, Label val)
        {
            Labels.Add(type, val);
            if (!ValueTypes.ContainsKey(type))
            {
                ValueTypes.Add(type, ElementValueTypes.String);
            }
            return this;
        }
        public void SetState(FrameState state)
        {
            if (Checkboxes.Count > 0)
            {
                foreach (KeyValuePair<StateElementName, IStateElement> entry in state.States)
                {
                    if (Checkboxes.ContainsKey(entry.Key))
                    {
                        Checkboxes[entry.Key].Checked = entry.Value.Checked;
                    }
                }
            }
        }
    }
}
