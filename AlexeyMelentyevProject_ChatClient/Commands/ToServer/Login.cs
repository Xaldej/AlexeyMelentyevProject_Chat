using Commands;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlexeyMelentyev_chat_project.Commands.ToServer
{
    public class Login : Command
    {
        public override string Name => "Login";

        public override void Execute(IMessenger messenger, string data)
        {
            var command = "/" + Name.ToLower() + ":" + data;
            try
            {
                messenger.SendCommand(command);
            }
            catch
            {
                string message = "Cannot initialize the user. App will be closed";
                string caption = "Error";
                MessageBox.Show(message, caption);
                Application.Exit();
            }
        }
    }
}
