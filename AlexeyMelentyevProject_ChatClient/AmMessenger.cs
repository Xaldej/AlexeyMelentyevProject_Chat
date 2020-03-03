using AlexeyMelentyev_chat_project.Commands;
using AlexeyMelentyev_chat_project.Commands.FromServer;
using AlexeyMelentyev_chat_project.Commands.ToServer;
using Commands;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlexeyMelentyev_chat_project
{
    public class AmMessenger : IMessenger
    {
        public Action<string> MessageIsGotten;

        public ClientSettings ClientSettings { get; set; }

        public string UserLogin { get; }

        NetworkStream Stream { get; set; }

        TcpClient TcpClient { get; set; }

        public List<Command> Commands { get; }

        public AmMessenger()
        {
        }

        public AmMessenger(ClientSettings clientSettings, string userLogin)
        {
            ClientSettings = clientSettings;
            UserLogin = userLogin;

            Commands = new List<Command>()
            {
                new CorrectLogin(),


                new AddContact(),
                new GetConactList(),
            };
        }

        public void ListenMessages()
        {
            byte[] data = new byte[TcpClient.ReceiveBufferSize];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            try
            {
                do
                {
                    bytes = Stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (Stream.DataAvailable);
            }
            catch
            {
                string errorMessage = "Connection lost. Try to restart a messenger";
                string caption = "Error";
                MessageBox.Show(errorMessage, caption);
            }
            

            var message = builder.ToString();

            if (CommandIdentifier.IsMessageACommand(message))
            {
                ExecuteCommand(message);
            }
            else
            {
                MessageIsGotten(message);
            }
        }

        public void ExecuteCommand(string message)
        {
            var commandAndData = CommandIdentifier.GetCommandAndDataFromMessage(message);

            var commandsToExecute = Commands.Where(c => c.CheckIsCalled(commandAndData.Command));

            foreach (var command in commandsToExecute)
            {
                command.Execute(this, commandAndData.Data);
            }
        }

        public void SendMessage(string message, Guid contactId)
        {   
            byte[] data = Encoding.Unicode.GetBytes(message);
            Stream.Write(data, 0, data.Length);
        }

        public void Process()
        {
            Connect();

            using (Stream = TcpClient.GetStream())
            {
                InitializeUser();

                while (true)
                {
                    ListenMessages();
                }
            }
        }

        private void InitializeUser()
        {
            SendUserLogin();
        }

        public void GetContactList()
        {
            

        }

        public void SendCommand(string command)
        {
            byte[] data = Encoding.Unicode.GetBytes(command);
            Stream.Write(data, 0, data.Length);
        }

        private void SendUserLogin()
        {
            var command = "/initializeuser:" + UserLogin;
            try
            {
                SendCommand(command);
            }
            catch
            {
                string message = "Cannot initialize the user. App will be closed";
                string caption = "Error";
                MessageBox.Show(message, caption);
                Application.Exit();
            }
        }

        private void Connect()
        {
            TcpClient = new TcpClient();
            try
            {
                TcpClient.Connect(ClientSettings.EndPoint);
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
