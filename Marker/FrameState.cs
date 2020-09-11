using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection.Marker
{
    enum StateElementName
    {
        FrameId,
        InBookmarks,
        Hided,
        Removed,
        TimeLineBar
    }
    interface IStateElement
    {
        string Text { get; set; }
        bool Checked { get; set; }
        int Value { get; set; }
    }
    class StateElementText : IStateElement
    {
        public string Text { get; set; }
        public int Value { get; set; }
        public bool Checked { get; set; }
    }
    class StateElementCheckbox : IStateElement
    {
        public string Text { get; set; }
        public int Value { get; set; }
        public bool Checked { get; set; }
    }
    class FrameState
    {
        public Dictionary<StateElementName, IStateElement> States = new Dictionary<StateElementName, IStateElement>();
        public bool GetBoolState(StateElementName name)
        {
            return States[name].Checked;
        }
        public void SetBoolState(StateElementName name, bool state)
        {
            States[name].Checked = state;
        }
        public string GetTextState(StateElementName name)
        {
            return States[name].Text;
        }
        public void SetTextState(StateElementName name, string text)
        {
            States[name].Text = text;
        }

    }
}
