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

        public List<Client> ConnectedClients { get; set; }

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

            ConnectedClients = new List<Client>();
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

                    // async?
                    AddClient(tcpClient);
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

        private void AddClient(TcpClient tcpClient)
        {
            var client = new Client(tcpClient, ConnectedClients);
            ConnectedClients.Add(client);
            client.Process();
            Console.WriteLine("client is added");
        }
    }
}