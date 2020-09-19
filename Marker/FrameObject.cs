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
        RectNormalized Rect { get; set; }
    }
    public struct FrameObject: IFrameObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RectNormalized Rect { get; set; }
        public FrameObject(int id, string name, RectNormalized rect)
        {
            Id = id;
            Name = name;
            Rect = rect;
        }
    }
}
