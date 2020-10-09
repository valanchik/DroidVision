using System.Drawing;
namespace YoloDetection.Marker
{
    public interface IControlRect
    {
        IRectNormalized Rect { get; set; }
        IFrameObject FrameObject { get; set; }
        bool Contains(Point<double> pos);
        void Move(Point<double> pos);
    }
}