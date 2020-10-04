using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace YoloDetection.Marker
{
    public class ControlRect
    {
        public RectangleF Rect { get; set; }
        public ControlRect(PointF point, SizeF size)
        {
            Rect = new RectangleF(point, size);
        }
        public bool Contains(PointF pos)
        {
            return Rect.Contains(pos);
        }
        public void Offset(PointF pos)
        {
            Rect.Offset(pos);
        }
        public void Move(PointF pos)
        {
            RectangleF rect = Rect;
            rect.Offset(pos);
            Rect = rect;
        }
    }
}
