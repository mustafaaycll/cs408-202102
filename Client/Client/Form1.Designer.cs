namespace Client
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
            this.ipLabel = new System.Windows.Forms.Label();
            this.portLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.postLabel = new System.Windows.Forms.Label();
            this.postTextBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.allpostsButton = new System.Windows.Forms.Button();
            this.friendsListBox = new System.Windows.Forms.ListBox();
            this.friendRemoveButton = new System.Windows.Forms.Button();
            this.addFriendButton = new System.Windows.Forms.Button();
            this.friendUsernameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.myPostsButton = new System.Windows.Forms.Button();
            this.friendPostsButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.postIDTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.Location = new System.Drawing.Point(47, 30);
            this.ipLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(20, 13);
            this.ipLabel.TabIndex = 0;
            this.ipLabel.Text = "IP:";
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(38, 54);
            this.portLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(29, 13);
            this.portLabel.TabIndex = 1;
            this.portLabel.Text = "Port:";
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(9, 78);
            this.usernameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(58, 13);
            this.usernameLabel.TabIndex = 2;
            this.usernameLabel.Text = "Username:";
            // 
            // ipTextBox
            // 
            this.ipTextBox.Location = new System.Drawing.Point(71, 27);
            this.ipTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(152, 20);
            this.ipTextBox.TabIndex = 3;
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(71, 51);
            this.portTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(152, 20);
            this.portTextBox.TabIndex = 4;
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(71, 75);
            this.usernameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(152, 20);
            this.usernameTextBox.TabIndex = 5;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(149, 99);
            this.connectButton.Margin = new System.Windows.Forms.Padding(2);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(74, 32);
            this.connectButton.TabIndex = 6;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // disconnectButton
            // 
            this.disconnectButton.Enabled = false;
            this.disconnectButton.Location = new System.Drawing.Point(71, 99);
            this.disconnectButton.Margin = new System.Windows.Forms.Padding(2);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(74, 32);
            this.disconnectButton.TabIndex = 7;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(282, 27);
            this.richTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(252, 381);
            this.richTextBox.TabIndex = 8;
            this.richTextBox.Text = "";
            // 
            // postLabel
            // 
            this.postLabel.AutoSize = true;
            this.postLabel.Location = new System.Drawing.Point(279, 418);
            this.postLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.postLabel.Name = "postLabel";
            this.postLabel.Size = new System.Drawing.Size(31, 13);
            this.postLabel.TabIndex = 9;
            this.postLabel.Text = "Post:";
            // 
            // postTextBox
            // 
            this.postTextBox.Enabled = false;
            this.postTextBox.Location = new System.Drawing.Point(314, 415);
            this.postTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.postTextBox.Name = "postTextBox";
            this.postTextBox.Size = new System.Drawing.Size(144, 20);
            this.postTextBox.TabIndex = 10;
            // 
            // sendButton
            // 
            this.sendButton.Enabled = false;
            this.sendButton.Location = new System.Drawing.Point(462, 412);
            this.sendButton.Margin = new System.Windows.Forms.Padding(2);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(72, 25);
            this.sendButton.TabIndex = 11;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // allpostsButton
            // 
            this.allpostsButton.Enabled = false;
            this.allpostsButton.Location = new System.Drawing.Point(538, 24);
            this.allpostsButton.Margin = new System.Windows.Forms.Padding(2);
            this.allpostsButton.Name = "allpostsButton";
            this.allpostsButton.Size = new System.Drawing.Size(104, 25);
            this.allpostsButton.TabIndex = 12;
            this.allpostsButton.Text = "All Posts";
            this.allpostsButton.UseVisualStyleBackColor = true;
            this.allpostsButton.Click += new System.EventHandler(this.allpostsButton_Click);
            // 
            // friendsListBox
            // 
            this.friendsListBox.Enabled = false;
            this.friendsListBox.FormattingEnabled = true;
            this.friendsListBox.Location = new System.Drawing.Point(71, 165);
            this.friendsListBox.Name = "friendsListBox";
            this.friendsListBox.Size = new System.Drawing.Size(152, 173);
            this.friendsListBox.TabIndex = 13;
            this.friendsListBox.SelectedIndexChanged += new System.EventHandler(this.friendsListBox_SelectedIndexChanged);
            // 
            // friendRemoveButton
            // 
            this.friendRemoveButton.Enabled = false;
            this.friendRemoveButton.Location = new System.Drawing.Point(71, 343);
            this.friendRemoveButton.Margin = new System.Windows.Forms.Padding(2);
            this.friendRemoveButton.Name = "friendRemoveButton";
            this.friendRemoveButton.Size = new System.Drawing.Size(152, 32);
            this.friendRemoveButton.TabIndex = 14;
            this.friendRemoveButton.Text = "Remove Selected Friend";
            this.friendRemoveButton.UseVisualStyleBackColor = true;
            this.friendRemoveButton.Click += new System.EventHandler(this.friendRemoveButton_Click);
            // 
            // addFriendButton
            // 
            this.addFriendButton.Enabled = false;
            this.addFriendButton.Location = new System.Drawing.Point(71, 403);
            this.addFriendButton.Margin = new System.Windows.Forms.Padding(2);
            this.addFriendButton.Name = "addFriendButton";
            this.addFriendButton.Size = new System.Drawing.Size(152, 32);
            this.addFriendButton.TabIndex = 17;
            this.addFriendButton.Text = "Add Friend";
            this.addFriendButton.UseVisualStyleBackColor = true;
            this.addFriendButton.Click += new System.EventHandler(this.addFriendButton_Click);
            // 
            // friendUsernameTextBox
            // 
            this.friendUsernameTextBox.Enabled = false;
            this.friendUsernameTextBox.Location = new System.Drawing.Point(71, 379);
            this.friendUsernameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.friendUsernameTextBox.Name = "friendUsernameTextBox";
            this.friendUsernameTextBox.Size = new System.Drawing.Size(152, 20);
            this.friendUsernameTextBox.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 382);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Username:";
            // 
            // myPostsButton
            // 
            this.myPostsButton.Enabled = false;
            this.myPostsButton.Location = new System.Drawing.Point(538, 53);
            this.myPostsButton.Margin = new System.Windows.Forms.Padding(2);
            this.myPostsButton.Name = "myPostsButton";
            this.myPostsButton.Size = new System.Drawing.Size(104, 25);
            this.myPostsButton.TabIndex = 18;
            this.myPostsButton.Text = "My Posts";
            this.myPostsButton.UseVisualStyleBackColor = true;
            this.myPostsButton.Click += new System.EventHandler(this.myPostsButton_Click);
            // 
            // friendPostsButton
            // 
            this.friendPostsButton.Enabled = false;
            this.friendPostsButton.Location = new System.Drawing.Point(538, 82);
            this.friendPostsButton.Margin = new System.Windows.Forms.Padding(2);
            this.friendPostsButton.Name = "friendPostsButton";
            this.friendPostsButton.Size = new System.Drawing.Size(104, 25);
            this.friendPostsButton.TabIndex = 19;
            this.friendPostsButton.Text = "Friend\'s Posts";
            this.friendPostsButton.UseVisualStyleBackColor = true;
            this.friendPostsButton.Click += new System.EventHandler(this.friendPostsButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Enabled = false;
            this.deleteButton.Location = new System.Drawing.Point(462, 438);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(72, 25);
            this.deleteButton.TabIndex = 22;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // postIDTextBox
            // 
            this.postIDTextBox.Enabled = false;
            this.postIDTextBox.Location = new System.Drawing.Point(314, 441);
            this.postIDTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.postIDTextBox.Name = "postIDTextBox";
            this.postIDTextBox.Size = new System.Drawing.Size(144, 20);
            this.postIDTextBox.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(268, 444);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Post ID:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 473);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.postIDTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.friendPostsButton);
            this.Controls.Add(this.myPostsButton);
            this.Controls.Add(this.addFriendButton);
            this.Controls.Add(this.friendUsernameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.friendRemoveButton);
            this.Controls.Add(this.friendsListBox);
            this.Controls.Add(this.allpostsButton);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.postTextBox);
            this.Controls.Add(this.postLabel);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.ipTextBox);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.ipLabel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Label postLabel;
        private System.Windows.Forms.TextBox postTextBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Button allpostsButton;
        private System.Windows.Forms.ListBox friendsListBox;
        private System.Windows.Forms.Button friendRemoveButton;
        private System.Windows.Forms.Button addFriendButton;
        private System.Windows.Forms.TextBox friendUsernameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button myPostsButton;
        private System.Windows.Forms.Button friendPostsButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.TextBox postIDTextBox;
        private System.Windows.Forms.Label label2;
    }
}

