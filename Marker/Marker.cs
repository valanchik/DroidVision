using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection.Marker
{
    interface IMarkerData
    {
        
    }
   
    class MarkerFasad
    {
        private static ImageConverter imgConverter = new ImageConverter();
        private static byte[] buffer = new byte[5000000];
        private static int bytesRead;
        private List<IMarkerData> Data = new List<IMarkerData>();
        private string FilePath;

        public MarkerFasad()
        {

        }
        public void Load (string path)
        {
            FilePath = path;
            MJPEGParser mjpegParser = new MJPEGParser();
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
                        IMarkerData d = new MarkerData((Image)imgConverter.ConvertFrom(mjpegParser.GetJPEG().ToArray()));
                        Data.Add(d);
                    }
                }
            }
        }
    }

    struct MarkerData: IMarkerData
    {
        Image Image;

        public MarkerData (Image image)
        {
            Image = image;
        }
    }
}
