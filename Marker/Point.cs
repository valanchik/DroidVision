using System;
using System.Drawing;

namespace Marker
{
    public struct Point<T> where T : unmanaged
    {
        public T X { get; set; }
        public T Y { get; set; }
        public Point(T x, T y)
        {
            X = x;
            Y = y;
        }
        public Point(Size<T> size)
        {
            X = size.Width;
            Y = size.Height;
        }
        public static implicit operator Point(Point<T> point)
        {
            return new Point((int)(dynamic)point.X, (int)(dynamic)point.Y);
        }
        public static implicit operator PointF(Point<T> point)
        {
            return new PointF((float)(dynamic)point.X, (float)(dynamic)point.Y);
        }
        public static implicit operator Point<T>(PointF point)
        {
            return new Point<T> { X = (dynamic)point.X, Y = (dynamic)point.Y };
        }
        public static Point<T> operator -(Point<T> left, Point<T> right)
        {
            return new Point<T> { X = (dynamic)left.X - right.X, Y = (dynamic)left.Y - right.Y };
        }
        public static Point<T> operator +(Point<T> left, Point<T> right)
        {
            return new Point<T> { X = (dynamic)left.X + right.X, Y = (dynamic)left.Y + right.Y };
        }
        public static Point<T> operator /(Point<T> left, T right)
        {
            return new Point<T> { X = (dynamic)left.X / right, Y = (dynamic)left.Y /right };
        }
        public static Point<T> operator *(Point<T> left, T right)
        {
            return new Point<T> { X = (dynamic)left.X * right, Y = (dynamic)left.Y * right };
        }
        public static Point<T> Empty { get => new Point<T>(default,default); }
    }
}
