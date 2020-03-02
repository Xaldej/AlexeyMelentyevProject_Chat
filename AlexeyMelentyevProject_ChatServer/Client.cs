using AlexeyMelentyevProject_ChatServer.Data;
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
            Messenger.UserLoginIsGotten += InitializeUser;
        }

        public void Process()
        {
            var thread = new Thread(new ThreadStart(Messenger.ListenMessages));
            thread.Start();
        }

        private void InitializeUser(string userLogin)
        {   
            try
            {
                GetUserFromDB(userLogin);
            }
            catch (Exception e)
            {
                Console.WriteLine("User is not logged in");
                var errorMessage = "Login problems. Try to reconnect\n" + "Detailed error: " + e.Message;
                Messenger.SendErrorToCurrentUser(errorMessage);
                ConnectedClients.Remove(this);
            }
        }

        private void GetUserFromDB(string userLogin)
        {
            using (var context = new AmMessengerContext())
            {
                var user = context.Users.Where(u => u.Login == userLogin).FirstOrDefault();

                if(user == null)
                {
                    user = new User()
                    {
                        Id = Guid.NewGuid(),
                        Login = userLogin
                    };

                    context.Users.Add(user);

                    context.SaveChanges();
                }

                User = user;

                Messenger.IsUserLoggedIn = true;
            }
        }
    }
}
