using Commands;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlexeyMelentyev_chat_project.Commands.FromServer
{
    public class ServerError : Command
    {
        public override string Name => "ServerError";

        public override void Execute(IMessenger messenger, string data)
        {
            var message = "Server error\n" +
                "Try to restart messenger\n\n" +
                "Details:\n" + data;

            MessageBox.Show("Server Error", message);
        }
    }
}
