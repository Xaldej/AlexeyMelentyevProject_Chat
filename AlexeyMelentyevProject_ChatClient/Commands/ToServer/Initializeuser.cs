using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlexeyMelentyev_chat_project.Commands.ToServer
{
    public class Initializeuser : Command
    {
        public override string Name => "Initializeuser";

        public override void Execute(AmMessenger messenger, string data)
        {
            var command = "/" + Name.ToLower() + ":" + messenger.UserLogin;
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
