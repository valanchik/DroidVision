using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marker.Interfaces
{
    public interface IViewBoxControler : IMediatorSetter
    {
        bool ImageNotExists { get; }
        Size Size { get; set; }
        double ImageScale { get; set; }
        PictureBox PictureBox { get; set; }
        IRectController RectController { get; set; }
        Image Image { get; set; }
        Size ImageSize { get; set; }
        bool Moving { get; set; }
        Point Location { get; set; }
        Point MousePosition { get; set; }
        void SetImage(Image image);
        void Clear();
        void Refresh();
        Rectangle GetImageRectangle();
        void StartMoving(Point point);
        void Resize();
        void Resize(Point mousePosition);
        Label MousePositionLabel { get; }
    }
}
