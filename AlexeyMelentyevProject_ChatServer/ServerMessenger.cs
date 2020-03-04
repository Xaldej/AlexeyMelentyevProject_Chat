using AlexeyMelentyevProject_ChatServer.Commands;
using AlexeyMelentyevProject_ChatServer.Data.Entities;
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
        public User User { get; set; }

        public List<ServerMessenger> ConnectedClients { get; set; }

        public TcpClient TcpClient { get; set; }

        NetworkStream Stream { get; set; }

        public List<Command> Commands { get; }

        ServerMessenger()
        {

        }

        public ServerMessenger(TcpClient tcpClient, List<ServerMessenger> connectedClients)
        {
            TcpClient = tcpClient;
            ConnectedClients = connectedClients;

            Commands = new List<Command>()
            {
                new InitializeUser()
            };
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
                        var client = ConnectedClients.Where(m => m.Equals(this)).FirstOrDefault();
                        ConnectedClients.Remove(client);

                        break;
                    }
                    

                    string message = builder.ToString();

                    if(CommandIdentifier.IsMessageACommand(message))
                    {
                        NewCommandIsGotten(message);
                    }
                    else
                    {
                        SendMessage(message, new Guid());
                    }
                }
            }
        }

        private void NewCommandIsGotten(string message)
        {
            var commandAndData = CommandIdentifier.GetCommandAndDataFromMessage(message);

            var commandsToExecute = Commands.Where(c => c.CheckIsCalled(commandAndData.Command));

            if (commandsToExecute.Count() == 0)
            {
                SendMessageToCurrentUser("Unknown command" + commandAndData.Command);
                return;
            }

            foreach (var command in commandsToExecute)
            {
                command.Execute(this, commandAndData.Data);
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

        private ServerMessenger GetClientToSend(Guid contactId)
        {
            //TO DO

            return ConnectedClients.First();
        }
    }
}
