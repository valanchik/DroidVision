using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace YoloDetection
{
    class DetectedObject
    {
        private string Name;
        private Rectangle Rect;
        public Vector Center;
        public Vector Head;
        public double Distance;
        public long Time;
        public Vector offsetVector;
        public static Vector emptyVector = new Vector(0, 0);
        public static DetectedObject Default = new DetectedObject("", new Rectangle(0,0,0,0));
        
        public DetectedObject(string name, Rectangle rect)
        {
            Time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            Name = name;
            Rect = rect;
            Center = new Vector(Rect.X+(Rect.Width/2), Rect.Y+(Rect.Height/2));
            Head = new Vector(Center.X, Center.Y - (Rect.Height/2.5));
            offsetVector = DetectedObject.emptyVector;
        }

        public bool IsActualTime(long t)
        {
            long now  = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            return now - t < Time;
        }

        public void DrawRect(PaintEventArgs e, Pen pen) => e.Graphics.DrawRectangle(pen, Rect);
        public void DrawCircle(PaintEventArgs e, Pen pen, Vector vect) => e.Graphics.DrawEllipse(pen, new Rectangle(new System.Drawing.Point((int)vect.X, (int)vect.Y), new System.Drawing.Size(3,3)));

    }
    class DetectedObjectController
    {
        Vector Screen;
        Vector DetectionSize;
        Vector ScreenCoef;
        private Vector Center = new Vector(0.5F, 0.5F);
        private Vector CenterScreen;
        private List<DetectedObject> Data = new List<DetectedObject>();
        public DetectedObjectController(Vector screen, Vector detectionSize)
        {
            SetSizes(screen, detectionSize);
        }
        public DetectedObjectController(Vector screen, Vector detectionSize, List<DetectedObject> data)
        {
            SetSizes(screen, detectionSize);
            SetData(data);
        }
        void SetData(List<DetectedObject> obj)
        {           
            foreach (DetectedObject o in obj)
            {
                o.Distance = GetDistance(CenterScreen, o.Center);
            }
            Data = obj.OrderBy(o => o.Distance).ToList();
        }
        public DetectedObject GetNearestRectFromCenter()
        {
            if (Data.Count>0)
            {
                Data[0].offsetVector = Vector.Subtract(Data[0].Head, CenterScreen);
                return Data[0];
            }
            return null;
        }
        public void Clear()
        {
            Data.Clear();
        }
        private double GetDistance(Vector vec1, Vector vec2)
        {
            return Math.Sqrt(Math.Pow(vec1.X - vec2.X, 2) + Math.Pow(vec1.Y - vec2.Y, 2));
        }
        private void SetSizes(Vector screen, Vector detectionSize)
        {
            Screen = screen;
            DetectionSize = detectionSize;
            ScreenCoef = new Vector(Screen.X / DetectionSize.X, Screen.Y / DetectionSize.Y);
            CenterScreen = new Vector(Screen.X * Center.X, Screen.Y * Center.Y);
        }
    }

}
