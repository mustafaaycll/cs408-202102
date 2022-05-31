using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    public partial class Form1 : Form
    {
        bool terminating = false;
        bool connected = false;
        bool unsuccesfull = false;
        Socket clientSocket;
        string username;
        List<string> friendList = new List<string>();
        string selectedFriendToBeRemoved;
        

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }



        //Connect button*******************************************************
        private void connectButton_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = ipTextBox.Text;

            int portNum;
            if (Int32.TryParse(portTextBox.Text, out portNum))
            {
                try
                {
                    clientSocket.Connect(IP, portNum);
                    connected = true;
                    username = usernameTextBox.Text;

                    //Add "U" prefix to username for login request, send to server
                    string toBeSent = "U" + username;
                    Byte[] buffer = Encoding.Default.GetBytes(toBeSent);
                    clientSocket.Send(buffer);
                    
                    //Directly start the recieve thread
                    Thread receiveThread = new Thread(Receive);
                    receiveThread.Start();

                }
                catch
                {
                    richTextBox.AppendText("Could not connect to the server!\n");
                }
            }
            else
            {
                richTextBox.AppendText("Check the port\n");
            }
        }
        


        //Recieve from server*******************************
        private void Receive()
        {
            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    clientSocket.Receive(buffer);

                    //Write incoming message into 'message' string
                    string message = Encoding.Default.GetString(buffer);
                    message = message.Substring(0, message.IndexOf("\0"));

                    //First char of the response is the response type
                    string response = message.Substring(0, 1);

                    //Approve response
                    if(response == "A")
                    {

                        //If the user is not approved by the server because the username does not exist in the database
                        if (message == "Ainvalid")
                        {
                            richTextBox.AppendText("Please enter a valid username.\n");
                            unsuccesfull = true;
                        }

                        //If the user tried to be added as a friend does not exists
                        if (message.Contains("AinvalidFriendName"))
                        {
                            richTextBox.AppendText("Specified user name: " + message.Substring(message.IndexOf('*') + 1) + " is not a valid username.\n");
                        }

                        //If friend addition succeeded
                        else if (message.Contains("AFriendAdded"))
                        {
                            richTextBox.AppendText(message.Substring(message.IndexOf('*') + 1) + " added as a friend.\n");
                        }

                        //If friend removal succeeded
                        else if (message.Contains("AFriendRemoved"))
                        {
                            richTextBox.AppendText(message.Substring(message.IndexOf('*') + 1) + " removed from friends list.\n");
                        }


                        //If the user is not approved by the server because he/she is already online
                        else if (message == "AalreadyOnline")
                        {
                            richTextBox.AppendText("This user is currently online\n");
                            unsuccesfull = true;
                        }

                        //If the user is approved by the server
                        else if (message == "Aapproved")
                        {
                            richTextBox.AppendText("Hello " + username + "! you are connected to the server\n");

                            ////Enable/disable buttons and textboxs
                            ipTextBox.Enabled = false;
                            portTextBox.Enabled = false;
                            usernameTextBox.Enabled = false;
                            connectButton.Enabled = false;
                            disconnectButton.Enabled = true;
                            postTextBox.Enabled = true;
                            sendButton.Enabled = true;
                            allpostsButton.Enabled = true;
                            friendsListBox.Enabled = true;
                            if (selectedFriendToBeRemoved != null)
                            {
                                friendRemoveButton.Enabled = true;
                            }
                            friendUsernameTextBox.Enabled = true;
                            addFriendButton.Enabled = true;
                            postIDTextBox.Enabled = true;
                            deleteButton.Enabled = true;
                            myPostsButton.Enabled = true;
                            friendPostsButton.Enabled = true;
                        }
                    }

                    //All posts response
                    else if(response == "P")
                    {

                        //Checks the third char to see if there are any posts to show
                        if(message.Substring(2,1) == "0")
                        {
                            richTextBox.AppendText("There are no posts to show.\n");
                        }
                        else
                        {

                            //Checks if the incoming post is the first post in this request
                            if(message.Substring(1,1) == "0")
                            {

                                //Prints the post on the rich text box
                                richTextBox.AppendText("\n");
                                richTextBox.AppendText("Showing all posts from clients:\n");
                                richTextBox.AppendText("\n");
                                richTextBox.AppendText("Username:");
                                richTextBox.AppendText(message.Substring(3, message.IndexOf("---") - 3));
                                richTextBox.AppendText("\n");
                                richTextBox.AppendText("Post ID: ");
                                richTextBox.AppendText(message.Substring(message.IndexOf("---") + 3, message.IndexOf("+++") - message.IndexOf("---") - 3));
                                richTextBox.AppendText("\n");
                                richTextBox.AppendText("Post: ");
                                richTextBox.AppendText(message.Substring(message.IndexOf("+++") + 3, message.IndexOf("***") - message.IndexOf("+++") - 3));
                                richTextBox.AppendText("\n");
                                richTextBox.AppendText("Time: ");
                                richTextBox.AppendText(message.Substring(message.IndexOf("***") + 3, 4));
                                richTextBox.AppendText("-");
                                richTextBox.AppendText(message.Substring(message.IndexOf("***") + 7, 2));
                                richTextBox.AppendText("-");
                                richTextBox.AppendText(message.Substring(message.IndexOf("***") + 9, 2));
                                richTextBox.AppendText("T");
                                richTextBox.AppendText(message.Substring(message.IndexOf("***") + 11, 2));
                                richTextBox.AppendText(":");
                                richTextBox.AppendText(message.Substring(message.IndexOf("***") + 13, 2));
                                richTextBox.AppendText(":");
                                richTextBox.AppendText(message.Substring(message.IndexOf("***") + 15, 2));
                                richTextBox.AppendText("\n");
                            }

                            //If the incoming post is not the first one in the request
                            else
                            {
                                //Prints the post on the rich text box
                                richTextBox.AppendText("\n");
                                richTextBox.AppendText("Username:");
                                richTextBox.AppendText(message.Substring(3, message.IndexOf("---") - 3));
                                richTextBox.AppendText("\n");
                                richTextBox.AppendText("Post ID: ");
                                richTextBox.AppendText(message.Substring(message.IndexOf("---") + 3, message.IndexOf("+++") - message.IndexOf("---") - 3));
                                richTextBox.AppendText("\n");
                                richTextBox.AppendText("Post: ");
                                richTextBox.AppendText(message.Substring(message.IndexOf("+++") + 3, message.IndexOf("***") - message.IndexOf("+++") - 3));
                                richTextBox.AppendText("\n");
                                richTextBox.AppendText("Time: ");
                                richTextBox.AppendText(message.Substring(message.IndexOf("***") + 3, 4));
                                richTextBox.AppendText("-");
                                richTextBox.AppendText(message.Substring(message.IndexOf("***") + 7, 2));
                                richTextBox.AppendText("-");
                                richTextBox.AppendText(message.Substring(message.IndexOf("***") + 9, 2));
                                richTextBox.AppendText("T");
                                richTextBox.AppendText(message.Substring(message.IndexOf("***") + 11, 2));
                                richTextBox.AppendText(":");
                                richTextBox.AppendText(message.Substring(message.IndexOf("***") + 13, 2));
                                richTextBox.AppendText(":");
                                richTextBox.AppendText(message.Substring(message.IndexOf("***") + 15, 2));
                                richTextBox.AppendText("\n");
                            }
                        }
                    }

                    else if (response == ":")
                    {
                        if (message.Length != 1)
                        {
                            friendList = message.Substring(1).Split('*').ToList();
                            friendsListBox.DataSource = friendList;
                        }
                        else
                        {
                            friendList.Clear();
                            friendsListBox.DataSource = new List<string>();
                            friendRemoveButton.Enabled = false;
                        }
                    }


                }
                catch
                {
                    
                    //Checks if the client is not closing and server did not decline the login request
                    if (!terminating && !unsuccesfull)
                    {
                        richTextBox.AppendText("The server has disconnected\n");
                        connected = false;
                    }

                    //Enable/disable buttons and textboxs
                    ipTextBox.Enabled = true;
                    portTextBox.Enabled = true;
                    usernameTextBox.Enabled = true;
                    connectButton.Enabled = true;
                    disconnectButton.Enabled = false;
                    postTextBox.Enabled = false;
                    sendButton.Enabled = false;
                    allpostsButton.Enabled = false;

                    clientSocket.Close();
                    connected = false;
                }

            }
        }



        //Disconnect button**********************************************************
        private void disconnectButton_Click(object sender, EventArgs e)
        {

            //Enable/disable buttons and textboxs
            ipTextBox.Enabled = true;
            portTextBox.Enabled = true;
            usernameTextBox.Enabled = true;
            connectButton.Enabled = true;
            disconnectButton.Enabled = false;
            postTextBox.Enabled = false;
            sendButton.Enabled = false;
            allpostsButton.Enabled = false;
            friendsListBox.DataSource = new List<string>();
            friendsListBox.Enabled = false;
            friendRemoveButton.Enabled = false;
            friendUsernameTextBox.Enabled = false;
            addFriendButton.Enabled = false;
            postIDTextBox.Enabled = false;
            deleteButton.Enabled = false;
            myPostsButton.Enabled = false;
            friendPostsButton.Enabled = false;

            clientSocket.Disconnect(true);
            terminating = true;
            connected = false;
            richTextBox.AppendText("Disconnected from server!\n");
        }



        //SSend button******************************************************
        private void sendButton_Click(object sender, EventArgs e)
        {

            //Checks if the text field is empty
            if(postTextBox.Text == "")
            {
                richTextBox.AppendText("Post can not be empty.\n");
            }
            else
            {
                //Add "S" prefix to the post for new post request, send to server
                string post = "S" + postTextBox.Text;
                Byte[] buffer = Encoding.Default.GetBytes(post);
                clientSocket.Send(buffer);

                //Print the new post on the rich text box
                richTextBox.AppendText("You have successfelly sent a post:\n");
                richTextBox.AppendText(username);
                richTextBox.AppendText(": ");
                richTextBox.AppendText(post.Substring(1,post.Length - 1));
                richTextBox.AppendText("\n");

                postTextBox.Clear();
            }
        }



        //All posts button***************************************************
        private void allpostsButton_Click(object sender, EventArgs e)
        {
            //Send "A" to server to request all posts
            string req = "A";
            Byte[] buffer = Encoding.Default.GetBytes(req);
            clientSocket.Send(buffer);
        }

        private void friendsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedFriendToBeRemoved = friendsListBox.SelectedItem.ToString();
            if (selectedFriendToBeRemoved != null)
            {
                friendRemoveButton.Enabled = true;
            }
        }

        private void friendRemoveButton_Click(object sender, EventArgs e)
        {
            if (selectedFriendToBeRemoved != null)
            {
                string removalInfo = "FR:" + username + "*" + selectedFriendToBeRemoved;
                Byte[] removalBuffer = Encoding.Default.GetBytes(removalInfo);
                clientSocket.Send(removalBuffer);
            }
        }

        private void addFriendButton_Click(object sender, EventArgs e)
        {
            string friendToBeAdded = friendUsernameTextBox.Text;

            if (friendToBeAdded.Length != 0)
            {
                string additionInfo = "FA:" + username + "*" + friendToBeAdded;
                Byte[] additionBuffer = Encoding.Default.GetBytes(additionInfo);
                clientSocket.Send(additionBuffer);
                friendUsernameTextBox.Text = "";
            }
        }
    }
}
