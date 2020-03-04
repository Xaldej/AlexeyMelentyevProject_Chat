using AlexeyMelentyev_chat_project.Commands.FromServer;
using AlexeyMelentyev_chat_project.Commands.ToServer;
using AlexeyMelentyevProject_ChatServer.Data.Entities;
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

        NetworkStream Stream { get; set; }

        public TcpClient TcpClient { get; set; }

        public List<Command> Commands { get; }

        public User User { get; set; }

        public string UserLogin { get; set; }

        public AmMessenger()
        {
        }

        public AmMessenger(string userLogin)
        {
            UserLogin = userLogin;

            Commands = new List<Command>()
            {
                new CorrectLogin(),

                new AddContact(),
                new Connect(),
                new GetConactList(),
                new Login(),
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
                Application.Exit();
            }
            

            var message = builder.ToString();

            if (CommandIdentifier.IsMessageACommand(message))
            {
                ExecuteCommands(message);
            }
            else
            {
                MessageIsGotten(message);
            }
        }

        public void ExecuteCommands(string message)
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
            Commands.Where(c => c.CheckIsCalled("/connect")).FirstOrDefault().Execute(this, string.Empty);

            using (Stream = TcpClient.GetStream())
            {
                ExecuteCommands($"/Login:{UserLogin}");

                while (true)
                {
                    ListenMessages();
                }
            }
        }

        public void SendCommand(string command)
        {
            byte[] data = Encoding.Unicode.GetBytes(command);
            Stream.Write(data, 0, data.Length);
        }
    }
}
