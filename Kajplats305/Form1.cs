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
using System.Text.RegularExpressions;

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
        string localUsername, localFirstName, localLastName;

        private void Form1_Load(object sender, EventArgs e)
        {
            LogIn();
            LoadMessages(true); // automatically load new messages after logging in
            LoadUsers(); // automatically load users after logging in
        }

        private void LogIn()
        {
            Login loginDialog = new Login();
            conn.Open();
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn;
            DialogResult result = loginDialog.ShowDialog(this);
            // show the form as a dialog and determines the return value
            // DialogResults are a bit limited so I chose to use OK for logging in with an existing user and YES for creating a new one and logging in
            if (result == DialogResult.OK)
            {
                // login
                this.localUsername = loginDialog.UsernameTextBox.Text;
                
                command.CommandText = "SELECT `FirstName`, `LastName` FROM `Users` WHERE `Username` = @username;";
                command.Prepare();
                command.Parameters.AddWithValue("@username", this.localUsername);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        this.localFirstName = reader["FirstName"].ToString();
                        this.localLastName = reader["LastName"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Användaren finns inte.");
                    conn.Close();
                    loginDialog.Dispose();
                    LogIn();
                }
            }
            else if (result == DialogResult.Yes)
            {
                // create new user
                this.localUsername = loginDialog.UsernameTextBox.Text;
                this.localLastName = loginDialog.LastNameTextBox.Text;
                this.localFirstName = loginDialog.FirstNameTextBox.Text;
                command.CommandText = "SELECT 1 FROM `Users` WHERE `Username` = @username;";
                command.Prepare();
                command.Parameters.AddWithValue("@username", this.localUsername);
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MySqlConnection con2 = (MySqlConnection)conn.Clone();
                    MySqlCommand command2 = new MySqlCommand();
                    con2.Open();
                    command2.Connection = con2;
                    command2.CommandText = "INSERT INTO `Users` (`Username`, `FirstName`, `LastName`) VALUES (@username, @firstName, @lastname);";
                    command2.Prepare();
                    command2.Parameters.AddWithValue("@username", this.localUsername);
                    command2.Parameters.AddWithValue("@firstName", this.localFirstName);
                    command2.Parameters.AddWithValue("@lastName", this.localLastName);
                    command2.ExecuteNonQuery();
                    con2.Close();
                    command2.Dispose();
                }
                else
                {
                    MessageBox.Show("Användaren finns redan");
                    conn.Close();
                    loginDialog.Dispose();
                    LogIn();
                }
                
            }
            else
            {
                // (technically it can't get here since theres no cancel button and I removed the exit button from the login form, hence just a lazy error message without any real information)
                MessageBox.Show("Error");
                conn.Close();
                loginDialog.Dispose();
                LogIn();
            }
            // release all resources used by the form
            loginDialog.Dispose();
            conn.Close();
            this.Text += this.localUsername;
        }

        private void LoadMessages(bool allChats) // false to load messages from only the open tab and true to load all messages sent to the user
        {
            conn.Open(); // open a connection to the server
            // create a new mysql command
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn; // set the connection for the command to the connection we created earlier
            command.CommandText = allChats ? "SELECT * FROM `Messages` WHERE `ToUser` = @username AND `Received` = 0;" : "SELECT * FROM `Messages` WHERE `ToUser` = @username AND `FromUser` = @fromUser AND `Received` = 0;"; // create the command, one version is for loading messages from all users and the other for loading messages from one user only (active chat tab), conditional statements are awesome
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
            MySqlConnection conn2 = (MySqlConnection)conn.Clone();
            conn2.Open();
            commandResponse.Connection = conn2;
            commandResponse.CommandText = "UPDATE `Messages` SET `Received` = 1, `ReceivedTime` = @currentTime WHERE `ID` = @id";
            commandResponse.Prepare();
            commandResponse.Parameters.AddWithValue("@currentTime", DateTime.Now);
            commandResponse.Parameters.AddWithValue("@id", 0);

            MySqlDataReader reader = command.ExecuteReader(); // execute the command and store the return values in a datareader

            while (reader.Read()) // while there's still data to retrieve
            {
                string from = reader["FromUser"].ToString(); // store the senders username in a string for easier access

                // if the tab doesn't already exist, create it /*(add a new tab for each message for now, need to figure out positioning...)*/
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
                message.Width = tabControl1.Width - 10; // set the width to fill the entire tab
                Point msgLocation = new Point(0, 0), prevLocation; // the location to put the message at and one variable for the foreach loop further down

                // if there are any children (textboxes) in the tabpage
                if (tabControl1.TabPages[from].HasChildren)
                {
                    // Regex reg = new Regex("[0-9]");
                    // temp solution '_>'
                    Control[] children = new Control[100];

                    /***********************************************************************************************************************
                    I'm confused, it doesn't find any other TextBoxes then the first two, what the hell is wrong?????SDLKFJJSDKFJLSDKFJSDLKF
                    ddlskfjdklsfjsdlkjfsdklfjsdlkfjsd whyyyyyyyyyyyyyyyyyyyyyyyyyyyy
                    what am I overlooking??????
                    a
                    a
                    a
                    a
                    a
                    a
                    Och mormor gick bort inatt (11 dec) så kan inte fixa det eller implementera loggat ut och admin läge...
                    ************************************************************************************************************************/

                    // add all current controls from the appropriate tab to an array
                    for (int i = 0; i < tabControl1.TabPages[from].Controls.Count; i++)
                    {
                        Control[] tempArr = tabControl1.TabPages[from].Controls.Find(i.ToString(), true);
                        foreach (Control temp in tempArr)
                        {
                            children[i] = temp;
                        }
                    }

                    for (int i = 0; i < children.Length; i++)
                    {
                        if (children[i] == null) // avoid null exceptions
                        {
                            break;
                        }
                        else
                        {
                            prevLocation = children[i].Location;
                            if (children[i].Location.Y > prevLocation.Y || children[i].Location.Y == prevLocation.Y)
                            {
                                msgLocation.Y = children[i].Height + 1; // move the new textbox below the current child
                                message.Name = int.Parse(children[i].Name + 1).ToString();
                            }
                        }
                        
                    }
                    message.Location = msgLocation; // give the textbox the new location
                }
                else
                {
                    message.Name = "0";
                    message.Location = msgLocation;
                }

                message.BringToFront();
                tabControl1.TabPages[from].Controls.Add(message); // add the textbox to the tab

                // update the receivedtime and received values for each message retreived from the server 
                commandResponse.Parameters["@currentTime"].Value = DateTime.Now;
                commandResponse.Parameters["@id"].Value = reader["ID"];
                // commandResponse.ExecuteNonQuery();
            }

            conn.Close(); // close the connection to the database
            conn2.Close();
            conn2.Dispose();
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
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

        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            string message = messageInput.Text; // store the message to be sent in a variable for easier access
            conn.Open();
            MySqlCommand command = new MySqlCommand();
            command.Connection = conn;
            command.CommandText = "INSERT INTO `Messages` (`FromUser`, `ToUser`, `Message`, `SentTime`, `ReceivedTime`, `Received`) VALUES (@from, @to, @message, @time, NULL, 0);";
            command.Prepare();
            command.Parameters.AddWithValue("@from", this.localUsername);
            command.Parameters.AddWithValue("@to", tabControl1.SelectedTab.Name);
            command.Parameters.AddWithValue("@message", message);
            command.Parameters.AddWithValue("@time", DateTime.Now);
            try
            {
                command.ExecuteNonQuery();
                TextBox sentMessage = new TextBox();
                sentMessage.Text = message;
                sentMessage.Multiline = true;
                sentMessage.Enabled = false; // disable input from the user
                sentMessage.BackColor = this.BackColor; // set the textbox backgroundcolor to the same as the main window
                sentMessage.Visible = true;
                sentMessage.WordWrap = true;
                sentMessage.Width = tabControl1.Width - 10; // set the width to the same as the tab
                sentMessage.Location = new Point(0, 140);
                sentMessage.TextAlign = HorizontalAlignment.Right;
                tabControl1.SelectedTab.Controls.Add(sentMessage);
                tabControl1.TabPages[tabControl1.SelectedTab.Name].Controls.Add(sentMessage);
                sentMessage.Show();
                sentMessage.BringToFront();
                messageInput.Clear();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                conn.Close();
            }
        }

        private void getUsersButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab.Controls.Clear(); // remove message history
            LoadUsers();
        }

        private void StartChatButton_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add(Users.Items[Users.SelectedIndex].ToString(), Users.Items[Users.SelectedIndex].ToString()); // add a tab with the selected user
            tabControl1.SelectTab(Users.Items[Users.SelectedIndex].ToString()); // select the recently created tab
            LoadMessages(false); // get any unread messages from the user
        }
    }
}
