using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlexeyMelentyev_chat_project.Commands.ToServer
{
    public class Connect : Command
    {
        public override string Name => "Connect";

        public override void Execute(AmMessenger messenger, string data)
        {
            var tcpClient = messenger.TcpClient = new TcpClient();
            var endPoint = messenger.ClientSettings.EndPoint;
            try
            {
                tcpClient.Connect(endPoint);
            }
            catch
            {
                string message = "Cannot connect to server";
                string caption = "Error";
                MessageBox.Show(message, caption);
                Application.Exit();
            }
        }
    }
}
