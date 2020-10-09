using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace YoloDetection.Marker
{

    public class ControlRect : IControlRect
    {
        public IRectNormalized Rect { get; set; }
        public IFrameObject FrameObject { get; set; }
        public ControlRect(IFrameObject frameObject, Point<double> point, Size<double> size)
        {
            FrameObject = frameObject;
            Rect = new RectNormalized(point, size);
        }
        public bool Contains(Point<double> pos)
        {
            return Rect.Contains(pos);
        }
        public void Move(Point<double> pos)
        {
            Rect.Move(pos);
        }
    }
}
