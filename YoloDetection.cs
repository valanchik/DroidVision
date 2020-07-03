using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Darknet;
using static Darknet.YoloWrapper;
namespace YoloDetection
{
    class YoloDetection
    {
        public delegate void OnDetectedObject(DetectedObject obj);
        public static OnDetectedObject OnObject;
        public delegate void OnRunTime(long timeMillisect);
        public static OnRunTime OnYoloDetect;
        public static byte[] lastImg = new byte[0];
        Thread receiveThread;
        public YoloDetection()
        {
            receiveThread = new Thread(new ThreadStart(YOLOThread));
            receiveThread.Priority = ThreadPriority.Highest;
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }

        private static void YOLOThread()
        {
            YoloWrapper yolo = new YoloWrapper("yolo-obj.cfg", "yolo-obj_best.weights", 0);
            bool flag = true;
            uint[] obj_ids = { 0};
            //uint[] obj_ids = { 0, 2 };

            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Interval = 1;

            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += (Object source, System.Timers.ElapsedEventArgs e) =>
            {
                if (flag && lastImg.Length > 0)
                {
                    flag = !flag;
                    Stopwatch SW = new Stopwatch(); // Создаем объект
                    SW.Start(); // Запускаем
                   
                    byte[] img = lastImg;
                    try
                    {
                        bbox_t[] items = yolo.Detect(img);
                        foreach (bbox_t box in items)
                        {
                            if (box.prob > 0.5 && Array.IndexOf(obj_ids, box.obj_id) > -1)
                            {
                                OnObject?.Invoke(new DetectedObject(box.obj_id.ToString(), new Rectangle((int)box.x, (int)box.y, (int)box.w, (int)box.h)));
                            }
                        }
                        SW.Stop();
                        OnYoloDetect?.Invoke(SW.ElapsedMilliseconds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    
                    flag = !flag;
                }
            };


            // Have the timer fire repeated events (true is the default)
            aTimer.AutoReset = true;

            // Start the timer
            aTimer.Enabled = true;
        }
    }
}
