﻿using System.Drawing;
using Marker.Interfaces;

namespace Marker
{
    public interface IControlRect
    {
        IRectNormalized Rect { get; set; }
        RectNormalizesPointType Type { get; set; }
        IFrameObject FrameObject { get; set; }
        bool Selected { get; set; }
        string Id { get; set; }

        bool Contains(Point<double> pos);
        void Move(Point<double> pos);
    }
}