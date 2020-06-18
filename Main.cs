using System;
using System.Activities;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Darknet;
using static Darknet.YoloWrapper;

namespace YoloDetection
{
    public partial class Main : Form
    {
        static ConcurrentBag<DetectedObject> detected = new ConcurrentBag<DetectedObject>();
        readonly UDPServer server;
        readonly udpKeyServer keyServer;
        Stopwatch SW;
        public static ImageConverter imgConverter = new ImageConverter();
        public static ImageConverter imgConverterToBytes = new ImageConverter();
        private static YoloDetection YoloDetection;
        private List<DetectedObject> paintObjects = new List<DetectedObject>();
        private DetectedObject lastObj = null;
        private long timeDetection = 0;
        IGameController gameController;
        private bool AutoMoving = false;
        private bool sendedToUDP = true;
        private static bool IsRender = true;
        private static Image imgConverted;
        private static Image imgCroped;
        private static Image imgCloned;
        private static byte[] bImg;
        private static  Bitmap bmpImage;
        private static Stopwatch SWController = new Stopwatch();

        public Main()
        {

            InitializeComponent();

            
            // arduino controller
            gameController = new UDPGameController("192.168.88.177", 8888);

            UDPGameController.OnReceive += (string message) =>
            {
                sendedToUDP = true;
                timeController.Invoke(new Action(() => timeController.Text = SWController.ElapsedMilliseconds.ToString() + " ms"));
            };

            server = new UDPServer();
            keyServer = new udpKeyServer(8889);
            udpKeyServer.OnKeyMoving += (bool status) =>
             {
                 if (status)
                 {
                     AutoMoving = !AutoMoving;
                 }
                 
             };

            UDPServer.mjpegParser.OnJPEG += (byte[] jpeg) =>
            {
                if (IsRender)
                {
                    IsRender = !IsRender;

                    YoloDetection.lastImg = jpeg;
                    pictureBox1.Image = (Image)imgConverter.ConvertFrom(jpeg);

                    /*imgConverted = (Image)imgConverter.ConvertFrom(jpeg);
                    imgCroped = cropImage(imgConverted, new Rectangle(0, 0, 320, 320));
                    imgCloned = (Image)imgCroped.Clone();
                    pictureBox1.Image = imgCroped;
                    bImg = (byte[])imgConverterToBytes.ConvertTo(imgCloned, typeof(byte[]));
                    YoloDetection.lastImg = bImg;*/
                    IsRender = !IsRender;
                }
            };
            YoloDetection.OnYoloDetect += (long time) =>
            {
                timeDetection = time;
                YoloRunTimeValue.Invoke(new Action(() => YoloRunTimeValue.Text = time.ToString() + " ms"));
            };
            YoloDetection.OnObject += (DetectedObject obj) =>
            {
                detected.Add(obj);
            };
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (AutoMoving)
            {
                autoMovingStatus.BackColor = Color.Green;
            } else
            {
                autoMovingStatus.BackColor = Color.Red;
            }
            /*Stopwatch SW = new Stopwatch(); // Создаем объект
            SW.Start(); // Запускаем*/

            List<DetectedObject> obj = new List<DetectedObject>();
            using (Pen pen = new Pen(Color.Red, 2))
            {
                while (!detected.IsEmpty)
                {
                    DetectedObject item;
                    if (detected.TryTake(out item))
                    {
                        item.DrawRect(e, pen);
                        obj.Add(item);
                    }
                }
            }
            DetectedObjectController objects = new DetectedObjectController(new Vector(852, 480), new Vector(852, 480), obj);
            DetectedObject o = objects.GetNearestRectFromCenter();
            
            if (o != null)
            {
                using (Pen pen = new Pen(Color.White, 2))
                {
                    o.DrawRect(e, pen);
                    o.DrawCircle(e, pen, o.Head);


                    if (AutoMoving)
                    {
                        
                        if (sendedToUDP)
                        {
                            if (!Vector.Equals(DetectedObject.emptyVector, o.offsetVector))
                            {
                                // инверсия вектора
                                //o.offsetVector = Vector.Multiply(o.offsetVector, -1);


                                sendedToUDP = false;;
                                gameController.MoveOffset(o.offsetVector);
                            }
                        }
                        
                    } 
                }
            }
            
            objects.Clear();

            /*SW.Stop();
            calcTimeValue.Text = (SW.ElapsedTicks / (TimeSpan.TicksPerMillisecond / 1000)).ToString() + " µs";*/

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            YoloDetection = new YoloDetection();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hook.UnHook();
        }

        private static Image cropImage(Image img, Rectangle cropArea)
        {
            bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

    }
}
