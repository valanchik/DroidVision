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
    interface IMarkerData
    {
        bool IsEmpty();
        int GetFrameId();
        Image GetImage();
    }
   
    class MarkerFasad
    {
        private  ImageConverter imgConverter = new ImageConverter();
        private  byte[] buffer = new byte[5000000];
        private  int bytesRead;
        private List<IMarkerData> Data = new List<IMarkerData>();
        private string FilePath;
        private PictureBox Window;
        public int CurrentFrame;

        public MarkerFasad(PictureBox window)
        {
            Window = window;
        }
        public void Load (string path)
        {
            FilePath = path;
            MJPEGParser mjpegParser = new MJPEGParser();
            int frameId = 1;
            using (Stream source = File.OpenRead(FilePath))
            {
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
                        IMarkerData d = new MarkerData(
                            (Image)imgConverter.ConvertFrom(mjpegParser.GetJPEG().ToArray()),
                            frameId
                        );
                        Data.Add(d);
                        frameId++;
                    }
                }
            }
        }

        public void ShowFrame (int frame)
        {
            IMarkerData f = Data.Find(o=>o.GetFrameId() == frame);
            if (f != null)
            {
                CurrentFrame = f.GetFrameId();
                Window.Image = f.GetImage();
            }
        }
    }
    struct MarkerData: IMarkerData
    {
        public Image Image;
        public int FrameId;

        public MarkerData (Image image, int frameId)
        {
            Image = image;
            FrameId = frameId;
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
    }
}
