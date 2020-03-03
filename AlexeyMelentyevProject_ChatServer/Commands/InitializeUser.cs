using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexeyMelentyevProject_ChatServer.Data;
using AlexeyMelentyevProject_ChatServer.Data.Entities;

namespace AlexeyMelentyevProject_ChatServer.Commands
{
    public class InitializeUser : Command
    {
        public override string Name => "InitializeUser";

        public override void Execute(Client client, string data)
        {
            var user = client.User;
            var userName = data;
            try
            {
                GetUserFromDB(user, userName);
                client.Messenger.SendMessageToCurrentUser("/correctlogin: ");
            }
            catch (Exception e)
            {
                Console.WriteLine("User is not logged in");
                var errorMessage = "Login problems. Try to reconnect\n" + "Detailed error: " + e.Message;
                client.Messenger.SendMessageToCurrentUser(errorMessage);
                client.ConnectedClients.Remove(client);
            }
        }

        private void GetUserFromDB(User user, string userLogin)
        {
            using (var context = new AmMessengerContext())
            {
                var userFromDb = context.Users.Where(u => u.Login == userLogin).FirstOrDefault();

                if (userFromDb == null)
                {
                    userFromDb = new User()
                    {
                        Id = Guid.NewGuid(),
                        Login = userLogin
                    };

                    context.Users.Add(userFromDb);

                    context.SaveChanges();

                    Console.WriteLine("User added to DB");
                }
                else
                {
                    Console.WriteLine("User is got from DB");
                }

                user = userFromDb;
            }
        }
    }
}
