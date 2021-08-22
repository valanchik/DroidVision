using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DroidVision
{
    class udpKeyServer
    {
        static int localPort;
        Thread receiveThread;
        public delegate void OnBool(bool value);
        public static OnBool OnKeyMoving;
        public udpKeyServer(int port)
        {
            try
            {
                localPort = port;

                receiveThread = new Thread(new ThreadStart(ReceiveMessage));
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
            UdpClient receiver = new UdpClient(localPort); 
            IPEndPoint remoteIp = null; 
            
            try
            {
                while (true)
                {
                    byte[] data = receiver.Receive(ref remoteIp);
                    string text = Encoding.ASCII.GetString(data);
                    if (text.StartsWith("am:"))
                    {
                        int x = 0;

                        Int32.TryParse(text.Substring(3), out x);
                        OnKeyMoving?.Invoke(x>0);
                    }

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
