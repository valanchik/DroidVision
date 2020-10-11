using System.Drawing;
using YoloDetection.Marker.Interfaces;

namespace YoloDetection.Marker
{
    public interface IControlRect
    {
        IRectNormalized Rect { get; set; }
        RectNormalizesPointType Type { get; set; }
        IFrameObject FrameObject { get; set; }
        bool Selected { get; set; }

        bool Contains(Point<double> pos);
        void Move(Point<double> pos);
    }
}