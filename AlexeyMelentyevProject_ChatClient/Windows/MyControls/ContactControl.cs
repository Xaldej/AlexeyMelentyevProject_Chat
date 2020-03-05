using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlexeyMelentyev_chat_project.Windows.MyControls
{
    public partial class ContactControl : UserControl
    {
        public string Login { get; set; }

        public Guid Id { get; set; }

        public ContactControl(string login, Guid id)
        {
            Login = login;
            Id = id;

            InitializeComponent();

            ContactLogin_label.Text = Login;
        }

    }
}
