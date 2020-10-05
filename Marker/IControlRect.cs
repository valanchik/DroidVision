using System.Drawing;
namespace YoloDetection.Marker
{
    public interface IControlRect
    {
        RectangleF Rect { get; set; }
        IFrameObject FrameObject { get; set; }
        bool Contains(PointF pos);
        void Move(PointF pos);
        void Offset(PointF pos);
    }
}