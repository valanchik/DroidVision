using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection.Marker.Interfaces
{
    public interface IFrameObject
    {
        int Id { get; set; }
        string Name { get; set; }
        IRectNormalized Rect { get; set; }
        SolidBrush FillRectBrush { get; set; }
        Color BaseColor { get; set; }
        Color BorderColor { get; set; }
        byte RectTransparent { get; set; }
        byte BorderTransparent { get; set; }
        bool Selected { get; set; }
        Color SelectedPointColor { get; set; }
        Color PointColor { get; set; }
        Color SelectedBorderColor { get; set; }
        Dictionary<RectNormalizesPointType, IControlRect> ControlRects { get; set; }
        Size<double> ControlsSize { get; set; }

        void Edit(RectNormalizesPointType type, Point<double> pos);
        List<IControlRect> GetControlRects();
        void Move(Point<double> pos);
    }
}
