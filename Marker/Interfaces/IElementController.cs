using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marker.Interfaces
{
    public interface IElementController : IMediatorSetter
    {
        event Action<IElementEvent> Event;

        IElementController Add(ElementName name, CheckBox val);
        IElementController Add(ElementName name, Button val);
        IElementController Add(ElementName name, Label val);
        IElementController Add(ElementName name, TrackBar val);
        IElementController Add(ElementName name, PictureBox val);
        IElementController Add(ElementName name, Timer val);
        CheckBox GetCheckbox(ElementName name);
        Button GetButton(ElementName name);
        Label GetLabel(ElementName name);
        TrackBar GetTrackBar(ElementName name);
        PictureBox GetPictureBox(ElementName name);
        Timer GetTimer(ElementName name);
    }
}
