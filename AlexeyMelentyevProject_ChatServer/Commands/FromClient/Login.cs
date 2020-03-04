using AlexeyMelentyevProject_ChatServer.Data;
using AlexeyMelentyevProject_ChatServer.Data.Entities;
using Commands;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyevProject_ChatServer.Commands.FromClient
{
    public class Login : Command
    {
        public override string Name => "Login";

        public override void Execute(IMessenger messenger, string data)
        {
            var userName = data;
            try
            {
                GetUserFromDB(messenger.User, userName);
                messenger.SendCommand("/correctlogin: ");
            }
            catch (Exception e)
            {
                Console.WriteLine("User is not logged in");
                var errorMessage = "Login problems. Try to reconnect\n" + "Detailed error: " + e.Message;
                messenger.SendCommand(errorMessage);
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
