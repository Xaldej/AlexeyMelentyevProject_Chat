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
        public int Port { get; set; }
        public string Ip { get; set; }

        public List<ServerMessenger> ConnectedClients { get; set; }

        public ServerMessenger Messenger { get; set; }

        TcpServer()
        {
            Port = 8888;
            Ip = "127.0.0.1";
        }

        public TcpServer(int port, string ip)
        {
            Port = port;
            Ip = ip;

            ConnectedClients = new List<ServerMessenger>();
        }

        public void StartServer()
        {   
            TcpListener server = null;

            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, Port);

                server.Start();

                Console.WriteLine("Waiting for connections");

                while (true)
                {
                    TcpClient tcpClient = server.AcceptTcpClient();

                    var client = new ServerMessenger(tcpClient, ConnectedClients);

                    Console.WriteLine("client is added");
                    ConnectedClients.Add(client);

                    var thread = new Thread(new ThreadStart(client.ListenMessages));
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