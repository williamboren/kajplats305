using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kajplats305
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            // if the user didn't enter a username, cancel closing the form and show a messagebox
            if (UsernameTextBox.Text == string.Empty)
            {
                e.Cancel = true;
                MessageBox.Show("Please enter your username");
            }
        }
    }
}
