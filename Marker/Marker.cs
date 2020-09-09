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
        public int CurrentFrame;
        public ElementController ElementController;
        public MarkerFasad(PictureBox window, Timer timer)
        {
            Window = window;
            Timer = timer;
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
                    if (bytesRead == buffer.Length)
                    {
                        mjpegParser.AddBytes(buffer);
                    } else
                    {
                        byte[] lastBytes = new byte[bytesRead];
                        Array.Copy(buffer, 0, lastBytes, 0, bytesRead);
                        mjpegParser.AddBytes(lastBytes);
                    }
                    if (mjpegParser.finded)
                    {
                        IFrame d = new Frame(
                            (Image)imgConverter.ConvertFrom(mjpegParser.GetJPEG().ToArray()),
                            frameId,
                            new FrameState()
                        );
                        Data.Add(d);
                        frameId++;
                    }
                }
            }
        }
        private IFrame GetFrameById(int frame)
        {
            return Data.Find(o => o.GetFrameId() == frame);
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
                CurrentFrame = frame.GetFrameId();
                Window.Image = frame.GetImage();
                ElementController.SetState(frame.GetState());
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
            int frame = CurrentFrame + 1;
            return Data.Find(o => o.GetFrameId() == frame);
        }
        private IFrame GetBackwardActiveFrame()
        {
            int frame = CurrentFrame - 1;
            return Data.Find(o => o.GetFrameId() == frame);
        }
    }
    
    struct ElementController
    {
        CheckBox FrameInBookmarks;
        CheckBox FrameHided;
        CheckBox FrameRemoved;
        public ElementController(CheckBox frameInBookmarks, CheckBox frameHided, CheckBox frameRemoved)
        {
            FrameInBookmarks = frameInBookmarks;
            FrameHided = frameHided;
            FrameRemoved = frameRemoved;
        }
        public void SetState(FrameState state)
        {
            FrameInBookmarks.Checked = state.Inbookmarks;
            FrameHided.Checked = state.Hided;
            FrameRemoved.Checked = state.Removed;
        }
    }
    interface IFrame
    {
        bool IsEmpty();
        int GetFrameId();
        Image GetImage();
        FrameState GetState();
    }
    struct FrameState
    {
        public bool Inbookmarks;
        public bool Hided;
        public bool Removed;
    }
    struct Frame: IFrame
    {
        private Image Image;
        private int FrameId;
        private FrameState State;

        public Frame (Image image, int frameId, FrameState state)
        {
            Image = image;
            FrameId = frameId;
            State = state;
        }
        public bool IsEmpty()
        {
            return false;
        }
        public int GetFrameId()
        {
            return FrameId;
        }
        public Image GetImage()
        {
            return Image;
        }

        public FrameState GetState()
        {
            return State;
        }
    }
}
