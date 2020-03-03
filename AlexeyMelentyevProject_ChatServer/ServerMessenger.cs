using Commands;
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

        public Action<string> NewCommandGotten;

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

                    try
                    {   
                        do
                        {
                            bytes = Stream.Read(data, 0, data.Length);
                            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        }
                        while (Stream.DataAvailable);
                    }
                    catch
                    {
                        var client = ConnectedClients.Where(c => c.Messenger.Equals(this)).FirstOrDefault();
                        ConnectedClients.Remove(client);

                        break;
                    }
                    

                    string message = builder.ToString();

                    if(CommandIdentifier.IsMessageACommand(message))
                    {
                        NewCommandGotten(message);
                        
                    }
                    else
                    {
                        SendMessage(message, new Guid());
                    }
                }
            }
        }

        public void SendMessageToCurrentUser(string message)
        {
            byte[] data = new byte[TcpClient.ReceiveBufferSize];
            var mes2 = message.ToUpper();
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
