using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marker
{
    public struct Size<T> where T : unmanaged
    {
        public T Width { get; set; }
        public T Height { get; set; }
        public static Size<T> Empty { get => new Size<T>(default, default); }
        public Size(Point<T> point) : this(point.X, point.Y) { }
        public Size(T width, T height)
        {
            Width = width;
            Height = height;
        }

        public static implicit operator Size(Size<T> point)
        {
            return new Size((int)(dynamic)point.Width, (int)(dynamic)point.Height);
        }
        public static implicit operator SizeF(Size<T> point)
        {
            return new SizeF((float)(dynamic)point.Width, (float)(dynamic)point.Height);
        }
        public static Size<T> operator -(Size<T> left, Size<T> right)
        {
            return new Size<T> { Width = (dynamic)left.Width - right.Width, Height = (dynamic)left.Height - right.Height };
        }
        public static Size<T> operator -(Size<T> left, Point<T> right)
        {
            return new Size<T> { Width = (dynamic)left.Width - right.X, Height = (dynamic)left.Height - right.Y };
        }
        public static Size<T> operator +(Size<T> left, Size<T> right)
        {
            return new Size<T> { Width = (dynamic)left.Width + right.Width, Height = (dynamic)left.Height + right.Height };
        }
        public static Size<T> operator +(Size<T> left, Point<T> right)
        {
            return new Size<T> { Width = (dynamic)left.Width + right.X, Height = (dynamic)left.Height + right.Y };
        }
        public static Size<T> operator /(Size<T> left, T right)
        {
            return new Size<T> { Width = (dynamic)left.Width / right, Height = (dynamic)left.Height / right };
        }
        public static Size<T> operator /( T left, Size<T> right)
        {
            return new Size<T> { Width = (dynamic)left / right.Width, Height = (dynamic)left / right.Height };
        }
    }
}
