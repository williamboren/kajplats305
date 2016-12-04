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

            LoadMessages(true); // automatically load new messages after logging in
            LoadUsers(); // automatically load users after logging in
        }

        private void LoadMessages(bool allChats)
        {
            conn.Open(); // open a connection to the server
            // create a new mysql command
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn; // set the connection for the command to the connection we created earlier
            command.CommandText = allChats ? "SELECT * FROM `Messages` WHERE `ToUser` = @username AND `Received` = 0;" : "SELECT * FROM `Messages` WHERE `ToUser` = @username AND `FromUser` = @fromUser AND `Received` = 0;"; // create the command, one version is for loading messages from all users and the other for loading messages from one user only (active chat tab)
            command.Prepare(); // prepare a version of the command on the server (speeds up execution)
            command.Parameters.AddWithValue("@username", localUsername); // add a value to the parameter in the command
            try
            {
                if (!allChats && tabControl1.SelectedTab.Name != "Start") command.Parameters.AddWithValue("@fromUser", tabControl1.SelectedTab.Name);
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show("Var god och öppna en chatt för att hämta meddelanden.", "Error");
            }

            // update the receivedtime and received in the database
            MySqlCommand commandResponse = new MySqlCommand();
            commandResponse.Connection = conn;
            commandResponse.CommandText = "UPDATE `Messages` SET `Received` = 1, `ReceivedTime = @currentTime WHERE `ID` = @id";
            commandResponse.Prepare();

            MySqlDataReader reader = command.ExecuteReader(); // execute the command and store the return values in a datareader

            while (reader.Read()) // while there's still data to retrieve
            {
                string from = reader["FromUser"].ToString(); // store the senders username in a string for easier access

                // if the tab doesn't already exist, create it (add a new tab for each message for now, need to figure out positioning...)
               // if (tabControl1.TabPages[from] == null)
                //{
                    tabControl1.TabPages.Add(from, from); // add a tab with the tabname and text set to the fromuser string
                //}

                TextBox message = new TextBox(); // create a new text box
                message.Text = reader["Message"].ToString(); // set the text of the textbox to the received message
                message.Multiline = true;
                message.Enabled = false; // disable input from the user
                message.BackColor = this.BackColor; // set the textbox backgroundcolor to the same as the main window
                message.WordWrap = true;
                message.Width = tabControl1.Width; // set the width to the same as the tab

                tabControl1.TabPages[from].Controls.Add(message); // add the textbox to the tab

                
                commandResponse.Parameters.AddWithValue("@currentTime", DateTime.Now);
                commandResponse.Parameters.AddWithValue("@ID", reader["ID"]);
                commandResponse.ExecuteNonQuery();
            }

            conn.Close(); // close the connection to the database
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(tabControl1.SelectedTab.Name);
            // toggle visibillity of controls for sending messages based on selected tab
            if (tabControl1.SelectedTab.Name == "Start")
            {
                messageInput.Enabled = false;
                sendMessageButton.Enabled = false;
            }
            else
            {
                messageInput.Enabled = true;
                sendMessageButton.Enabled = true;
            }
        }

        private void LoadUsers()
        {
            // Check the comments for LoadMessages() for explanations on what's happening #lazy
            conn.Open();
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT `Username` FROM `Users`";
            command.Prepare();
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Users.Items.Add(reader["Username"]); // add each username to the combobox
            }

            conn.Close();
        }

        private void getMessagesButton_Click(object sender, EventArgs e)
        {
            LoadMessages(true);
        }

        private void getUsersButton_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void StartChatButton_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add(Users.Items[Users.SelectedIndex].ToString(), Users.Items[Users.SelectedIndex].ToString()); // add a tab with the selected user
            tabControl1.SelectTab(Users.Items[Users.SelectedIndex].ToString()); // select the recently created tab
            LoadMessages(false); // get any messages from the user
        }
    }
}
