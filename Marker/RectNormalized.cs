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
        RectangleF Rectangle { get; set; }
        bool Contains(PointF point);
        void Move(PointF pos);
    }
    public class RectNormalized: IRectNormalized
    {
        private PointF _LeftBottom;
        private PointF _RightTop;
        private PointF _End;
        public PointF Start { get; set; }
        public PointF End { get => _End; set {
                _End = value;
                if (_End.X > 1) _End.X = 1;
                if (_End.Y > 1) _End.Y = 1;
                _LeftBottom = new PointF(Start.X, _End.Y);
                _RightTop = new PointF(_End.X,  Start.Y );
                _Rectangle = new RectangleF().FromPointsF(_LeftBottom, _RightTop);
            } }
        public PointF LeftTop => Start;
        public PointF LeftBottom => _LeftBottom;
        public PointF RightTop => _RightTop;
        public PointF RightBottom => End;
        private RectangleF _Rectangle = new RectangleF();
        public RectangleF Rectangle { get => _Rectangle; 
                set {
                    Start = value.Location;
                    End = new PointF(value.Right, value.Bottom);
                } }
        public RectNormalized() : this(new PointF(), new PointF()) { }
        public RectNormalized(PointF start, PointF end)
        {
            Start = start;
            End = end;
        }
        public bool Contains(PointF point)
        {
            return Rectangle.Contains(point);
        }
        public void Move(PointF pos)
        {
            RectangleF rect = Rectangle;
            rect.Offset(pos);
            Rectangle = rect;
        }
    }
}
