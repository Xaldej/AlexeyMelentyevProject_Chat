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

        public override void Execute(AmMessenger messenger, string data)
        {
            var message = "/getconactlist:" + messenger.UserLogin;
            messenger.ExecuteCommands(message);
        }
    }
}
