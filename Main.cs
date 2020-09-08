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
using OpenCvSharp;
using System.Drawing.Imaging;
using YoloDetection.Marker;

namespace YoloDetection
{
    public partial class Main : Form
    {
        static ConcurrentBag<DetectedObject> detected = new ConcurrentBag<DetectedObject>();
        static List<DetectedObject> heads = new List<DetectedObject>();
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
        private static Image cimg;
        private static byte[] bImg;
        private static Bitmap bmpImage;
        private static Stopwatch SWController = new Stopwatch();
        private static Stopwatch SWTimeDetectObject = new Stopwatch();
        private KalmanFilter2D Kalman2D = new KalmanFilter2D(f: 1, h: 1, q: 2, r: 15);
        private KalmanFilter2D Kalman2DPredict = new KalmanFilter2D(f: 1, h: 1, q: 2, r: 0);
        private KalmanFilterRectangle KalmanRect = new KalmanFilterRectangle(f: 1, h: 1, q: 2, r: 15);
        private bool KalmanReseted = true;
        private bool KalmanPredictReseted = true;
        FireController FC = new FireController(5);
        MoveLimitter ML = new MoveLimitter(22);
        private Vector CalibrateVector = new Vector(0, 0);
        private float CalibrateMouseCoeff = 1;
        private Counter OffsetCounter = new Counter();
        private Counter2d PredictCounter = new Counter2d();
        private double lastLengthMove = 0;
        private double TimePerLenght = 0;
        private double LastUDPTime = 0;
        private int TurnTimeOut = 230;
        private Vector lastOffset = new Vector(0, 0);
        private bool stopedMoving = false;
        private int cnt = 0;
        private Stopwatch SavePicSW = new Stopwatch();
        private bool savingImg = false;
        private byte[] lastJPEG = new byte[0];
        private MJPEGWriter mjpegWriter;
        private MarkerFasad marker;
        public Main()
        {

            InitializeComponent();
            marker = new MarkerFasad(imageViewer, playTimer);

            kalmanError_TextChanged(null, null);
            covariance_TextChanged(null, null);
            maxFirePErSecond_TextChanged(null, null);
            turnTimeOutValue_TextChanged(null, null);
            mouseCoeff_TextChanged(null, null);
            mjpegWriter = new MJPEGWriter("video.mjpeg");
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
                timeController.Invoke(new Action(() => timeController.Text = LastUDPTime.ToString() + " ms, " + TimePerLenght.ToString() + " ms на 1 длинны"));
            };

            server = new UDPServer();
            keyServer = new udpKeyServer(8889);
            udpKeyServer.OnKeyMoving += (bool status) =>
             {
                 if (status)
                 {
                     AutoMoving = !AutoMoving;
                     GameCommand gc = new GameCommand();
                     gc.LED = AutoMoving;
                     gameController.MakeCommand(gc);
                 }

             };

            UDPServer.mjpegParser.OnJPEG += (byte[] jpeg) =>
            {
                if (IsDetected)
                {
                    
                    lastJPEG = jpeg;
                    YoloDetection.lastImg = jpeg;
                    if (drawImage.Checked)
                    {
                        try
                        {
                            pictureBox1.Image = (Image)imgConverter.ConvertFrom(jpeg);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
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
                YoloRunTimeValue.Invoke(new Action(() => {

                    YoloRunTimeValue.Text = time.ToString() + " ms";
                    Make();
                }));
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
        async private void Make() {
            
            autoMovingStatus.BackColor = AutoMoving ? Color.Green : Color.Red;
            
            List<DetectedObject> obj = new List<DetectedObject>();
            while (!detected.IsEmpty)
            {
                DetectedObject item;
                if (detected.TryTake(out item))
                {
                    obj.Add(item);
                }
            }
            DetectedObjectController objects = new DetectedObjectController(new Vector(1920, 1080), new Vector(852, 480), obj);
            //DetectedObjectController objects = new DetectedObjectController(new Vector(1920, 1080), new Vector(1920, 1080), obj);
            DetectedObject o = objects.GetNearestRectFromCenter();

            if (o != null)
            {
                // получем обсласти головы
                Mat croppedImg = Mat.FromImageData(lastJPEG);
                try
                {
                    croppedImg = new Mat(croppedImg, new OpenCvSharp.Rect(o.Rect.X, o.Rect.Y, o.Rect.Width, o.Rect.Height));
                    heads = await GetHeads(croppedImg.ToBytes());
                } catch (Exception e)
                {
                    heads.Clear();
                }
                Vector Head = o.Head;
                Rectangle FireRect = o.Rect;
                if (heads.Count>0)
                {
                    Head = new Vector(o.Rect.X+heads[0].Center.X, o.Rect.Y+ heads[0].Center.Y);
                    FireRect = heads[0].Rect;
                }
                
                if (KalmanReseted)
                {
                    Kalman2D.SetState(Head, 40);

                    KalmanRect.SetState(o.Rect, 40F);
                    KalmanReseted = false;
                }
                KalmanRect.Correct(o.Rect);
                
                o.Rect = KalmanRect.State;
                

                Kalman2D.Correct(Head);
                Head = Kalman2D.State;
                if (drawImage.Checked && pictureBox1.Image != null)
                {
                    if (createDetectedImg.Checked)
                    {
                        try
                        {
                            /*cimg = cropImage(pictureBox1.Image, o.Rect);
                            cimg.Save(@".\pic\" + saveImgPrefix.Text + "_" + cnt + ".png", System.Drawing.Imaging.ImageFormat.Png);*/
                            croppedImg.SaveImage(@".\pic\" + saveImgPrefix.Text + "_" + cnt + ".png");
                            cnt++;
                        }
                        catch (Exception ee)
                        {
                            Console.WriteLine(ee);
                        }

                        SavePicSW.Restart();
                    }

                    using (Pen pen = new Pen(Color.Red, 2))
                    using (Pen pen2 = new Pen(Color.Yellow, 2))
                    using (Graphics G = Graphics.FromImage(pictureBox1.Image))
                    {
                        o.DrawRect(G, pen);
                        if (heads.Count>0)
                        {
                            //o.DrawCircle(G, pen2, heads[0].Center);
                            Rectangle rect = new Rectangle(
                                o.Rect.X+heads[0].Rect.X,
                                o.Rect.Y+heads[0].Rect.Y,
                                heads[0].Rect.Width,
                                heads[0].Rect.Height
                                );
                            o.DrawRect(G, pen2, rect);
                        } else
                        {
                            o.DrawCircle(G, pen, Head);
                        }
                         
                    }
                    
                    pictureBox1.Refresh();
                }
                if (AutoMoving)
                {

                    if (true || sendedToUDP)
                    {

                        if (lastObj != null)
                        {
                            if (KalmanPredictReseted)
                            {
                                Kalman2DPredict.SetState(Head, 0);
                                PredictCounter.Reset();
                                PredictCounter.Add(Head);
                                KalmanPredictReseted = false;
                            }

                            //Vector predicted = objects.Predict(Vector.Subtract(Head, lastObj.Head), OffsetCounter.Avg*2);
                            Vector delta = Vector.Subtract(Head, lastObj.Head);
                            double scalar = OffsetCounter.Avg;
                            double scalarLimit = 1F;
                            if (scalar > scalarLimit)
                            {
                                scalar = scalarLimit;
                            }
                            Vector predicted = Vector.Add(Head, Vector.Multiply(delta, scalar));

                            PredictCounter.Add(predicted);
                            Kalman2DPredict.Correct(PredictCounter.Avg);
                            predicted = Kalman2DPredict.State;

                            //o.offsetVector = objects.GetFromCenter(predicted);
                            o.offsetVector = objects.GetFromCenter(Head);

                            //o.DrawCircle(e, predictPen, predicted);

                        }
                        else
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
                                if (objects.IsObjectCrossCenter(FireRect) && FC.IsCanFire())
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
                                        //Console.WriteLine(o.offsetVector);
                                    }
                                    lastLengthMove = o.offsetVector.Length;
                                    StartUDPSW();
                                    if (canFire.Checked && FC.IsCanFire() /*&& OffsetCounter.Avg < 2*/)
                                    {
                                        if (!fireOnlyRect.Checked || (fireOnlyRect.Checked && objects.IsObjectCrossCenter(o)))
                                        {
                                            FC.Fire();
                                            // компенсация отдачи
                                            o.offsetVector = Vector.Add(o.offsetVector, new Vector(0, 0));
                                            command.X = (int)o.offsetVector.X;
                                            command.Y = (int)o.offsetVector.Y;

                                            command.ClickType = MouseClickTypes.LeftBtn;
                                            command.ClickTimeout = TurnTimeOut;
                                        }
                                    }
                                    command.LED = AutoMoving;

                                    if (!movingX.Checked)
                                    {
                                        command.X = 0;
                                    }
                                    if (!movingY.Checked)
                                    {
                                        command.Y = 0;
                                    }

                                    gameController.MakeCommand(command);
                                    stopedMoving = false;
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
            else
            {
                KalmanReseted = true;
                // останавливаем движение
                if (false && !stopedMoving)
                {
                    stopedMoving = true;
                    GameCommand command = new GameCommand();
                    command.LED = AutoMoving;
                    //gameController.MakeCommand(command);
                }

                //OffsetCounter.Reset();
            }
            if (lastObj != null && o != null)
            {
                OffsetCounter.Add(objects.GetDistance(lastObj.offsetVector, o.offsetVector));
                avgOffsetObject.Text = OffsetCounter.Avg.ToString();
            }
            lastObj = o;
            objects.Clear();
            if (IsWriteStream.Checked)
            {
                if (drawImage.Checked && pictureBox1.Image != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        pictureBox1.Image.Save(memoryStream, ImageFormat.Jpeg);
                        mjpegWriter.Add(memoryStream.ToArray());
                    }

                }
                else
                {
                    mjpegWriter.Add(lastJPEG);
                }
            }
            
                
            IsDetected = true;
        }
        private Task<List<DetectedObject>> GetHeads(byte[] data)
        {
            return Task.Run(() =>
            {
                List<DetectedObject> tmp = new List<DetectedObject>();
                YoloDetection.lastImgTiny = data;
                bool ready = false;
                YoloDetection.OnTinyObject = (DetectedObject objTiny) =>
                {
                    tmp.Add(objTiny);
                };
                YoloDetection.OnYoloTinyDetect = (long time) =>
                {
                    ready = true;
                };
                while (!ready)
                {
                    Thread.Sleep(1);
                }

                return tmp;
            });
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (savingImg)
            {
                return;
            }

            /*Stopwatch SW = new Stopwatch(); // Создаем объект
            SW.Start(); // Запускаем*/


            
            
            
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

        private void button1_Click(object sender, EventArgs e)
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

        private void createDetectedImg_CheckedChanged(object sender, EventArgs e)
        {
            if (createDetectedImg.Checked)
            {
                SavePicSW.Start();
            } else
            {
                SavePicSW.Stop();
                SavePicSW.Reset();
            }
        }

        private void autoMovingStatus_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void autoMovingStatus_MouseClick(object sender, MouseEventArgs e)
        {
            AutoMoving = !AutoMoving;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (FC.IsCanFire())
            {
                FC.Fire();
                Console.WriteLine(FC.Counter);
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
                gc.LED = led13.Checked;
                gameController.MakeCommand(gc);
            }
        }

        private void maxFirePErSecond_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void CanMouseMove_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void saveImgPrefix_TextChanged(object sender, EventArgs e)
        {

        }

        private void gc_commnad_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog FBD = new OpenFileDialog();
            
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(FBD.SelectedPath);
                dataPath.Text = FBD.FileName;
                marker.Load(FBD.FileName);
            }
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void canFire_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void movingX_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void movingY_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void mPlay_Click(object sender, EventArgs e)
        {
            playTimer.Enabled = !playTimer.Enabled;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            marker.ShowForwardFrame();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            playTimer.Interval = trackBar1.Value;
        }

        private void trackBar1_Move(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            marker.ShowBackwardFrame();
        }
    }
}
