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
using static Darknet.YoloWrapperTiny;
namespace YoloDetection
{
    class YoloDetection
    {
        public delegate void OnDetectedObject(DetectedObject obj);
        public static OnDetectedObject OnObject;
        public static OnDetectedObject OnTinyObject;
        public delegate void OnRunTime(long timeMillisect);
        public static OnRunTime OnYoloDetect;
        public static OnRunTime OnYoloTinyDetect;
        public static byte[] emptyBytes = new byte[0];
        public static byte[] lastImg = emptyBytes;
        public static byte[] lastImgTiny = emptyBytes;
        Thread receiveThread;
        Thread TinyreceiveThread;
        public YoloDetection()
        {
            receiveThread = new Thread(new ThreadStart(YOLOThread));
            receiveThread.Priority = ThreadPriority.Highest;
            receiveThread.IsBackground = true;
            receiveThread.Start();

            TinyreceiveThread = new Thread(new ThreadStart(YOLOTinyThread));
            TinyreceiveThread.Priority = ThreadPriority.Highest;
            TinyreceiveThread.IsBackground = true;
            TinyreceiveThread.Start();
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
                        YoloWrapper.bbox_t[] items = yolo.Detect(img);
                        foreach (YoloWrapper.bbox_t box in items)
                        {
                            if (box.prob > 0.5 && Array.IndexOf(obj_ids, box.obj_id) > -1)
                            {
                                OnObject?.Invoke(new DetectedObject(box.obj_id.ToString(), new Rectangle((int)box.x, (int)box.y, (int)box.w, (int)box.h)));
                            }
                        }
                        SW.Stop();
                        OnYoloDetect?.Invoke(SW.ElapsedMilliseconds);
                        lastImg = emptyBytes;
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
        private static void YOLOTinyThread()
        {
            YoloWrapperTiny yolo = new YoloWrapperTiny("yolo-tiny.cfg", "yolo-tiny_best.weights", 0);
            bool flag = true;
            uint[] obj_ids = { 0 };
            //uint[] obj_ids = { 0, 2 };

            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Interval = 1;

            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += (Object source, System.Timers.ElapsedEventArgs e) =>
            {
                if (flag && lastImgTiny.Length > 0)
                {
                    flag = !flag;
                    Stopwatch SW = new Stopwatch(); // Создаем объект
                    SW.Start(); // Запускаем

                    byte[] img = lastImgTiny;
                    try
                    {
                        YoloWrapperTiny.bbox_t[] items = yolo.Detect(img);
                        foreach (YoloWrapperTiny.bbox_t box in items)
                        {
                            if (box.prob > 0.4 && box.obj_id == 0)
                            {
                                OnTinyObject?.Invoke(new DetectedObject(box.obj_id.ToString(), new Rectangle((int)box.x, (int)box.y, (int)box.w, (int)box.h)));
                            }
                        }
                        SW.Stop();
                        OnYoloTinyDetect?.Invoke(SW.ElapsedMilliseconds);
                        
                        lastImgTiny = emptyBytes;
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
