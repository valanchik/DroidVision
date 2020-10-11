using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection.Marker.Interfaces
{
    public interface IFrameObejctContainer
    {
        List<IFrameObject> FrameObjectList { get; set; }
        IFrameObject Selected { get; set; }
        void Add(IFrameObject frameObejct);
        void Set(List<IFrameObject> list);
        void SetSelectedObject(int Id, bool seleted);
        void SetSelectedObject(IFrameObject d, bool seleted);
    }
}
