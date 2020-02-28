using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlexeyMelentyevProject_ChatServer
{
    public class TcpServer
    {
        public int Port = 8888;
        public string Ip = "127.0.0.1";

        //private Dictionary

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

                    TcpClient tcpClient = server.AcceptTcpClient();

                    var client = new Client(tcpClient);

                    var thread = new Thread(new ThreadStart(client.InitiateProcess));
                    thread.Start();
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