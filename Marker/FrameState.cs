using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection.Marker
{
    enum StateElementName
    {
        InBookmarks,
        Hided,
        Removed
    }
    interface IStateElement
    {
        string GetText();
        bool GetBool();
    }
    abstract class StateElement
    {
        public abstract string GetText();
        public abstract bool GetBool();
    }
    class StateElementText : StateElement, IStateElement
    {
        public string Text;
        public override string GetText()
        {
            return Text;
        }
        public override bool GetBool()
        {
            return Text != "";
        }
    }
    class StateElementCheckbox : StateElement, IStateElement
    {
        public bool Checked;
        public override string GetText()
        {
            return Checked.ToString();
        }
        public override bool GetBool()
        {
            return Checked;
        }
    }
    class FrameState
    {
        public Dictionary<StateElementName, IStateElement> States = new Dictionary<StateElementName, IStateElement>();
        public bool GetBoolState(StateElementName name)
        {
            return States[name].GetBool();
        }
        public string GetTextState(StateElementName name)
        {
            return States[name].GetText();
        }

    }
}
