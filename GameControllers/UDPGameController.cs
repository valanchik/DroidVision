using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace YoloDetection
{
    enum MouseClickTypes
    {
        None,
        LeftBtn,
        RightBtn
    }
    struct GameCommand
    {
        public int X { get; set; }
        public int Y { get; set; }
        [JsonProperty("ctype")]
        public MouseClickTypes ClickType { get; set; }
        [JsonProperty("ctimeout")]
        public int ClickTimeout { get; set; }
    }
    struct MouseEvent
    {
        public double x, y;
        public bool click;
    }
    class UDPGameController : IGameController
    {
        UdpClient client;
        Thread receiveThread;
        private static string Host;
        static int Port;
        public delegate void OnString(string value);
        public static OnString OnReceive;
        public UDPGameController (string host, int port) {
            Host = host;
            Port = port;
            client = new UdpClient(Host, Port);
            receiveThread = new Thread(new ThreadStart(ReceiveMessage));
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }

        public void MoveOffset(Vector vector)
        {
            string message = "m"+vector.X+":"+vector.Y;
            byte[] data = Encoding.ASCII.GetBytes(message);
            client.Send(data, data.Length);
        }

        public void MoveTo(Vector vector)
        {
            GameCommand gc = new GameCommand();
            gc.X = (int)vector.X;
            gc.Y = (int)vector.Y;
            MakeCommand(gc);
        }
        private static void ReceiveMessage()
        {
            UdpClient receiver = new UdpClient(Port); // UdpClient для получения данных
            IPEndPoint remoteIp = null; // адрес входящего подключения
            try
            {
                while (true)
                {
                    byte[] data = receiver.Receive(ref remoteIp);
                    string text = Encoding.ASCII.GetString(data);
                    OnReceive?.Invoke(text);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                receiver.Close();
            }
        }

        public Vector StringToVector(string str)
        {
            Regex regex = new Regex(@"([\-\d]+)x([\-\d]+).*?");
            MatchCollection matches = regex.Matches(str);
            if (matches.Count > 0)
            {
                double x = 0;
                double y =0;
                foreach (Match match in matches)
                {
                    double.TryParse(match.Groups[1].Value, out x);
                    double.TryParse(match.Groups[2].Value, out y);
                    break;
                }
                    

                return new Vector(x, y);
            }
            else
            {
                return new Vector(0, 0);
            }
            
        }

        public void MakeCommand(GameCommand command)
        {
            string str = JsonConvert.SerializeObject(command);
            byte[] data = Encoding.ASCII.GetBytes(str);
            client.Send(data, data.Length);
        }
    }
}
