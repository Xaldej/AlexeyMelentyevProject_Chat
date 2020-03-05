using AlexeyMelentyevProject_ChatServer.Data;
using Commands;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyev_chat_project.Commands.FromServer
{
    public class CorrectAddingContact : Command
    {
        public Action ContactListIsUpdated;

        public override string Name => "CorrectAddingContact";

        public override void Execute(IMessenger messenger, string data)
        {
            var userToAdd = UsersJsonParser.JsonToOneUsers(data);

            messenger.UserContacts.Add(userToAdd);
            ContactListIsUpdated();
        }
    }
}
