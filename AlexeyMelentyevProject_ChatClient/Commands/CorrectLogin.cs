using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyev_chat_project.Commands
{
    public class CorrectLogin : Command
    {
        public override string Name => "CorrectLogin";

        public override void Execute(AmMessenger messenger, string data)
        {
            messenger.GetContactList();
        }
    }
}
