using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Kajplats305
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Kajplats305 - ";
        } 
        // set up a connection to the database
        static string connstring = "server=192.168.250.103;user id=kajplats;password=305;database=kajplats305";
        static string connstring2 = "server=127.0.0.1;user id=root;database=kajplats305";
        MySqlConnection conn = new MySqlConnection(connstring2);
        string localUsername;

        private void Form1_Load(object sender, EventArgs e)
        {
            Login loginDialog = new Login();
            // show the form as a dialog and determines the return value
            if (loginDialog.ShowDialog(this) == DialogResult.OK)
            {
                // if it's OK then save the username and add the username to the title text
                this.localUsername = loginDialog.UsernameTextBox.Text;
                this.Text += this.localUsername;
            }
            else
            {
                // otherwise just show an error (technically it can't get here since theres no cancel button and I removed the exit button from the login form
                MessageBox.Show("Error");
            }
            // release all resources used by the form
            loginDialog.Dispose();

            LoadMessages(); // automatically load new messages after logging in
        }

        private void LoadMessages()
        {
            conn.Open(); // open a connection to the server
            // create a new mysql command
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn; // set the connection for the command to the connection we created earlier
            command.CommandText = "SELECT * FROM `Messages` WHERE `ToUser` = @username;"; // create the command
            command.Prepare(); // prepare a version of the command on the server (speeds up execution)
            command.Parameters.AddWithValue("@username", localUsername); // add a value to the parameter in the command

            MySqlDataReader reader = command.ExecuteReader(); // execute the command and store the return values in a datareader

            while (reader.Read()) // while there's still data to retrieve
            {
                string from = reader["FromUser"].ToString(); // store the senders username in a string for easier access

                // if the tab doesn't already exist, create it
                if (tabControl1.TabPages[from] == null)
                {
                    tabControl1.TabPages.Add(from, from); // add a tab with the tabname and text set to the fromuser string
                }

                TextBox message = new TextBox(); // create a new text box
                message.Text = reader["Message"].ToString(); // set the text of the textbox to the received message
                message.Multiline = true;
                message.Enabled = false; // disable input from the user
                message.BackColor = this.BackColor; // set the textbox backgroundcolor to the same as the main window
                message.WordWrap = true;
                message.Width = tabControl1.Width / 2; // set the width to 50% of the tab

                tabControl1.TabPages[from].Controls.Add(message); // add the textbox to the tab
            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            // toggle visibillity of controls for sending messages based on selected tab
            if (tabControl1.SelectedTab.Name == "Start")
            {
                messageInput.Visible = false;
                sendMessageButton.Visible = false;
            }
            else
            {
                messageInput.Visible = true;
                sendMessageButton.Visible = true;
            }
        }

        private void getMessagesButton_Click(object sender, EventArgs e)
        {
            LoadMessages();
        }
    }
}
