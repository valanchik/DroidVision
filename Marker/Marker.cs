using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoloDetection.Marker
{
    public enum ElementControllerType
    {
        Common,
        Frame,
        PlaySpeeed,
        Window,
        Timer
    }

    
    public interface IMarker: IMediatorSetter
    {
        IFrame CurrentFrame { get; set; }
        void Load(string path);
        bool ShowBackwardFrame();
        bool ShowForwardFrame();
        bool ShowFrame(int frame);
        bool ShowFrame(IFrame frame);
    }
    public class Marker: IMarker
    {
        private  ImageConverter imgConverter = new ImageConverter();
        private List<IFrame> Data = new List<IFrame>();
        private string FilePath;
        
        public IFrame CurrentFrame { get; set; }

        public IMediator Mediator { get; set; }

        public Marker()
        {
        }
        public void Load(string path)
        {
            FilePath = path;
            MJPEGParser mjpegParser = new MJPEGParser();
            int frameId = 1;
            Data.Clear();
            using (Stream source = File.OpenRead(FilePath))
            {
                byte[] buffer = new byte[2048];
                int bytesRead;
                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    
                    if (!TryGetJPEG(mjpegParser, buffer, bytesRead, out var jpeg)) continue;

                    IFrame d = CreateFrame(jpeg, frameId);

                    Data.Add(d);
                    frameId++;
                }

                if (Data.Count == 0) return;

                TrackBar timeline = Mediator.GetElementController(ElementControllerType.Common).GetTrackBar(ElementName.TimeLineBar);

                timeline.Maximum = Data.Count;

                ShowFrame(Data[0]);
            }
        }
        

        private IFrame CreateFrame(byte[] jpeg, int frameId)
        {
            FrameState state = new FrameState
            {
                States = ((ElementControllerFrame)Mediator.GetElementController(ElementControllerType.Frame)).GetNewDefaultStates()
            };
            state.SetTextState(ElementName.FrameId, frameId.ToString());
            state.SetIntState(ElementName.TimeLineBar, frameId);

            IFrame d = new Frame(
                (Image)imgConverter.ConvertFrom(jpeg),
                frameId,
                state
            );
            return d;
        }

        private bool TryGetJPEG(MJPEGParser mjpegParser, byte[] buffer, int bytesRead, out byte[] jpeg)
        {
            jpeg = null;
            if (bytesRead == buffer.Length)
            {
                mjpegParser.AddBytes(buffer);
            }
            else
            {
                byte[] lastBytes = new byte[bytesRead];
                Array.Copy(buffer, 0, lastBytes, 0, bytesRead);
                mjpegParser.AddBytes(lastBytes);
            }
            if (!mjpegParser.finded) return false;
            jpeg = mjpegParser.GetJPEG().ToArray();
            return true;
        }

        private IFrame GetFrameById(int frame)
        {
            return Data.Find(o => o.FrameId == frame);
        }
        public bool ShowFrame (int frame)
        {
            IFrame f = GetFrameById(frame);
            return ShowFrame(f);
        }
        public bool ShowFrame(IFrame frame)
        {
            if (frame != null)
            {
                Mediator.ShowFrame(frame);
                return true;
            }
            return false;
        }
        public bool ShowForwardFrame()
        {
            IFrame f = GetForwardActiveFrame();
            return ShowFrame(f);
        }
        public bool ShowBackwardFrame()
        {
            IFrame f = GetBackwardActiveFrame();
            return ShowFrame(f);
        }
        private IFrame GetForwardActiveFrame()
        {
            if (CurrentFrame!=null)
            {
                int frame = CurrentFrame.FrameId + 1;
                return Data.Find(o => o.FrameId == frame);
            }
            return null;
        }
        private IFrame GetBackwardActiveFrame()
        {
            if (CurrentFrame != null)
            {
                int frame = CurrentFrame.FrameId - 1;
                return Data.Find(o => o.FrameId == frame);
            }
            return null;
        }

        public void SetMediator(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
    
    
    
}
