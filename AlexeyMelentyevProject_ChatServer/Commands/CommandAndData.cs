using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexeyMelentyevProject_ChatServer.Commands
{
    public struct CommandAndData
    {
        public string Command;

        public string Data;

        public CommandAndData(string command, string data)
        {
            Command = command;
            Data = data;
        }
    }
}
