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
        Removed
    }
    interface IStateElement
    {
        string GetText();
        void SetText(string text);
        bool GetBool();
        void SetBool(bool val);
    }
    abstract class StateElement: IStateElement
    {
        public abstract string GetText();
        public abstract bool GetBool();
        public abstract void SetText(string text);
        public abstract void SetBool(bool val);
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

        public override void SetText(string text)
        {
            Text = text;
        }

        public override void  SetBool(bool val)
        {
            Text = val ? bool.TrueString : bool.FalseString;
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

        public override void SetText(string text)
        {
            Checked = text != "";
        }

        public override void SetBool(bool val)
        {
            Checked = val;
        }
    }
    class FrameState
    {
        public Dictionary<StateElementName, IStateElement> States = new Dictionary<StateElementName, IStateElement>();
        public bool GetBoolState(StateElementName name)
        {
            return States[name].GetBool();
        }
        public void SetBoolState(StateElementName name, bool state)
        {
            States[name].SetBool(state);
        }
        public string GetTextState(StateElementName name)
        {
            return States[name].GetText();
        }
        public void SetTextState(StateElementName name, string text)
        {
            States[name].SetText(text);
        }

    }
}
