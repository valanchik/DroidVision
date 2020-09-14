using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection.Marker
{
    interface IFrameObject
    {
        Rectangle Rect { get; set; }
    }
    class FrameObject: IFrameObject
    {
        public Rectangle Rect { get; set; }
        public FrameObject(Rectangle rect)
        {
            Rect = rect;
        }
    }
}
