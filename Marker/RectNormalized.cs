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
        Point<double> Start { get; set; }
        Point<double> End { get; set; }
        Point<double> LeftTop { get; }
        Point<double> LeftBottom { get;  }
        Point<double> RightTop { get; }
        Point<double> RightBottom { get; }
        Rectangle<double> Rectangle { get; set; }
        bool Contains(Point<double> point);
        void Move(Point<double> pos);
    }
    public class RectNormalized: IRectNormalized
    {
        private Point<double> _LeftBottom;
        private Point<double> _RightTop;
        private Point<double> _End;
        public Point<double> Start { get; set; }
        public Point<double> End { get => _End; set
            {
                _End = value;
                if (_End.X > 1) _End.X = 1;
                if (_End.Y > 1) _End.Y = 1;
                _LeftBottom = new Point<double> { X = Start.X, Y = _End.Y };
                _RightTop = new Point<double> { X = _End.X, Y = Start.Y };
                _Rectangle = Rectangle<double>.FromPoints(_LeftBottom, _RightTop);
            }
        }
        public Point<double> LeftTop => Start;
        public Point<double> LeftBottom => _LeftBottom;
        public Point<double> RightTop => _RightTop;
        public Point<double> RightBottom => End;
        private Rectangle<double> _Rectangle = new Rectangle<double>();
        public Rectangle<double> Rectangle { get => _Rectangle; 
                set {
                    Start = value.Location;
                    End = new Point<double> { X = value.Right, Y = value.Bottom };
                } }
        public RectNormalized(Point<double> pos, Size<double> size) : this(pos, new Point<double>(pos.X + size.Width, pos.Y + size.Height)) { }
        public RectNormalized() : this(new Point<double>(), new Point<double>()) { }
        public RectNormalized(Point<double> start, Point<double> end)
        {
            Start = start;
            End = end;
        }
        public bool Contains(Point<double> point)
        {
            return Rectangle.Contains(point);
        }
        public void Move(Point<double> pos)
        {
            Rectangle<double> rect = Rectangle;
            rect.Offset(pos);
            Rectangle = rect;
        }
    }
}
