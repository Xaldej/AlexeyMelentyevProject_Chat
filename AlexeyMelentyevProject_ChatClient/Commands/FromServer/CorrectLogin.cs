﻿using Commands;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyev_chat_project.Commands.FromServer
{
    public class CorrectLogin : Command
    {
        public override string Name => "CorrectLogin";

        public override void Execute(IMessenger messenger, string data)
        {
            messenger.User.Id = Guid.Parse(data);
            var message = "/getconactlist:" + data;
            messenger.SendCommand(message);
        }
    }
}
