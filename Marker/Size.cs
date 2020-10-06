using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection.Marker
{
    public struct Size<T> where T : unmanaged
    {
        public T Width { get; set; }
        public T Height { get; set; }

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
            return new Size<T> { Width = (dynamic)left.Width - right.Width, Height = (dynamic)left.Height - right.Width };
        }
        public static Size<T> operator /(Size<T> left, T right)
        {
            return new Size<T> { Width = (dynamic)left.Width / right, Height = (dynamic)left.Height / right };
        }
    }
}
