using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection.Marker
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
    }
    public struct FrameObject: IFrameObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IRectNormalized Rect { get; set; }
        public Color BaseColor { get; set; }
        public Color BorderColor { get; set; }
        public Color SelectedBorderColor { get; set; }
        public Color PointColor { get; set; }
        public Color SelectedPointColor { get; set; }
        public SolidBrush FillRectBrush { get; set; }
        public byte RectTransparent { get; set; }
        public byte BorderTransparent { get; set; }
        public bool Selected { get; set; }
        public FrameObject(int id, string name, RectNormalized rect)
        {
            Id = id;
            Name = name;
            Rect = rect;
            RectTransparent = 0;
            BorderTransparent = 0;
            BaseColor = Color.Red;
            BorderColor = Color.FromArgb(BorderTransparent, BaseColor);
            SelectedBorderColor = Color.Magenta;
            PointColor = Color.White;
            SelectedPointColor = Color.Magenta;
            Selected = false;
            FillRectBrush = new SolidBrush(Color.FromArgb(RectTransparent, BaseColor));

        }
    }
}
