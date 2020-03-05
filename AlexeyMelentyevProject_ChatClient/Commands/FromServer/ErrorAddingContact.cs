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
    public class ErrorAddingContact : Command
    {
        public override string Name => "ErrorAddingContact";

        public override void Execute(IMessenger messenger, string data)
        {   
            MessageBox.Show("Error", data);
        }
    }
}
