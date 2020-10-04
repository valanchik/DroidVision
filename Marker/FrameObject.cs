﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace YoloDetection.Marker
{
    public enum RectNormalizesPointType
    {
        Rect,
        LeftTopPoint,
        LeftBottomPoint,
        RightTopPoint,
        RightBottomPoint,
        LeftSide,
        RightSide,
        TopSide,
        BottomSide,
    }
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
        Dictionary<RectNormalizesPointType, ControlRect> ControlRects { get; set;}
        SizeF ControlsSize { get; set; }
        RectNormalizesPointType GetPointType(PointF point);
        void Move(PointF pos);
    }
    public class FrameObject : IFrameObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IRectNormalized Rect { get; set; }
        public Dictionary<RectNormalizesPointType, ControlRect> ControlRects { get; set; } = new Dictionary<RectNormalizesPointType, ControlRect>();
        public SizeF ControlsSize { get; set; }
        public Color BaseColor { get; set; }
        public Color BorderColor { get; set; }
        public Color SelectedBorderColor { get; set; }
        public Color PointColor { get; set; }
        public Color SelectedPointColor { get; set; }
        public SolidBrush FillRectBrush { get; set; }
        public byte RectTransparent { get; set; }
        public byte BorderTransparent { get; set; }
        public bool Selected { get; set; }
        private IRectNormalized PRect { get; set; }
        public FrameObject() : this(0, "", new RectNormalized(), SizeF.Empty) { }
        public FrameObject(RectNormalized rect) : this(0, "", rect, SizeF.Empty) { }
        public FrameObject(RectNormalized rect, SizeF controlsSize) : this(0, "", rect, controlsSize) { }
        public FrameObject(int id, string name, RectNormalized rect, SizeF controlsSize)
        {
            Id = id;
            Name = name;
            Rect = rect;
            ControlsSize = controlsSize;
            CreateControlRects();
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
        protected void CreateControlRects(){
            PointF offsetPoint = new PointF((ControlsSize.Width / 2) * -1, (ControlsSize.Height / 2) * -1);
            ControlRects.Add(RectNormalizesPointType.LeftTopPoint, new ControlRect(Rect.LeftTop, ControlsSize));
            ControlRects[RectNormalizesPointType.LeftTopPoint].Move(offsetPoint);
            ControlRects.Add(RectNormalizesPointType.LeftBottomPoint, new ControlRect(Rect.LeftBottom, ControlsSize));
            offsetPoint.Y *= -1;
            ControlRects[RectNormalizesPointType.LeftBottomPoint].Move(offsetPoint);
            ControlRects.Add(RectNormalizesPointType.RightBottomPoint, new ControlRect(Rect.RightBottom, ControlsSize));
            offsetPoint.X *= -1;
            ControlRects[RectNormalizesPointType.RightBottomPoint].Move(offsetPoint);
            ControlRects.Add(RectNormalizesPointType.RightTopPoint, new ControlRect(Rect.RightTop, ControlsSize));
            offsetPoint.Y *= -1;
            ControlRects[RectNormalizesPointType.RightTopPoint].Move(offsetPoint);
        }
        public RectNormalizesPointType GetPointType(PointF point)
        {
            foreach(var elm in ControlRects)
            {
                if (elm.Value.Contains(point))
                {
                    return elm.Key;
                }
            }
            return RectNormalizesPointType.Rect;
        }
        public void Move(PointF pos)
        {
            Rect.Move(pos);
            foreach (var elm in ControlRects)
            {
                ControlRects[elm.Key].Move(pos);
            }
        }
    }
}
