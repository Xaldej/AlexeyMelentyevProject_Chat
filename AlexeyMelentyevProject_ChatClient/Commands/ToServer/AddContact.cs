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
    public class AddContact : Command
    {
        public override string Name => "AddContact";

        public override void Execute(IMessenger messenger, string data)
        {
            var command = "/" + Name.ToLower() + ":" + data;
            try
            {
                messenger.SendCommand(command);
            }
            catch
            {
                string errorMessage = "Error adding contact. Try again";
                string caption = "Error";
                MessageBox.Show(errorMessage, caption);
            }
        }
    }
}
