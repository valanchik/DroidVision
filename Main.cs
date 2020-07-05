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
        private static Stopwatch SWTimeDetectObject = new Stopwatch();
        private KalmanFilter2D Kalman2D = new KalmanFilter2D(f: 1, h: 1, q: 2, r: 15);
        private KalmanFilter2D Kalman2DPredict = new KalmanFilter2D(f: 1, h: 1, q: 2, r: 0);
        private KalmanFilterRectangle KalmanRect = new KalmanFilterRectangle(f: 1, h: 1, q: 2, r: 15);
        private bool KalmanReseted = true;
        private bool KalmanPredictReseted = true;
        FireController FC = new FireController(5);
        MoveLimitter ML = new MoveLimitter(22);
        private Vector CalibrateVector = new Vector(0,0);
        private float CalibrateMouseCoeff = 1;
        private Counter OffsetCounter = new Counter();
        private Counter2d PredictCounter = new Counter2d();
        private double lastLengthMove = 0;
        private double TimePerLenght = 0;
        private double LastUDPTime = 0;
        private int TurnTimeOut = 230;
        private Vector lastOffset = new Vector(0,0);
        public Main()
        {

            InitializeComponent();
            kalmanError_TextChanged(null, null);
            covariance_TextChanged(null,null);
            maxFirePErSecond_TextChanged(null,null);
            turnTimeOutValue_TextChanged(null, null);
            mouseCoeff_TextChanged(null, null);

            // arduino controller
            gameController = new UDPGameController("192.168.88.177", 8888);

            UDPGameController.OnReceive += (string message) =>
            {
                lastOffset = Vector.Parse(message);
                //Console.WriteLine(message);
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
                    try
                    {
                        pictureBox1.Image = (Image)imgConverter.ConvertFrom(jpeg);
                    } catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    
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
                    using (Pen predictPen = new Pen(Color.Yellow, 3))
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

                            if (true || sendedToUDP)
                            {

                                if (lastObj != null)
                                {
                                    if (KalmanPredictReseted)
                                    {
                                        Kalman2DPredict.SetState(o.Head, 0);
                                        PredictCounter.Reset();
                                        PredictCounter.Add(o.Head);
                                        KalmanPredictReseted = false;
                                    }
                                    
                                    //Vector predicted = objects.Predict(Vector.Subtract(o.Head, lastObj.Head), OffsetCounter.Avg*2);
                                    Vector delta = Vector.Subtract(o.Head, lastObj.Head);
                                    double scalar = OffsetCounter.Avg;
                                    double scalarLimit = 1F;
                                    if (scalar>scalarLimit)
                                    {
                                        scalar = scalarLimit;
                                    }
                                    Vector predicted = Vector.Add(o.Head, Vector.Multiply(delta, scalar*3));
                                    
                                    PredictCounter.Add(predicted);
                                    Kalman2DPredict.Correct(PredictCounter.Avg);
                                    predicted = Kalman2DPredict.State;
                                    
                                    o.offsetVector = objects.GetFromCenter(predicted);

                                    o.DrawCircle(e, predictPen, predicted);

                                } else
                                {
                                    PredictCounter.Reset();
                                    //KalmanPredictReseted = true;
                                }

                                // инверсия вектора
                                //o.offsetVector = Vector.Multiply(o.offsetVector, -1);
                                if (calibrate.Checked)
                                {
                                    StartUDPSW();
                                    //Vector v = objects.GetFromCenter(o.offsetVector);
                                    Vector v = o.offsetVector;
                                    AutoCalibrate(v);
                                    lastLengthMove = v.Length;
                                    gameController.MoveTo(v);
                                    AutoMoving = !AutoMoving;
                                }
                                else
                                {
                                    if (onlyFire.Checked)
                                    {
                                        if (objects.IsObjectCrossCenter(o) && FC.IsCanFire())
                                        {
                                            //StartUDPSW();
                                            FC.Fire();
                                            GameCommand command = new GameCommand();
                                            command.ClickType = MouseClickTypes.LeftBtn;
                                            gameController.MakeCommand(command);
                                        }
                                    }
                                    else
                                    {
                                        //ML.SetMinMilliseconds(LastUDPTime+ timeDetection);
                                        if (ML.IsCanMove())
                                        {

                                            /*if (o.offsetVector.Length>1000) {
                                                Vector norm = new Vector(o.offsetVector.X, o.offsetVector.Y);
                                                norm.Normalize();
                                                o.offsetVector = Vector.Multiply(norm, 100);
                                            }*/

                                            GameCommand command = new GameCommand();
                                            ML.Move();
                                            if (autoCalibration.CheckState == CheckState.Checked)
                                            {
                                                AutoCalibrate(o.offsetVector);
                                            }
                                            o.offsetVector = Vector.Multiply(CalibrateMouseCoeff, o.offsetVector);
                                            
                                            if (CanMouseMove.Checked)
                                            {
                                                command.X = (int)o.offsetVector.X;
                                                command.Y = (int)o.offsetVector.Y;
                                                Console.WriteLine(o.offsetVector);
                                            }
                                            lastLengthMove = o.offsetVector.Length;
                                            StartUDPSW();
                                            if (canFire.Checked && FC.IsCanFire() && objects.IsObjectCrossCenter(o) /*&& OffsetCounter.Avg < 2*/)
                                            {

                                                FC.Fire();

                                                o.offsetVector = Vector.Add(o.offsetVector, new Vector(0, 6));
                                                command.X = (int)o.offsetVector.X;
                                                command.Y = (int)o.offsetVector.Y;

                                                command.ClickType = MouseClickTypes.LeftBtn;
                                                command.ClickTimeout = TurnTimeOut;
                                                
                                            }
                                            gameController.MakeCommand(command);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            sendedToUDP = true;
                        }
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
                CalibrateMouseCoeff *=  (float)((CalibrateVector.Length + delta) / CalibrateVector.Length);
                mouseCoeff.Text = CalibrateMouseCoeff.ToString();
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
            button1.Enabled = false;
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

        private double DoubleParser(string text)
        {
            double val = double.MaxValue;
            double.TryParse(text, out val);
            return val;
        }
        private float FloatParser(string text)
        {
            float val = float.MaxValue;
            float.TryParse(text, out val);
            return val;
        }
        private int IntParser(string text)
        {
            int val = int.MaxValue;
            Int32.TryParse(text, out val);
            return val;
        }
        private void kalmanError_TextChanged(object sender, EventArgs e)
        {
            if (kalmanError.Text == "")
            {
                return;
            }
            double val = DoubleParser(kalmanError.Text);

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
            if (FC.IsCanFire())
            {
                FC.Fire();
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

        private void turnTimeOutValue_TextChanged(object sender, EventArgs e)
        {
            int v = 0;
            Int32.TryParse(turnTimeOutValue.Text, out v);
            TurnTimeOut = v;
        }

        private void mouseCoeff_TextChanged(object sender, EventArgs e)
        {
            if (mouseCoeff.Text == "" || mouseCoeff.Text.Substring(mouseCoeff.Text.Length-1,1) == ",")
            {
                return;
            }
            float val = -1;

            float.TryParse(mouseCoeff.Text, out val);
            if (val > -1)
            {
                mouseCoeff.Text = val.ToString();
                CalibrateMouseCoeff = val;
            }
            else
            {
                mouseCoeff.Text = "1";
            }
            mouseCoeff.SelectionStart = mouseCoeff.Text.Length;
            mouseCoeff.Focus();
        }
    }
}
