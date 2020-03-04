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
            var user = messenger.User;
            var userName = data;
            try
            {
                user = GetUserFromDB(userName);
                var id = user.Id;
                messenger.SendCommand($"/correctlogin:{id}");
            }
            catch (Exception e)
            {
                Console.WriteLine("User is not logged in");
                var errorMessage = "Login problems. Try to reconnect\n" + "Detailed error: " + e.Message;
                messenger.SendCommand(errorMessage);
            }
        }

        private User GetUserFromDB(string userLogin)
        {
            User user;

            using (var context = new AmMessengerContext())
            {
                user = context.Users.Where(u => u.Login == userLogin).FirstOrDefault();

                if (user == null)
                {
                    user = new User()
                    {
                        Id = Guid.NewGuid(),
                        Login = userLogin
                    };

                    context.Users.Add(user);

                    context.SaveChanges();

                    Console.WriteLine("User added to DB");
                }
                else
                {
                    Console.WriteLine("User is got from DB");
                }
            }

            return user;
        }
    }
}
