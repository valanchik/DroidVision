﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoloDetection.Marker
{
    interface IMarkerFrame
    {
        bool IsEmpty();
        int GetFrameId();
        Image GetImage();
    }
   
    class MarkerFasad
    {
        private  ImageConverter imgConverter = new ImageConverter();
        private List<IMarkerFrame> Data = new List<IMarkerFrame>();
        private string FilePath;
        private PictureBox Window;
        private Timer Timer;
        public int CurrentFrame;

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
                        IMarkerFrame d = new MarkerFrame(
                            (Image)imgConverter.ConvertFrom(mjpegParser.GetJPEG().ToArray()),
                            frameId
                        );
                        Data.Add(d);
                        frameId++;
                    }
                }
            }
        }
        public bool ShowFrame (int frame)
        {
            IMarkerFrame f = GetFrameById(frame);
            if (f != null)
            {
                CurrentFrame = f.GetFrameId();
                Window.Image = f.GetImage();
                return true;
            }
            return false;
        }
        private IMarkerFrame GetFrameById(int frame)
        {
            return Data.Find(o => o.GetFrameId() == frame);
        }
    }
    struct MarkerFrame: IMarkerFrame
    {
        public Image Image;
        public int FrameId;

        public MarkerFrame (Image image, int frameId)
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
