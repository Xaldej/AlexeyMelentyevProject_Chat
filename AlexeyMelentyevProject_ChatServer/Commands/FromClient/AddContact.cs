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
    public class AddContact : Command
    {
        public override string Name => "AddContact";

        public override void Execute(IMessenger messenger, string data)
        {
            try
            {
                AddContactFromDb(messenger, data);
            }
            catch(Exception e)
            {
                var errorMessage = "/showerror:" +
                    "Error adding contact\n\n" +
                    "Details:\n" + e.Message;
                messenger.SendCommand(errorMessage);
            }
            
        }

        private void AddContactFromDb(IMessenger messenger, string data)
        {
            var user = messenger.User;

            User userToAdd;
            using (var context = new AmMessengerContext())
            {
                userToAdd = context.Users.Where(u => u.Login == data).FirstOrDefault();

                if (userToAdd == null)
                {
                    messenger.SendCommand("/showerror:Contact is not found");
                }
                else
                {
                    if (messenger.UserContacts.Where(u => u.Id == userToAdd.Id).FirstOrDefault() != null)
                    {
                        messenger.SendCommand("/showerror:Contact is already in your list");
                    }
                    else
                    {
                        messenger.UserContacts.Add(userToAdd);

                        var contactRelationship = new ContactRelationship()
                        {
                            Id = Guid.NewGuid(),
                            UserId = messenger.User.Id,
                            ContactId = userToAdd.Id,
                        };

                        context.ContactRelationships.Add(contactRelationship);

                        context.SaveChanges();

                        var command = "/correctaddingcontact:" + UsersJsonParser.OneUserToJson(userToAdd);
                        messenger.SendCommand(command);
                    }
                    
                }
            }
        }
    }
}
