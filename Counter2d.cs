using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Schema;

namespace YoloDetection
{
    class Counter2d
    {
        
        private Vector avg;
        private Vector summ;
        public Vector Avg => avg;
        public double Count => list.Count;
        public Vector Summ => summ;
        private int maxList = 3;
        private List<Vector> list = new List<Vector>();

        public Counter2d()
        {
        }
        public Counter2d Add(Vector val)
        {
            
            list.Add(val);
            summ = new Vector(0,0);
            list.FindAll((e) => {

                summ = Vector.Add(summ, e);
                return true;
             });
            avg = Vector.Divide(summ, Count);
            if (Count>maxList)
            {
                list.RemoveAt(0);
            }
            
            return this;
        }
        public Counter2d Reset()
        {
            list.Clear();
            avg = new Vector(0,0);
            summ = new Vector(0, 0);
            return this;
        }
    }
}
