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
    public partial class LoginForm : Form
    {
        public Action<string> LoginIsEntered;

        bool isClosedByUser = true;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void Login_button_Click(object sender, EventArgs e)
        {
            LoginEntered();
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(isClosedByUser)
            {
                Application.Exit();
            }
        }

        private void Login_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginEntered();
            }
        }

        private void LoginEntered()
        {
            string userName = string.Empty;

            userName = Login_textBox.Text;

            if (userName == string.Empty)
            {
                return;
            }

            LoginIsEntered(userName);

            isClosedByUser = false;

            this.Close();
        }
    }
}
