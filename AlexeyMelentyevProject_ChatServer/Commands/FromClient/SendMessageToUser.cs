using AlexeyMelentyevProject_ChatServer.Data;
using Commands;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyevProject_ChatServer.Commands.FromClient
{
    public class SendMessageToUser : Command
    {
        public override string Name => "SendMessageToUser";

        public override void Execute(IMessenger messenger, string data)
        {
            var messageToUser = MessagesJsonParser.JsonToOneMessage(data);

            //TO DO
        }
    }
}
