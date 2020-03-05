using AlexeyMelentyev_chat_project.Commands.FromServer;
using AlexeyMelentyev_chat_project.Commands.ToServer;
using AlexeyMelentyevProject_ChatServer.Data;
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
        public User User { get; set; }

        public List<User> UserContacts { get; set; }

        public TcpClient TcpClient { get; set; }

        public List<Command> Commands { get; }

        NetworkStream Stream { get; set; }

        public User ChosenUser { get; set; }

        public Action<string> MessageIsGotten;
        public Action<List<User>> ContactsAreUpdated;

        CorrectAddingContact CorrectAddingContact { get; set; }
        CorrectContactList CorrectContactList { get; set; }


        public AmMessenger()
        {
        }

        public AmMessenger(string userLogin)
        {
            User = new User()
            {
                Login = userLogin
            };

            CorrectAddingContact = new CorrectAddingContact();
            CorrectAddingContact.ContactListIsUpdated += UpdateContacts;

            CorrectContactList = new CorrectContactList();
            CorrectContactList.ContactListIsUpdated += UpdateContacts;


            UserContacts = new List<User>();

            Commands = new List<Command>()
            {
                CorrectAddingContact,
                CorrectContactList,
                new CorrectLogin(),
                new ServerError(),
                new ShowError(),

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

        public void SendMessage(string message)
        {
            var messageToUser = new MessageToUser()
            {
                FromUserId = User.Id,
                ToUserId = ChosenUser.Id,
                Text = message,
            };

            var command = "/sendmessagetouser:" + MessagesJsonParser.OneMessageToJson(messageToUser);

            byte[] data = Encoding.Unicode.GetBytes(command);
            Stream.Write(data, 0, data.Length);
        }

        public void Process()
        {
            Commands.Where(c => c.CheckIsCalled("/connect")).FirstOrDefault().Execute(this, string.Empty);

            using (Stream = TcpClient.GetStream())
            {
                ExecuteCommands($"/Login:{User.Login}");

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

        private void UpdateContacts()
        {
            ContactsAreUpdated(UserContacts);
        }
    }
}
