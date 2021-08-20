using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marker
{
    public struct Rectangle<T> where T : unmanaged
    {
        public Point<T> Location;
        private Size<T> _Size;
        public Size<T> Size { get=>_Size;set {
                _Size = value;
                SetSize();
            } }
        public T Left => Location.X;
        public T Top => Location.Y;
        public T Bottom;
        public T Right;
        public Rectangle(T x, T y, T width, T height) : this(new Point<T> { X = x, Y = y }, new Size<T> { Width = width, Height = height }) { }
        private T x;
        private T y;
        public Rectangle(Point<T> pos, Size<T> size)
        {
            Right = Bottom = default;
            Location = pos;
            x = y = default;
            _Size = size;
            SetSize();
        }
        public bool Contains(Point<T> pos)
        {
            x = (dynamic)pos.X;
            y = (dynamic)pos.Y;

            return x <= (dynamic)Right && x >= (dynamic)Left && y <= (dynamic)Bottom && y >= (dynamic)Top;
        }
        public void Offset(Point<T> pos)
        {
            Size += pos;
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
        public Rectangle UnNormalize(Size window) {
            return new Rectangle(
            new Point((int)((dynamic)Location.X * window.Width), (int)((dynamic)Location.Y * window.Height)),
            new Size((int)((dynamic)Size.Width * window.Width), (int)((dynamic)Size.Height * window.Height))
            );
        }
        
        private void SetSize()
        {
            Right = (dynamic)Location.X + _Size.Width;
            Bottom = (dynamic)Location.Y + _Size.Height;
        }

    }
}
