using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection.Marker
{
    interface IObject
    {
        Rectangle Rect { get; set; }
    }
    class ObjectSelector
    {
        public IObject CreateObject(Rectangle rect)
        {

            return null;
        }
    }
}
