using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyev_chat_project.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public bool CheckIsCalled(string command) => command.ToLower().Contains("/" + Name.ToLower());
        //{
        //    var commandToLower = command.ToLower();
        //    var nameToLower = "/" + Name.ToLower();

        //    var isContain = commandToLower.Contains(nameToLower);

        //    return isContain;
        //}

        public abstract void Execute(AmMessenger messenger, string data);
    }
}
