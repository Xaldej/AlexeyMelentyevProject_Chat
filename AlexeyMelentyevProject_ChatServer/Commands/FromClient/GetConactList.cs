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
    public class GetConactList : Command
    {
        public override string Name => "GetConactList";

        public override void Execute(IMessenger messenger, string data)
        {
            try
            {
                messenger.UserContacts = GetContactsFromDb(messenger.User);
            }
            catch(Exception e)
            {
                messenger.SendCommand($"/servererror:{e.Message}");
            }

            if (messenger.UserContacts.Count() > 0)
            {
                var contactsJson = UsersJsonParser.ManyUsersToJson(messenger.UserContacts);

                var command = "/correctcontactlist:" + contactsJson;
                messenger.SendCommand(command);
            }
        }

        private List<User> GetContactsFromDb(User user)
        {
            var contacts = new List<User>();

            using (var contect = new AmMessengerContext())
            {
                var contactsIds = contect.ContactRelationships.Where(cr => cr.UserId == user.Id).Select(cr=>cr.ContactId).ToList();

                foreach (var id in contactsIds)
                {
                    var contact = contect.Users.Where(u => u.Id == id).FirstOrDefault();
                    contacts.Add(contact);
                }
            }

            return contacts;
        }
    }
}
