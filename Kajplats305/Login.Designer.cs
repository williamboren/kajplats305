namespace Kajplats305
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.newUserButton = new System.Windows.Forms.Button();
            this.FirstNameTextBox = new System.Windows.Forms.TextBox();
            this.LastNameTextBox = new System.Windows.Forms.TextBox();
            this.newUserDoneButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.Location = new System.Drawing.Point(12, 26);
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.Size = new System.Drawing.Size(159, 20);
            this.UsernameTextBox.TabIndex = 0;
            this.UsernameTextBox.Text = "Username...";
            // 
            // LoginButton
            // 
            this.LoginButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.LoginButton.Location = new System.Drawing.Point(177, 26);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 1;
            this.LoginButton.Text = "Logga In";
            this.LoginButton.UseVisualStyleBackColor = true;
            // 
            // newUserButton
            // 
            this.newUserButton.Location = new System.Drawing.Point(258, 26);
            this.newUserButton.Name = "newUserButton";
            this.newUserButton.Size = new System.Drawing.Size(108, 23);
            this.newUserButton.TabIndex = 2;
            this.newUserButton.Text = "Skapa Användare";
            this.newUserButton.UseVisualStyleBackColor = true;
            this.newUserButton.Click += new System.EventHandler(this.newUserButton_Click);
            // 
            // FirstNameTextBox
            // 
            this.FirstNameTextBox.Location = new System.Drawing.Point(12, 52);
            this.FirstNameTextBox.Name = "FirstNameTextBox";
            this.FirstNameTextBox.Size = new System.Drawing.Size(158, 20);
            this.FirstNameTextBox.TabIndex = 3;
            this.FirstNameTextBox.Text = "FirstName...";
            this.FirstNameTextBox.Visible = false;
            // 
            // LastNameTextBox
            // 
            this.LastNameTextBox.Location = new System.Drawing.Point(12, 78);
            this.LastNameTextBox.Name = "LastNameTextBox";
            this.LastNameTextBox.Size = new System.Drawing.Size(158, 20);
            this.LastNameTextBox.TabIndex = 4;
            this.LastNameTextBox.Text = "LastName...";
            this.LastNameTextBox.Visible = false;
            // 
            // newUserDoneButton
            // 
            this.newUserDoneButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.newUserDoneButton.Location = new System.Drawing.Point(12, 104);
            this.newUserDoneButton.Name = "newUserDoneButton";
            this.newUserDoneButton.Size = new System.Drawing.Size(75, 23);
            this.newUserDoneButton.TabIndex = 5;
            this.newUserDoneButton.Text = "Klar";
            this.newUserDoneButton.UseVisualStyleBackColor = true;
            this.newUserDoneButton.Visible = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 197);
            this.ControlBox = false;
            this.Controls.Add(this.newUserDoneButton);
            this.Controls.Add(this.LastNameTextBox);
            this.Controls.Add(this.FirstNameTextBox);
            this.Controls.Add(this.newUserButton);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.UsernameTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Login";
            this.Text = "Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Login_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button LoginButton;
        public System.Windows.Forms.TextBox UsernameTextBox;
        private System.Windows.Forms.Button newUserButton;
        private System.Windows.Forms.Button newUserDoneButton;
        public System.Windows.Forms.TextBox FirstNameTextBox;
        public System.Windows.Forms.TextBox LastNameTextBox;
    }
}