using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoloDetection.Marker.Interfaces
{
    public interface IRectController
    {
        bool CreatingFrameObjec { get; set; }
        List<IControlRect> ControlRects { get; set; }
        IControlRect SelectedControlRect { get; set; }

        event Action<IFrameObject> OnNewFrameObject;
        void DrawAll();
        void Draw(IFrameObject frameObject);
        void MouseLeftDown(object sender, MouseEventArgs e);
        void MouseLeftUp(object sender, MouseEventArgs e);
        void MouseWheel(object sender, MouseEventArgs e);
        void Move(object sender, MouseEventArgs e);
        void SetFrameObjectList(List<IFrameObject> list);
    }
}
