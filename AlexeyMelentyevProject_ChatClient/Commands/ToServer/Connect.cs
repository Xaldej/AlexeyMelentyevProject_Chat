using Commands;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlexeyMelentyev_chat_project.Commands.ToServer
{
    public class Connect : Command
    {
        public override string Name => "Connect";

        public override void Execute(IMessenger messenger, string data)
        {
            var tcpClient = messenger.TcpClient = new TcpClient();

            //todo: ger from config
            var Ip = "127.0.0.1";
            var Port = 8888;

            IPAddress IpAddr = IPAddress.Parse(Ip);
            IPEndPoint EndPoint = new IPEndPoint(IpAddr, Port);

            try
            {
                tcpClient.Connect(EndPoint);
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
