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

        public TcpClient TcpClient { get; set; }

        NetworkStream Stream { get; set; }

        ServerMessenger()
        {

        }

        public ServerMessenger(TcpClient tcpClient, List<ServerMessenger> connectedClients)
        {
            TcpClient = tcpClient;
            ConnectedClients = connectedClients;
        }

        public void ListenMessages()
        {
            using (Stream = TcpClient.GetStream())
            {
                byte[] data = new byte[TcpClient.ReceiveBufferSize];
                while (true)
                {
                    
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = Stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (Stream.DataAvailable);

                    string message = builder.ToString();

                    SendMessage(message, new Guid());
                }
            }
        }

        public void SendMessage(string message, Guid contactId)
        {
            byte[] data = new byte[TcpClient.ReceiveBufferSize];
            var mes2 = message.ToUpper();
            data = Encoding.Unicode.GetBytes(mes2);

            var messengerToSend = GetClientToSend(contactId);

            Stream.Write(data, 0, data.Length);
        }

        private ServerMessenger GetClientToSend(Guid contactId)
        {
            //TO DO

            return ConnectedClients.First();
        }
    }
}
