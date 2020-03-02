using AlexeyMelentyevProject_ChatServer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlexeyMelentyevProject_ChatServer
{
    public class Client
    {
        User User { get; set; }
        ServerMessenger Messenger { get; set; }

        List<Client> ConnectedClients { get; set; }

        Client()
        {

        }

        public Client(TcpClient tcpClient, List<Client> connectedClients)
        {
            ConnectedClients = connectedClients;
            Messenger = new ServerMessenger(tcpClient, ConnectedClients);
        }

        public void Process()
        {
            try
            {
                InitializeUser();
            }
            catch(Exception e)
            {
                Console.WriteLine();
                var errorMessage = "Login problems. Try to reconnect\n" + "Detailed error: " + e.Message;
                Messenger.SendMessage(errorMessage, User.Guid);
                ConnectedClients.Remove(this);
            }

            var thread = new Thread(new ThreadStart(Messenger.ListenMessages));
            thread.Start();
        }

        private void InitializeUser()
        {
            //throw new NotImplementedException();
        }
    }
}
