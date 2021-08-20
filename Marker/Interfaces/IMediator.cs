using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marker.Interfaces
{
    public interface IMediator
    {
        RectController rectController { get; set; }
        IViewBoxControler ViewBoxController { get; set; }
        IFrame GetCurrentFrame();
        IElementController GetElementController(ElementControllerType type);
        IMarker GetMarker();
        void ShowFrame(IFrame frame);
        void ChangePlaySpeed(int spped);
        void AddFrameObjectToCurrentFrame(IFrameObject frameObject);
        void EndEditViewBox();
    }
    public interface IMediatorSetter
    {
        IMediator Mediator { get; set; }
        void SetMediator(IMediator mediator);
    }
}
