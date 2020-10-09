using System;
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
        Size<double> ControlsSize { get; set; }
        List<IControlRect> GetControlRects();
        RectNormalizesPointType GetPointType(PointF point);
        void Move(PointF pos);
    }
    public class FrameObject : IFrameObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IRectNormalized Rect { get; set; }
        public Dictionary<RectNormalizesPointType, ControlRect> ControlRects { get; set; } = new Dictionary<RectNormalizesPointType, ControlRect>();
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
        public FrameObject() : this(0, "", new RectNormalized(), Size<double>.Empty) { }
        public FrameObject(RectNormalized rect) : this(0, "", rect, Size<double>.Empty) { }
        public FrameObject(RectNormalized rect, Size<double> controlsSize) : this(0, "", rect, controlsSize) { }
        protected List<IControlRect> ControlRectsList { get; set; } = new List<IControlRect>();
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
            SelectedPointColor = Color.Magenta;
            Selected = false;
            FillRectBrush = new SolidBrush(Color.FromArgb(RectTransparent, BaseColor));
        }
        protected void CreateControlRects(){
            Point<double> offsetPoint = new Point<double>(ControlsSize.Width, ControlsSize.Height);
            ControlRects.Add(RectNormalizesPointType.LeftTopPoint, new ControlRect(this, Rect.LeftTop, ControlsSize));
            ControlRectsList.Add(ControlRects[RectNormalizesPointType.LeftTopPoint]);
            ControlRects.Add(RectNormalizesPointType.LeftBottomPoint, new ControlRect(this,Rect.LeftBottom, ControlsSize));
            ControlRectsList.Add(ControlRects[RectNormalizesPointType.LeftBottomPoint]);
            offsetPoint.X = 0;
            offsetPoint.Y *= -1;
            ControlRects[RectNormalizesPointType.LeftBottomPoint].Move(offsetPoint);
            ControlRects.Add(RectNormalizesPointType.RightBottomPoint, new ControlRect(this, Rect.RightBottom, ControlsSize));
            ControlRectsList.Add(ControlRects[RectNormalizesPointType.RightBottomPoint]);
            offsetPoint.X = ControlsSize.Width*-1;
            ControlRects[RectNormalizesPointType.RightBottomPoint].Move(offsetPoint);
            ControlRects.Add(RectNormalizesPointType.RightTopPoint, new ControlRect(this, Rect.RightTop, ControlsSize));
            ControlRectsList.Add(ControlRects[RectNormalizesPointType.RightTopPoint]);
            offsetPoint.Y = 0;
            ControlRects[RectNormalizesPointType.RightTopPoint].Move(offsetPoint);
        }
        public List<IControlRect> GetControlRects()
        {
            return ControlRectsList;
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
