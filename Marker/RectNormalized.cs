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
    public interface IRectNormalized
    {
        PointF Start { get; set; }
        PointF End { get; set; }
        PointF LeftTop { get; }
        PointF LeftBottom { get;  }
        PointF RightTop { get; }
        PointF RightBottom { get; }
        List<IControlPoint> ControlPoints { get; set; }
    }
    public class RectNormalized: IRectNormalized
    {
        private PointF _LeftBottom;
        private PointF _RightTop;
        private PointF _End;
        public PointF Start { get; set; }
        public PointF End { get => _End; set {
                _End = value;
                _LeftBottom = new PointF(Start.X, _End.Y);
                _RightTop = new PointF(_End.X,  Start.Y );
            } }
        public PointF LeftTop => Start;
        public PointF LeftBottom => _LeftBottom;
        public PointF RightTop => _RightTop;
        public PointF RightBottom => End;
        public List<IControlPoint> ControlPoints { get; set; }
        public RectNormalized() : this(new PointF(), new PointF(), new List<IControlPoint>()) { }
        public RectNormalized(PointF start, PointF end, List<IControlPoint> controlPoints)
        {
            Start = start;
            End = end;
            ControlPoints = controlPoints;
        }
    }
}
