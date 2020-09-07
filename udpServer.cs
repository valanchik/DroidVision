using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace YoloDetection
{
    class UDPServer
    {

        static int localPort; // локальный порт для прослушивания входящих подключений
        public delegate void OnBytes(byte[] jpeg);
        public static OnBytes OnJPEG;
        public static MJPEGParser mjpegParser = new MJPEGParser();
        Thread receiveThread;
        public UDPServer()
        {
            int cnt = 0;
            
            try
            {
                localPort = 9191;

                receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Priority = ThreadPriority.Highest;
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ReceiveMessage()
        {
            UdpClient receiver = new UdpClient(localPort); // UdpClient для получения данных
            IPEndPoint remoteIp = null; // адрес входящего подключения
            try
            {
                while (true)
                {
                    byte[] data = receiver.Receive(ref remoteIp);
                    mjpegParser.AddBytes(data);
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