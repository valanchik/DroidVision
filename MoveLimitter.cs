using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection
{
    class MoveLimitter
    {
        private Stopwatch SW = new Stopwatch();
        private double MinMilliseconds;
        public MoveLimitter(double MinMilliseconds)
        {
            SetMinMilliseconds(MinMilliseconds);
        }
        public MoveLimitter Move()
        {
            Reset();
            SW.Start();

            return this;
        }
        public MoveLimitter Reset()
        {
            SW.Reset();
            return this;
        }

        public bool IsCanMove()
        {

            if (!SW.IsRunning || SW.ElapsedMilliseconds > MinMilliseconds)
            {
                return true;
            }
            return false;
        }
        public MoveLimitter SetMinMilliseconds(double value)
        {
            MinMilliseconds = value;
            return this;
        }

    }
}
