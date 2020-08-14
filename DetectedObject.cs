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
        public string Name;
        public Rectangle Rect;
        public Vector Center;
        public Vector Head;
        public double Distance;
        public long Time;
        public Vector offsetVector;
        public static Vector emptyVector = new Vector(0, 0);
        public static DetectedObject Default = new DetectedObject("", new Rectangle(0,0,0,0));
        
        public DetectedObject(string name, Rectangle rect)
        {
            Name = name;
            Rect = rect;
            Center = new Vector(Rect.X+(Rect.Width/2), Rect.Y+(Rect.Height/2));
            Head = new Vector(Center.X, Center.Y - (Rect.Height/2.6F));
            offsetVector = DetectedObject.emptyVector;
        }
        public void SetTime(long time)
        {
            Time = time;
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
        private Vector CenterDetectionSize;
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
                if (o.Rect.Height> DetectionSize.Y)
                {
                    o.Rect.Height = (int)DetectionSize.Y;
                }
                if (o.Rect.Width > DetectionSize.X)
                {
                    o.Rect.Width = (int)DetectionSize.X;
                }
            }
            Data = obj.OrderBy(o => o.Distance).ToList();
        }
        public DetectedObject GetNearestRectFromCenter()
        {
            if (Data.Count>0)
            {
                Data[0].offsetVector = GetFromCenter(Data[0].Head);
                return Data[0];
            }
            return null;
        }
        public Vector GetFromCenter(Vector vect)
        {
            return Vector.Subtract(ConvertDetectionScreenSizeToScreeSize(vect), CenterScreen);
        }
        private Vector ConvertDetectionScreenSizeToScreeSize(Vector detectionSizeVector)
        {
            return new Vector(detectionSizeVector.X*ScreenCoef.X, detectionSizeVector.Y* ScreenCoef.Y);
        } 
        public bool IsObjectCrossCenter(DetectedObject obj)
        {
            Rectangle rect = obj.Rect;
/*            rect.X *= (int)ScreenCoef.X;
            rect.Width *= (int)ScreenCoef.X;
            rect.Y *= (int)ScreenCoef.Y;
            rect.Height *= (int)ScreenCoef.Y;*/
            if ((rect.X< CenterDetectionSize.X && (rect.X+ rect.Width) > CenterDetectionSize.X) 
                &&
                (rect.Y < CenterDetectionSize.Y && (rect.Y + rect.Height) > CenterDetectionSize.Y))
            {
                return true;
            }
            return false;
        }
        public void Clear()
        {
            Data.Clear();
        }
        public double GetDistance(Vector vec1, Vector vec2)
        {
            return Vector.Subtract(vec2, vec1).Length; ;
        }
        public Vector Predict(Vector from, double scalar)
        {
            //Vector v = Vector.Subtract(to.offsetVector, from.offsetVector);
            /*Vector v = new Vector(from.X, from.Y);
            v.Normalize();*/
            return Vector.Add(from, Vector.Multiply(from, scalar));
        }
        private void SetSizes(Vector screen, Vector detectionSize)
        {
            Screen = screen;
            DetectionSize = detectionSize;
            ScreenCoef = new Vector(Screen.X / DetectionSize.X, Screen.Y / DetectionSize.Y);
            CenterScreen = new Vector(Screen.X * Center.X, Screen.Y * Center.Y);
            CenterDetectionSize = new Vector(detectionSize.X * Center.X, detectionSize.Y * Center.Y);
        }
    }

}
