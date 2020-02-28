using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyevProject_ChatServer
{
    public class ServerMessenger : IMessenger
    {
        public List<ServerMessenger> ConnectedClients { get; set; }

        public TcpClient TcpListener { get; set; }

        ServerMessenger()
        {

        }

        public ServerMessenger(TcpClient tcpClient, List<ServerMessenger> connectedClients)
        {
            TcpListener = tcpClient;
            ConnectedClients = connectedClients;
        }

        public void ListenMessages()
        {
            using (var stream = TcpListener.GetStream())
            {
                byte[] data = new byte[64];
                while (true)
                {
                    
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();

                    SendMessage(message, "temp");

                    var mes2 = message.ToUpper();
                    data = Encoding.Unicode.GetBytes(mes2);
                    stream.Write(data, 0, data.Length);
                }
            }
        }

        public void SendMessage(string message, string contactName)
        {
        }

        private ServerMessenger GetClientToSend(string contactName)
        {
            //TO DO

            return ConnectedClients.First();
        }
    }
}
