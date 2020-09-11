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
        bool IsEmpty();
    }

    struct Frame : IFrame
    {
        public Image Image { get; set; }
        public int FrameId { get; set; }
        public FrameState State { get; set; }

        public Frame(Image image, int frameId, FrameState state)
        {
            Image = image;
            FrameId = frameId;
            State = state;
        }
        public bool IsEmpty()
        {
            return false;
        }
    }
}
