using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection
{
    
    class MJPEGParser
    {
        public List<byte> Data = new List<byte>();
        public delegate void OnBytes(byte[] jpeg);
        public OnBytes OnJPEG;
        private byte[] SOI = new byte[] { 0xFF, 0xD8 };
        private byte[] EOI = new byte[] { 0xFF, 0xD9 };
        private int startIndex;
        private int endIndex;
        private bool started = false;
        private int pos = 0;
        
        public MJPEGParser()
        {
            
        }

        public void addBytes(byte[] bytes)
        {
            int len = bytes.Length+pos;
            for (int i = pos; i < len; i++)
            {
                if (SOI[0] == bytes[i-pos] && len>i+1  && SOI[1] == bytes[i-pos+1])
                {
                    startIndex = i;
                }
                if ( EOI[0] == bytes[i-pos] && len > i + 1 && EOI[1] == bytes[i-pos + 1])
                {
                    endIndex = i+1;
                    Data.Add(bytes[i - pos]);
                    Data.Add(bytes[i-pos+1]);
                    List<byte> jpeg = Data.GetRange(startIndex, endIndex - startIndex+1);
                    Data.RemoveRange(0, endIndex - startIndex + 1);
                    pos -= endIndex - startIndex + 1;
                    if (jpeg[0] == SOI[0] && jpeg[1] == SOI[1])
                    {
                        OnJPEG?.Invoke(jpeg.ToArray());
                    }
                    
                    i++;
                    continue;
                }
                Data.Add(bytes[i-pos]);
            }
            pos = Data.Count();
        }
    }
}
