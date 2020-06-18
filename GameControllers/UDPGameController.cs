using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace YoloDetection
{
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
            throw new NotImplementedException();
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
    }
}
