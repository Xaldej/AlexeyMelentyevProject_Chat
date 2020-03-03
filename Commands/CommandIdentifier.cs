using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public static class CommandIdentifier
    {
        public static bool IsMessageACommand(string command)
        {
            var result = false;

            if (command == string.Empty)
            {
                return false;
            }

            if (command[0] == '/')
            {
                result = true;
            }

            return result;
        }

        public static CommandAndData GetCommandAndDataFromMessage(string fullCommand)
        {
            string commandName = string.Empty;
            string commandData = string.Empty;

            var wasDividerInLine = false;

            foreach (var ch in fullCommand)
            {
                if (ch == ':')
                {
                    wasDividerInLine = true;
                }
                else
                {
                    if (wasDividerInLine)
                    {
                        commandData += ch;
                    }
                    else
                    {
                        commandName += ch;
                    }
                }
            }

            return new CommandAndData(commandName, commandData);
        }
    }
}
