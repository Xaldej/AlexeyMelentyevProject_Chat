using AlexeyMelentyevProject_ChatServer.Commands;
using AlexeyMelentyevProject_ChatServer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace AlexeyMelentyevProject_ChatServer
{
    public class Client
    {
        public User User { get; set; }

        public ServerMessenger Messenger { get; set; }

        public List<Client> ConnectedClients { get; set; }

        public List<Command> Commands { get; }

        Client()
        {
            
        }

        public Client(TcpClient tcpClient, List<Client> connectedClients)
        {
            ConnectedClients = connectedClients;
            Messenger = new ServerMessenger(tcpClient, ConnectedClients);
            Messenger.NewCommandGotten += OnGetCommand;

            Commands = new List<Command>()
            {
                new InitializeUser()
            };
        }

        private void OnGetCommand(string message)
        {
            var commandAndData = CommandIdentifier.GetCommandAndDataFromMessage(message);

            var commandsToExecute = Commands.Where(c => c.CheckIsCalled(commandAndData.Command));

            if(commandsToExecute.Count()==0)
            {
                Messenger.SendErrorToCurrentUser("Unknown command");
                return;
            }

            foreach (var command in commandsToExecute)
            {
                command.Execute(this, commandAndData.Data);
            }
        }

        public void Process()
        {
            var thread = new Thread(new ThreadStart(Messenger.ListenMessages));
            thread.Start();
        }

        
    }
}
