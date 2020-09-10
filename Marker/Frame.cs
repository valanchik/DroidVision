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
        bool IsEmpty();
        int GetFrameId();
        Image GetImage();
        FrameState GetState();
    }

    struct Frame : IFrame
    {
        private Image Image;
        private int FrameId;
        private FrameState State;

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
        public int GetFrameId()
        {
            return FrameId;
        }
        public Image GetImage()
        {
            return Image;
        }

        public FrameState GetState()
        {
            return State;
        }
    }
}
