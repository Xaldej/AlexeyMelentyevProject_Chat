using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlexeyMelentyev_chat_project.Commands.ToServer
{
    public class GetConactList : Command
    {
        public override string Name => "GetConactList";

        public override void Execute(AmMessenger messenger, string data)
        {
            var command = "/" + Name.ToLower() + ":" + data;
            try
            {
                messenger.SendCommand(command);
            }
            catch
            {
                string errorMessage = "Cannot get contact list. Try to restart a messenger";
                string caption = "Error";
                MessageBox.Show(errorMessage, caption);
            }
        }
    }
}