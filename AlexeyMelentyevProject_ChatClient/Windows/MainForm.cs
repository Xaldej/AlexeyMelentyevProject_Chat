﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlexeyMelentyev_chat_project.Windows.MyControls;
using AlexeyMelentyevProject_ChatServer.Data.Entities;

namespace AlexeyMelentyev_chat_project.Windows
{
    public partial class MainForm : Form
    {
        private AmMessenger Messenger { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void AM_Chat_Load(object sender, EventArgs e)
        {   
            Login();
        }

        private void Login()
        {
            var loginForm = new LoginForm();

            loginForm.LoginIsEntered += CreateMessenger;

            loginForm.ShowDialog();
        }

        private void CreateMessenger(string userLogin)
        {
            Messenger = new AmMessenger(userLogin);
            Messenger.MessageIsGotten += ShowGottenMessage;
            Messenger.ContactsAreUpdated += UpdateContacts;

            var thread = new Thread(Messenger.Process);
            thread.Start();
        }

        private void UpdateContacts(List<User> contacts)
        {
            Contacts_panel.Invoke(new Action(() => Contacts_panel.Controls.Clear()));

            foreach (var contact in contacts)
            {
                var contactControl = new ContactControl(contact) { Dock = DockStyle.Top };

                contactControl.ContactChosen += ChangeContact;

                Contacts_panel.Invoke(new Action(() => Contacts_panel.Controls.Add(contactControl)));
            }
        }

        private void ChangeContact(ContactControl contactControl)
        {
            //TO DO: update history
            var previousChosenControls = Contacts_panel.Controls.OfType<ContactControl>().Where(c=>c.BackColor == Color.Silver);

            foreach (var control in previousChosenControls)
            {
                control.BackColor = Color.Gainsboro;
            }

            Chat_panel.Enabled = true;
            contactControl.BackColor = Color.Silver;

            Messenger.ChosenUser = contactControl.User;
        }

        private void InputMessage_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TrySendMessage();
            }
        }

        private void Send_button_Click(object sender, EventArgs e)
        {
            TrySendMessage();
        }

        private void ShowGottenMessage(string message)
        {
            ChatHistory_richTextBox.Invoke(new Action(() => ChatHistory_richTextBox.SelectionAlignment = HorizontalAlignment.Left));
            ChatHistory_richTextBox.Invoke(new Action(() => ChatHistory_richTextBox.AppendText(message + "\n")));
        }

        private void TrySendMessage()
        {   
            var userInput = InputMessage_textBox.Text;

            var isUserInputCorrect = ValidateUserInput(userInput);

            if(isUserInputCorrect)
            {
                ShowMessage(userInput);
                try
                {
                    Messenger.SendMessage(userInput);
                }
                catch
                {
                    
                    ChatHistory_richTextBox.SelectionAlignment = HorizontalAlignment.Center;
                    ChatHistory_richTextBox.AppendText("------NO CONNECTION TO SERVER------\n" +
                                                       "message is not sent\n" +
                                                       "try to reconnect\n");
                }
            }
        }

        private void ShowMessage(string userInput)
        {
            ChatHistory_richTextBox.SelectionAlignment = HorizontalAlignment.Right;
            ChatHistory_richTextBox.AppendText(userInput + "\n");
            InputMessage_textBox.Clear();
        }

        private bool ValidateUserInput(string inputMessage)
        {
            var isMessageCorrect = false;

            if (inputMessage != string.Empty)
            {
                isMessageCorrect = true;
            }

            return isMessageCorrect;
        }

        private void AddContact_button_Click(object sender, EventArgs e)
        {
            var addContactForm = new AddContactForm();

            addContactForm.LoginToAddIsEntered += AddContact;

            addContactForm.ShowDialog();
        }

        private void AddContact(string userName)
        {
            var message = "/addcontact:" + userName;
            Messenger.ExecuteCommands(message);
        }
    }
}
