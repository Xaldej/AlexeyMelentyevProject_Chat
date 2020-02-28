﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlexeyMelentyev_chat_project.Windows
{
    public partial class AM_Chat : Form
    {
        private AmMessenger Messenger;

        public AM_Chat()
        {
            InitializeComponent();
        }

        private void AM_Chat_Load(object sender, EventArgs e)
        {
            Messenger = new AmMessenger();
            Messenger.MessageIsGotten += ShowGottenMessage;
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
            ChatHistory_richTextBox.SelectionAlignment = HorizontalAlignment.Left;
            ChatHistory_richTextBox.AppendText(message + "\n");
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
                    Messenger.SendMessage(userInput, "temp");
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
    }
}
