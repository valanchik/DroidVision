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
    class MarkerFasad
    {
        private  ImageConverter imgConverter = new ImageConverter();
        private List<IFrame> Data = new List<IFrame>();
        private string FilePath;
        private PictureBox Window;
        private Timer Timer;
        private ElementController ElementController;
        public IFrame CurrentFrame;
        public MarkerFasad(PictureBox window, Timer timer, ElementController elementController)
        {
            Window = window;
            Timer = timer;
            ElementController = elementController;
            ElementController.SetMarker(this);
        }
        public void Load (string path)
        {
            FilePath = path;
            MJPEGParser mjpegParser = new MJPEGParser();
            int frameId = 1;
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

                TrackBar timeline = ElementController.GetTrackBar(StateElementName.TimeLineBar);

                timeline.Maximum = Data.Count;

                ShowFrame(Data[0]);
            }
        }

        private IFrame CreateFrame(byte[] jpeg, int frameId)
        {
            FrameState state = new FrameState();

            state.States = ElementController.GetNewDefaultStates();
            state.SetTextState(StateElementName.FrameId, frameId.ToString());
            state.SetIntState(StateElementName.TimeLineBar, frameId);

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
                CurrentFrame = frame;
                Window.Image = frame.Image;
                ElementController.SetFrameState(frame.State);
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
    }
    
    
    
}
