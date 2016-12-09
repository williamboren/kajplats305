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
            if (LoginButton.Enabled)
            {
                // if the user didn't enter a username, cancel closing the form and show a messagebox
                if (UsernameTextBox.Text == string.Empty)
                {
                    e.Cancel = true;
                    MessageBox.Show("Please enter your username");
                }
            }
            else
            {
                if (UsernameTextBox.Text == string.Empty || FirstNameTextBox.Text == string.Empty || LastNameTextBox.Text == string.Empty)
                {
                    e.Cancel = true;
                    MessageBox.Show("Please enter your Username, First Name and Last Name");
                }
            }
            
        }

        private void newUserButton_Click(object sender, EventArgs e)
        {
            LoginButton.Enabled = false;
            newUserDoneButton.Visible = true;
            FirstNameTextBox.Visible = true;
            LastNameTextBox.Visible = true;
        }
    }
}
