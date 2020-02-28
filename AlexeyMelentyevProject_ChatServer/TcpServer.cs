using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyevProject_ChatServer
{
    public class TcpServer
    {
        public int Port = 8888;
        public string Ip = "127.0.0.1";

        public TcpServer()
        {

        }

        public TcpServer(int port, string ip)
        {
            Port = port;
            Ip = ip;
        }

        public void StartServer()
        {   
            TcpListener server = null;

            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, Port);

                server.Start();

                while (true)
                {
                    Console.WriteLine("Waiting for connections");

                    using (TcpClient client = server.AcceptTcpClient())
                    {
                        string response = "Hellow World!";

                        byte[] responseData = Encoding.UTF8.GetBytes(response);

                        NetworkStream stream = client.GetStream();

                        stream.Write(responseData, 0, responseData.Length);

                        Console.WriteLine("Response is send}");

                        stream.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                {
                    server.Stop();
                }
            }
        }
    }
}