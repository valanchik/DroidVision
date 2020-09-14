using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection.Marker
{
    interface IFrame
    {
        Image Image { get; set; }
        int FrameId { get; set; }
        FrameState State { get; set; }
        List<IFrameObject> Objects { get; set; }
        bool IsEmpty();
    }

    class Frame : IFrame
    {
        public Image Image { get; set; }
        public int FrameId { get; set; }
        public FrameState State { get; set; }
        public List<IFrameObject> Objects { get; set; }

        public Frame(Image image, int frameId, FrameState state)
        {
            Image = image;
            FrameId = frameId;
            State = state;
            Objects = new List<IFrameObject>();
        }
        public bool IsEmpty()
        {
            return false;
        }

    }
}
