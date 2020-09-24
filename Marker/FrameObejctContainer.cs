using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection.Marker
{
    public interface IFrameObejctContainer
    {
        List<IFrameObject> FrameObjectList { get; set; }

        void Add(IFrameObject frameObejct);
        void Set(List<IFrameObject> list);
    }
    public class FrameObejctContainer: IFrameObejctContainer
    {
        public List<IFrameObject> FrameObjectList { get; set; } = new List<IFrameObject>();

        public void Set(List<IFrameObject> list)
        {
            FrameObjectList = list;
        }
        public void Add(IFrameObject frameObejct) {
            FrameObjectList.Add(frameObejct);
        }
        public void SetSelectedObject(int Id, bool seleted)
        {
            IFrameObject d = FrameObjectList.Find(fo => fo.Id == Id);
            if (d != null) {
                d.Selected = seleted;
                FrameObjectList.FindAll(fo => {
                    if (fo.Id != Id)
                    {
                        fo.Selected = false;
                    };
                    return false;
                });
            };
        }

    }
}
