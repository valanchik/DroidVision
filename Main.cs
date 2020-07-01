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
        public static bool sendedToUDP = true;
        private static bool IsRender = true;
        private static bool IsDetected = true;
        private static Image imgConverted;
        private static Image imgCroped;
        private static Image imgCloned;
        private static byte[] bImg;
        private static  Bitmap bmpImage;
        private static Stopwatch SWController = new Stopwatch();
        private KalmanFilter2D Kalman2D = new KalmanFilter2D(f: 1, h: 1, q: 2, r: 15);
        private KalmanFilterRectangle KalmanRect = new KalmanFilterRectangle(f: 1, h: 1, q: 2, r: 15);
        private bool KalmanReseted = true;
        FireController FC = new FireController(5);
        MoveLimitter ML = new MoveLimitter(22);
        private Vector CalibrateVector = new Vector(0,0);
        private double CalibrateMouseCoeff = 1;
        private Counter OffsetCounter = new Counter();
        private double lastLengthMove = 0;
        private double TimePerLenght = 0;
        private double LastUDPTime = 0;
        public Main()
        {

            InitializeComponent();

            
            // arduino controller
            gameController = new UDPGameController("192.168.88.177", 8888);

            UDPGameController.OnReceive += (string message) =>
            {
                sendedToUDP = true;
                SWController.Stop();
                TimePerLenght = lastLengthMove / SWController.ElapsedMilliseconds;
                LastUDPTime = SWController.ElapsedMilliseconds;
                timeController.Invoke(new Action(() => timeController.Text = LastUDPTime.ToString() + " ms, "+TimePerLenght.ToString()+" ms на 1 длинны"));
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
                if (IsDetected)
                {
                    IsDetected = !IsDetected;

                    YoloDetection.lastImg = jpeg;
                    pictureBox1.Image = (Image)imgConverter.ConvertFrom(jpeg);
                    /*imgConverted = (Image)imgConverter.ConvertFrom(jpeg);
                    imgCroped = cropImage(imgConverted, new Rectangle(0, 0, 320, 320));
                    imgCloned = (Image)imgCroped.Clone();
                    pictureBox1.Image = imgCroped;
                    bImg = (byte[])imgConverterToBytes.ConvertTo(imgCloned, typeof(byte[]));
                    YoloDetection.lastImg = bImg;*/
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
        private void StartUDPSW()
        {
            sendedToUDP = false;
            SWController.Restart();
            SWController.Start();
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
            using (Pen pen = new Pen(Color.Red, 2) )
            {
                using (Pen pen2 = new Pen(Color.Yellow, 2))
                {
                    while (!detected.IsEmpty)
                    {
                        DetectedObject item;
                        if (detected.TryTake(out item))
                        {
                            switch (item.Name)
                            {
                                case ("0"):
                                    //item.DrawRect(e, pen);
                                    break;
                                case ("1"):
                                    //item.DrawRect(e, pen2);
                                    break;
                                default:
                                    item.DrawRect(e, pen);
                                    break;
                            }
                            
                            obj.Add(item);
                        }
                    }
                }
            }
            DetectedObjectController objects = new DetectedObjectController(new Vector(1920, 1080), new Vector(852, 480), obj);
            DetectedObject o = objects.GetNearestRectFromCenter();
            
            if (o != null)
            {
                using (Pen pen = new Pen(Color.White, 2))
                {
                    if (KalmanReseted)
                    {
                        Kalman2D.SetState(o.Head, 40);
                        KalmanRect.SetState(o.Rect, 40F);
                        KalmanReseted = false;
                    }
                    KalmanRect.Correct(o.Rect);
                    o.Rect = KalmanRect.State;

                    o.DrawRect(e, pen);

                    Kalman2D.Correct(o.Head);
                    o.Head = Kalman2D.State;

                    o.DrawCircle(e, pen, o.Head);

                    if (AutoMoving)
                    {
                        
                        if (sendedToUDP)
                        {
                                                            
                                // инверсия вектора
                                //o.offsetVector = Vector.Multiply(o.offsetVector, -1);
                                if (calibrate.Checked)
                                {
                                    StartUDPSW();
                                    //AutoCalibrate(o.offsetVector);
                                    Console.WriteLine(o.offsetVector);
                                    lastLengthMove = o.offsetVector.Length;
                                    gameController.MoveTo(o.offsetVector);
                                    AutoMoving = !AutoMoving;
                                } else
                                {
                                    if (onlyFire.Checked)
                                    {
                                        if (objects.IsObjectCrossCenter(o) && FC.IsCanFire())
                                        {
                                            StartUDPSW();
                                            FC.Fire();
                                            GameCommand command = new GameCommand();
                                            command.ClickType = MouseClickTypes.LeftBtn;
                                            gameController.MakeCommand(command);
                                        }
                                    }
                                    else
                                    {
                                        ML.SetMinMilliseconds(LastUDPTime+ timeDetection);
                                        if (ML.IsCanMove())
                                        {
                                            
                                            if (o.offsetVector.Length>1000) {
                                                Vector norm = new Vector(o.offsetVector.X, o.offsetVector.Y);
                                                norm.Normalize();
                                                o.offsetVector = Vector.Multiply(norm, 100);
                                            }
                                            StartUDPSW();
                                            GameCommand command = new GameCommand();
                                            if (FC.IsCanFire() && objects.IsObjectCrossCenter(o) /*&& OffsetCounter.Avg < 2*/)
                                            {
                                                FC.Fire();
                                                command.ClickType = MouseClickTypes.LeftBtn;
                                            }
                                            ML.Move();
                                            command.X = (int)o.offsetVector.X;
                                            command.Y = (int)o.offsetVector.Y;
                                            lastLengthMove = o.offsetVector.Length;
                                            gameController.MakeCommand(command);
                                            
                                            Console.WriteLine(o.offsetVector);


                                        }
                                        
                                    }
                                }
                        }
                    } else
                    {
                        sendedToUDP = true;
                    }
                    
                }
            } else
            {
                KalmanReseted = true;
                //OffsetCounter.Reset();
            }
            if (lastObj != null && o != null )
            {
                OffsetCounter.Add(objects.GetDistance(lastObj.offsetVector, o.offsetVector));
                avgOffsetObject.Text = OffsetCounter.Avg.ToString();
            }
            lastObj = o;
            objects.Clear();
            IsDetected = !IsDetected;
            /*SW.Stop();
            calcTimeValue.Text = (SW.ElapsedTicks / (TimeSpan.TicksPerMillisecond / 1000)).ToString() + " µs";*/

        }
        private void AutoCalibrate (Vector offsetVector)
        {
            if (!Vector.Equals(DetectedObject.emptyVector, CalibrateVector))
            {
                double delta = Vector.Subtract(offsetVector, CalibrateVector).Length;
                CalibrateMouseCoeff = ((CalibrateVector.Length + delta) / CalibrateVector.Length);
                vectorCoeff.Text = CalibrateMouseCoeff.ToString();
                CalibrateVector = DetectedObject.emptyVector;
            }
            else
            {
                CalibrateVector = offsetVector;
            }
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

        private void kalmanError_TextChanged(object sender, EventArgs e)
        {
            if (kalmanError.Text == "")
            {
                return;
            }
            double val = -1;

            double.TryParse(kalmanError.Text, out val);
            if (val>-1)
            {
                kalmanError.Text = val.ToString();
                Kalman2D.R = val;
                KalmanRect.R = val;
            } else
            {
                kalmanError.Text = "1";
            }
            kalmanError.SelectionStart = kalmanError.Text.Length;
            kalmanError.Focus();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string text = gc_commnad.Text;
            MouseClickTypes type = (MouseClickTypes)mouseClickType.SelectedIndex;
            string timeout = mouseTimeout.Text;
            Vector vect = gameController.StringToVector(text);
            GameCommand gc = new GameCommand();
            gc.X = (int)vect.X;
            gc.Y = (int)vect.Y;
            gc.ClickType = type;
            int ct = 0;
            Int32.TryParse(timeout, out ct);
            gc.ClickTimeout = ct;
            gameController.MakeCommand(gc);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            mouseClickType.SelectedIndex = 0;
        }

        private void mouseClickType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mouseTimeout.Enabled = !((MouseClickTypes)mouseClickType.SelectedIndex == MouseClickTypes.None);
        }

        private void maxFirePErSecond_TextChanged(object sender, EventArgs e)
        {
            double v = 1;
            double.TryParse(maxFirePErSecond.Text, out v);
            FC.SetMaxFirePerSecond(v);
        }

        private void covariance_TextChanged(object sender, EventArgs e)
        {
            if (covariance.Text == "")
            {
                return;
            }
            float val = -1;

            float.TryParse(covariance.Text, out val);
            if (val > -1)
            {
                covariance.Text = val.ToString();
                Kalman2D.Covariance = val;
                KalmanRect.Covariance = val;
            }
            else
            {
                covariance.Text = "1";
            }
            covariance.SelectionStart = covariance.Text.Length;
            covariance.Focus();
        }
    }
}
