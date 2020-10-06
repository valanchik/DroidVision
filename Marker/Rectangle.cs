using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection.Marker
{
    public struct Rectangle<T> where T : unmanaged
    {
        public Point<T> Location;
        public Size<T> Size;
        public T Left => Location.X;
        public T Top => Location.Y;
        public T Bottom;
        public T Right;
        public Rectangle(T x, T y, T width, T height) : this(new Point<T> { X = x, Y = y}, new Size<T> {Width = width, Height = height }) { }
        public Rectangle(Point<T> pos, Size<T> size) {
            Location = pos;
            Size = size;
            Right = (dynamic)Location.X + Size.Width;
            Bottom = (dynamic)Location.Y + Size.Height;            
        }
        public bool Contains(Point<T> pos)
        {
            return false;
        }
        public void Offset(Point<T> pos)
        {
            Location += pos;
        }
        public static implicit operator Rectangle(Rectangle<T> rect)
        {
            return new Rectangle(rect.Location, rect.Size);
        }
        public static implicit operator RectangleF(Rectangle<T> rect)
        {
            return new RectangleF(rect.Location, rect.Size);
        }
        public static Rectangle<T> FromPoints(Point<T> leftTop, Point<T> rightBottom)
        {
            T X = leftTop.X,
                  Y = leftTop.Y,
                Width = Math.Abs((dynamic)leftTop.X - rightBottom.X),
                Height = Math.Abs((dynamic)leftTop.Y - rightBottom.Y);
            if ((dynamic)leftTop.X > rightBottom.X) X = rightBottom.X;
            if ((dynamic)leftTop.Y > rightBottom.Y) Y = rightBottom.Y;
            return new Rectangle<T>(X, Y, Width, Height);
        }
    }
}
