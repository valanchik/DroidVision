using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection
{
    class MJPEGWriter
    {
        private string Path;
        private FileStream File;
        private List<byte> Buf = new List<byte>();
        public MJPEGWriter(string path)
        {
            Path = path;
            File = new FileStream(path, FileMode.Create, FileAccess.Write);
        }
        public void Add(byte[] data)
        {
            Buf.AddRange(data);
            Write();
        }

        private void Write()
        {
            byte[] data = Buf.ToArray();
            File.Write(data, 0, data.Length);
            Buf.Clear();
        }
    }
}
