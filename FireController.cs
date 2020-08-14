using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection
{
    
    class FireController
    {
        public double Counter => counter;
        private Stopwatch SW = new Stopwatch();
        private Stopwatch SWCounter = new Stopwatch();
        private int counter = 0;
        private double MaxFirePerSecond;
        private double MinMilliseconds;
        public FireController(double maxFirePerSecond)
        {
            SetMaxFirePerSecond(maxFirePerSecond);
        }
        public FireController Fire()
        {
           
            Reset();
            SW.Start();
            if (!SWCounter.IsRunning)
            {
                SWCounter.Start();
                counter++;
            } else if (SWCounter.ElapsedMilliseconds<1000)
            {
                counter++;
            } else
            {
                counter = 1;
                SWCounter.Reset();
            }
            return this;
        }
        public FireController Reset()
        {
            SW.Reset();
            return this;
        }

        public bool IsCanFire()
        {
            
            if (!SW.IsRunning || SW.ElapsedMilliseconds>MinMilliseconds)
            {
                return true;
            } 
            return false;
        }
        public FireController SetMaxFirePerSecond(double value)
        {
            MaxFirePerSecond = value;
            MinMilliseconds = (1 / MaxFirePerSecond) * 1000;
            return this;
        }

    }
}
