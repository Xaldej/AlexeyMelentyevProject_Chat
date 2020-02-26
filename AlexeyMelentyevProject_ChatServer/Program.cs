using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyevProject_ChatServer
{
    class Program
    {
        const int port = 8888;

        static void Main(string[] args)
        {
            TcpListener server = null;

            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);

                server.Start();

                while(true)
                {
                    Console.WriteLine("Waiting for connections");

                    TcpClient client = server.AcceptTcpClient();

                    NetworkStream stream = client.GetStream();

                    string response = "Hellow World!";

                    byte[] responseData = Encoding.UTF8.GetBytes(response);

                    
                    stream.Write(responseData, 0, responseData.Length);
                    Console.WriteLine("Response is send}");
                    
                    stream.Close();
                    
                    client.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server!=null)
                {
                    server.Stop();
                }
            }
        }
    }
}
