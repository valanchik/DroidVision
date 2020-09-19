using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection.Marker
{
    public interface IControlPoint
    {
        Point Point { get; set; }
        int Size { get; set; }
        Rectangle Rect { get; set; }
    }
    public class ControlPoint: IControlPoint
    {
        public Point Point { get; set; }
        public int Size { get; set; }
        public Rectangle Rect { get; set; }

        public ControlPoint(): this(new Point(), 1,  new Rectangle()) { }
        public ControlPoint(Point point, int size, Rectangle rect) 
        {
            Point = point;
            Size = size;
            Rect = rect;
        }
        public bool Contains(Point point)
        {
            return Rect.Contains(point);
        }
    }
    interface IRectNormalized
    {
        Vector2 Start { get; set; }
        Vector2 End { get; set; }
        List<IControlPoint> ControlPoints { get; set; }
    }
    public class RectNormalized: IRectNormalized
    {
        public Vector2 Start { get; set; }
        public Vector2 End { get; set; }
        public List<IControlPoint> ControlPoints { get; set; }
        public RectNormalized() : this(new Vector2(), new Vector2(), new List<IControlPoint>()) { }
        public RectNormalized(Vector2 start, Vector2 end, List<IControlPoint> controlPoints)
        {
            Start = start;
            End = end;
            ControlPoints = controlPoints;
        }
    }
}
