using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marker.Interfaces;
namespace Marker
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
    public class FrameObject : IFrameObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IRectNormalized Rect { get; set; }
        public Dictionary<RectNormalizesPointType, IControlRect> ControlRects { get; set; } = new Dictionary<RectNormalizesPointType, IControlRect>();
        public Size<double> ControlsSize { get; set; }
        public Color BaseColor { get; set; }
        public Color BorderColor { get; set; }
        public Color SelectedBorderColor { get; set; }
        public Color PointColor { get; set; }
        public Color SelectedPointColor { get; set; }
        public SolidBrush FillRectBrush { get; set; }
        public byte RectTransparent { get; set; }
        public byte BorderTransparent { get; set; }
        public bool Selected { get; set; }
        protected List<IControlRect> ControlRectsList { get; set; } = new List<IControlRect>();
        public FrameObject() : this(0, "", new RectNormalized(), Size<double>.Empty) { }
        public FrameObject(RectNormalized rect) : this(0, "", rect, Size<double>.Empty) { }
        public FrameObject(RectNormalized rect, Size<double> controlsSize) : this(0, "", rect, controlsSize) { }
        public FrameObject(int id, string name, RectNormalized rect, Size<double> controlsSize)
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
            SelectedPointColor = Color.Black;
            Selected = false;
            FillRectBrush = new SolidBrush(Color.FromArgb(RectTransparent, BaseColor));
        }
        protected void CreateControlRects(){
            Point<double> offsetPoint = new Point<double>(ControlsSize.Width, ControlsSize.Height);
            ControlRects.Add(RectNormalizesPointType.LeftTopPoint, new ControlRect(this, RectNormalizesPointType.LeftTopPoint, Rect.LeftTop, ControlsSize));
            ControlRectsList.Add(ControlRects[RectNormalizesPointType.LeftTopPoint]);
            ControlRects.Add(RectNormalizesPointType.LeftBottomPoint, new ControlRect(this, RectNormalizesPointType.LeftBottomPoint, Rect.LeftBottom, ControlsSize));
            ControlRectsList.Add(ControlRects[RectNormalizesPointType.LeftBottomPoint]);
            offsetPoint.X = 0;
            offsetPoint.Y *= -1;
            ControlRects[RectNormalizesPointType.LeftBottomPoint].Move(offsetPoint);
            ControlRects.Add(RectNormalizesPointType.RightBottomPoint, new ControlRect(this, RectNormalizesPointType.RightBottomPoint, Rect.RightBottom, ControlsSize));
            ControlRectsList.Add(ControlRects[RectNormalizesPointType.RightBottomPoint]);
            offsetPoint.X = ControlsSize.Width * -1;
            ControlRects[RectNormalizesPointType.RightBottomPoint].Move(offsetPoint);
            ControlRects.Add(RectNormalizesPointType.RightTopPoint, new ControlRect(this, RectNormalizesPointType.RightTopPoint, Rect.RightTop, ControlsSize));
            ControlRectsList.Add(ControlRects[RectNormalizesPointType.RightTopPoint]);
            offsetPoint.Y = 0;
            ControlRects[RectNormalizesPointType.RightTopPoint].Move(offsetPoint);
        }
        public List<IControlRect> GetControlRects()
        {
            return ControlRectsList;
        }
        public void Move(Point<double> pos)
        {
            Rect.Move(pos);
            foreach (var elm in ControlRects)
            {
                ControlRects[elm.Key].Move(pos);
            }
        }
        public void Edit(RectNormalizesPointType type, Point<double> pos)
        {
            switch (type)
            {
                case RectNormalizesPointType.LeftTopPoint:
                    Rect.LeftTop += pos;
                    ControlRects[type].Move(pos);
                    ControlRects[RectNormalizesPointType.LeftBottomPoint].Move(new Point<double>(pos.X, 0));
                    ControlRects[RectNormalizesPointType.RightTopPoint].Move(new Point<double>(0, pos.Y));
                    break;
                case RectNormalizesPointType.LeftBottomPoint:
                    Rect.LeftBottom += pos;
                    ControlRects[type].Move(pos);
                    ControlRects[RectNormalizesPointType.LeftTopPoint].Move(new Point<double>(pos.X, 0));
                    ControlRects[RectNormalizesPointType.RightBottomPoint].Move(new Point<double>(0, pos.Y));
                    break;
                case RectNormalizesPointType.RightTopPoint:
                    Rect.RightTop += pos;
                    ControlRects[type].Move(pos);
                    ControlRects[RectNormalizesPointType.LeftTopPoint].Move(new Point<double>(0, pos.Y));
                    ControlRects[RectNormalizesPointType.RightBottomPoint].Move(new Point<double>(pos.X, 0));
                    break;
                case RectNormalizesPointType.RightBottomPoint:
                    Rect.RightBottom += pos;
                    ControlRects[type].Move(pos);
                    ControlRects[RectNormalizesPointType.LeftBottomPoint].Move(new Point<double>(0, pos.Y));
                    ControlRects[RectNormalizesPointType.RightTopPoint].Move(new Point<double>(pos.X, 0));
                    break;
            }
        }
    }
}
