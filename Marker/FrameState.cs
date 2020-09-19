using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection.Marker
{
    
    public interface IStateElement
    {
        string Text { get; set; }
        bool Checked { get; set; }
        int Value { get; set; }
    }
    public class StateElement : IStateElement
    {
        public string Text { get; set; }
        public int Value { get; set; }
        public bool Checked { get; set; }
    }
    public class StateElementText : StateElement
    {
    }
    public class StateElementBool : StateElement
    {
    }
    public class StateElementInt : StateElement
    {
    }
    public class FrameState
    {
        public Dictionary<ElementName, IStateElement> States = new Dictionary<ElementName, IStateElement>();
        public bool GetBoolState(ElementName name)
        {
            return States[name].Checked;
        }
        public void SetBoolState(ElementName name, bool state)
        {
            States[name].Checked = state;
        }
        public string GetTextState(ElementName name)
        {
            return States[name].Text;
        }
        public void SetTextState(ElementName name, string text)
        {
            States[name].Text = text;
        }
        public int GetIntState(ElementName name)
        {
            return States[name].Value;
        }
        public void SetIntState(ElementName name, int value)
        {
            States[name].Value = value;
        }

    }
}
