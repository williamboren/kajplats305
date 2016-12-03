namespace Kajplats305
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hämtaMeddelandenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggaUtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Start = new System.Windows.Forms.TabPage();
            this.messageInput = new System.Windows.Forms.TextBox();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this.getMessagesButton = new System.Windows.Forms.Button();
            this.administratorlägeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menyToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(838, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menyToolStripMenuItem
            // 
            this.menyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hämtaMeddelandenToolStripMenuItem,
            this.administratorlägeToolStripMenuItem,
            this.loggaUtToolStripMenuItem});
            this.menyToolStripMenuItem.Name = "menyToolStripMenuItem";
            this.menyToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.menyToolStripMenuItem.Text = "Meny";
            // 
            // hämtaMeddelandenToolStripMenuItem
            // 
            this.hämtaMeddelandenToolStripMenuItem.Name = "hämtaMeddelandenToolStripMenuItem";
            this.hämtaMeddelandenToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.hämtaMeddelandenToolStripMenuItem.Text = "Hämta Meddelanden";
            this.hämtaMeddelandenToolStripMenuItem.Click += new System.EventHandler(this.getMessagesButton_Click);
            // 
            // loggaUtToolStripMenuItem
            // 
            this.loggaUtToolStripMenuItem.Name = "loggaUtToolStripMenuItem";
            this.loggaUtToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.loggaUtToolStripMenuItem.Text = "Logga Ut";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Start);
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(814, 375);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.TabIndexChanged += new System.EventHandler(this.tabControl1_TabIndexChanged);
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(4, 22);
            this.Start.Name = "Start";
            this.Start.Padding = new System.Windows.Forms.Padding(3);
            this.Start.Size = new System.Drawing.Size(806, 349);
            this.Start.TabIndex = 0;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            // 
            // messageInput
            // 
            this.messageInput.Location = new System.Drawing.Point(16, 405);
            this.messageInput.Multiline = true;
            this.messageInput.Name = "messageInput";
            this.messageInput.Size = new System.Drawing.Size(571, 38);
            this.messageInput.TabIndex = 2;
            this.messageInput.Visible = false;
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Location = new System.Drawing.Point(594, 408);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(112, 32);
            this.sendMessageButton.TabIndex = 3;
            this.sendMessageButton.Text = "Skicka Meddelande";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Visible = false;
            // 
            // getMessagesButton
            // 
            this.getMessagesButton.Location = new System.Drawing.Point(712, 408);
            this.getMessagesButton.Name = "getMessagesButton";
            this.getMessagesButton.Size = new System.Drawing.Size(114, 32);
            this.getMessagesButton.TabIndex = 0;
            this.getMessagesButton.Text = "Hämta Meddelanden";
            this.getMessagesButton.UseVisualStyleBackColor = true;
            this.getMessagesButton.Click += new System.EventHandler(this.getMessagesButton_Click);
            // 
            // administratorlägeToolStripMenuItem
            // 
            this.administratorlägeToolStripMenuItem.Name = "administratorlägeToolStripMenuItem";
            this.administratorlägeToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.administratorlägeToolStripMenuItem.Text = "Administratorläge";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 454);
            this.Controls.Add(this.getMessagesButton);
            this.Controls.Add(this.sendMessageButton);
            this.Controls.Add(this.messageInput);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hämtaMeddelandenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loggaUtToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Start;
        private System.Windows.Forms.TextBox messageInput;
        private System.Windows.Forms.Button sendMessageButton;
        private System.Windows.Forms.Button getMessagesButton;
        private System.Windows.Forms.ToolStripMenuItem administratorlägeToolStripMenuItem;
    }
}

