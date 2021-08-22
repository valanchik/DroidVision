using System.Collections.Generic;
using System.IO;

namespace DroidVision
{
    // ReSharper disable once InconsistentNaming
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
