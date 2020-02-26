using System;
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
                Send_Message();
            }
        }

        private void Send_button_Click(object sender, EventArgs e)
        {
            Send_Message();
        }

        private void ShowGottenMessage(string message)
        {
            ChatHistory_richTextBox.SelectionAlignment = HorizontalAlignment.Left;
            ChatHistory_richTextBox.AppendText(message + "\n");
        }

        private void Send_Message()
        {
            var inputMessage = InputMessage_textBox.Text;

            if (inputMessage == string.Empty)
            {
                return;
            }

            if (Messenger.TrySendMessage(inputMessage, "temp"))
            {
                ChatHistory_richTextBox.SelectionAlignment = HorizontalAlignment.Right;
                ChatHistory_richTextBox.AppendText(inputMessage + "\n");
                InputMessage_textBox.Clear();
            }
        }

    }
}
