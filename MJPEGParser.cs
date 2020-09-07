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
        public byte[] Example = new byte[] { 0xFD, 0xFF, 0xD8, 0xFF,0xFF,0xFF, 0xFF, 0xD9, 0xFE };
        public bool finded = false;
        private byte[] SOI = new byte[] { 0xFF, 0xD8 };
        private byte[] EOI = new byte[] { 0xFF, 0xD9 };
        private List<byte> jpeg = new List<byte>();
        private int startIndex;
        private int endIndex;
        private int lastIndex;
        private int len = 0;
        private int i = 0;
        private byte first = 0;
        private byte second = 0;
        

        public MJPEGParser()
        {
            
        }

        public MJPEGParser AddBytes(byte[] bytes)
        {
            Data.AddRange(bytes);
            len = Data.Count;
            finded = false;
            for (i = lastIndex; i < len; i++)
            {
                first = Data[i];
                second = len > i + 1? Data[i + 1] : (byte)0;
                if (SOI[0] == first && len>i+1  && SOI[1] == second)
                {
                    startIndex = i;
                }
                if ( EOI[0] == first && len > i + 1 && EOI[1] == second)
                {
                    endIndex = i+1;
                    finded = true;
                    break;
                }
            }
            if (finded)
            {
                jpeg = Data.GetRange(startIndex, endIndex - startIndex + 1);
                Data.RemoveRange(0, endIndex  + 1);
                //startIndex = Data.Count>0 ? Data.Count - 1 : 0;
                if (jpeg[0] == SOI[0] && jpeg[1] == SOI[1])
                {
                    OnJPEG?.Invoke(jpeg.ToArray());
                }
            }
            lastIndex = Data.Count > 0 ? Data.Count - 1 : 0;
            return this;
        }
        public List<byte> GetJPEG()
        {
            return jpeg;
        }
    }
}
