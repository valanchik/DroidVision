using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DroidVision
{
    class Counter
    {
        
        private double avg;
        private double summ;
        public double Avg => avg;
        public double Count => list.Count;
        public double Summ => summ;
        private int maxList = 10;
        private List<double> list = new List<double>();

        public Counter()
        {
        }
        public Counter Add(double val)
        {
            
            list.Add(val);
            summ = 0;
            list.FindAll((e) => {

                summ += e;
                return true;
                });
            avg = summ / Count;
            if (Count>maxList)
            {
                list.RemoveAt(0);
            }
            
            return this;
        }
        public Counter Reset()
        {
            list.Clear();
            avg = 0;
            summ = 0;
            return this;
        }
    }
}
