﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DroidVision
{
    public static class Extensions
    {
        public static bool TryAdd<TKey,TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key)) return false;
            dict.Add(key, value);
            return true;
        }
        public static Rectangle FromPoints(this Rectangle rect, Point start, Point end)
        {
            int X = start.X,
                  Y = start.Y,
                Width = Math.Abs(start.X - end.X),
                Height = Math.Abs(start.Y - end.Y);
            if (start.X > end.X) X = end.X;
            if (start.Y > end.Y) Y = end.Y;
            return new Rectangle(X, Y, Width, Height);
        }
        public static Rectangle UnNormalize( this RectangleF rect, Size size)
        {
            return new Rectangle(
                new Point((int)(rect.Location.X * size.Width), (int)(rect.Location.Y * size.Height)),
                new Size((int)(rect.Size.Width*size.Width), (int)(rect.Size.Height*size.Height))
                );
        }
        public static RectangleF FromPointsF( this RectangleF rect, PointF start, PointF end)
        {
            float X = start.X,
                  Y = start.Y,
                Width = Math.Abs(start.X - end.X),
                Height = Math.Abs(start.Y - end.Y);
            if (start.X > end.X) X = end.X;
            if (start.Y > end.Y) Y = end.Y;
            return new RectangleF(X, Y, Width, Height);
        }
        public static void DrawCircle(this Graphics g, Pen pen, Point point, int radius)
        {
            g.DrawEllipse(pen, new RectangleF(new Point(point.X-(radius/2), point.Y-(radius/2)), new Size(radius, radius)));
        }
        public static void FillCircle(this Graphics g, SolidBrush brush, Point point, int radius)
        {
            g.FillEllipse(brush, new RectangleF(new PointF(point.X - (float)(radius / 2), point.Y - (float)(radius / 2)), new Size(radius, radius)));
        }
        public static Point Divide(this Point point, double value)
        {
            return new Point((int)(point.X / value), (int)(point.Y / value));
        }
    }
}
