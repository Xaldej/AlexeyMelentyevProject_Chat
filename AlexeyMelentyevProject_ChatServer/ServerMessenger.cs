using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace AlexeyMelentyevProject_ChatServer
{
    public class ServerMessenger : IMessenger
    {
        public List<Client> ConnectedClients { get; set; }

        public TcpClient TcpClient { get; set; }

        NetworkStream Stream { get; set; }

        public bool IsUserLoggedIn { get; set; }

        public Action<string> UserLoginIsGotten;

        ServerMessenger()
        {

        }

        public ServerMessenger(TcpClient tcpClient, List<Client> connectedClients)
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

                    if(IsUserLoggedIn)
                    {
                        SendMessage(message, new Guid());
                    }
                    else
                    {
                        UserLoginIsGotten(message);
                    }
                }
            }
        }

        public void SendErrorToCurrentUser(string error)
        {
            byte[] data = new byte[TcpClient.ReceiveBufferSize];
            var mes2 = error.ToUpper();
            data = Encoding.Unicode.GetBytes(mes2);

            Stream.Write(data, 0, data.Length);
        }

        public void SendMessage(string message, Guid contactId)
        {
            byte[] data = new byte[TcpClient.ReceiveBufferSize];
            var mes2 = message.ToUpper();
            data = Encoding.Unicode.GetBytes(mes2);

            var clientToSend = GetClientToSend(contactId);

            Stream.Write(data, 0, data.Length);
        }

        private Client GetClientToSend(Guid contactId)
        {
            //TO DO

            return ConnectedClients.First();
        }
    }
}
