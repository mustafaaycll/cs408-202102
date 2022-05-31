using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {

        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();
        bool terminating = false;
        bool listening = false;

        //List to keep online users
        List<string> connectedClients = new List<string>();

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }

        private string getFriendsAsString(IEnumerable<string> friendshipLines, string username)
        {
            string friends = "";


            foreach (string item in friendshipLines)
            {
                string indicator = item.Substring(0, item.IndexOf(":"));
                if (indicator == username)
                {
                    friends = item.Substring(item.IndexOf(":"));
                    break;
                }
            }

            return friends;
        }

        private bool validateUser(string friend)
        {
            var users = File.ReadLines(@"../../user-db.txt");

            //Checks if the user is in the user database
            foreach (string line in users)
            {
                if (friend == line)
                {
                    return true;
                }
            }

            return false;
        }



        //Listen Button *********************************************************
        private void listenButton_Click(object sender, EventArgs e)
        {
            int serverPort;

            if (Int32.TryParse(portTextBox.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                listening = true;
                listenButton.Enabled = false;

                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                richTextBox.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                richTextBox.AppendText("Please check port number \n");
            }
        }



        //Accepting new clients*************************************************
        private void Accept()
        {
            while (listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();
                    clientSockets.Add(newClient);

                    //Directly accept all clients and create their recieve threads
                    Thread receiveThread = new Thread(() => Receive(newClient));
                    receiveThread.Start();
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        richTextBox.AppendText("The socket stopped working.\n");
                    }
                }
            }
        }



        //Recieve from clients***************************************
        private void Receive(Socket thisClient)
        {
            bool connected = true;
            string username = "";

            while (connected && !terminating)
            {
                
                try
                {
                    Byte[] buffer = new Byte[256];
                    thisClient.Receive(buffer);

                    //Write the incoming message into 'message' string
                    string message = Encoding.Default.GetString(buffer);
                    message = message.Substring(0, message.IndexOf("\0"));

                    //First char of the message is the type of the request
                    string request = message.Substring(0, 1);

                    //Login request
                    if(request == "U")
                    {
                        connected = true;
                        bool exists = false;
                        bool alreadyConnected = false;
                        username = message.Substring(1, message.Length - 1);
                        
                        //Reads the user database
                        var lines2 = File.ReadLines(@"../../user-db.txt");
                        
                        exists = false;
                        richTextBox.AppendText(username);

                        //Checks if the user is in the user database
                        foreach (string line in lines2)
                        {
                            if (username == line)
                            {
                                exists = true;
                                break;
                            }
                        }

                        //Checks if the user is already connected
                        foreach (string users in connectedClients)
                        {
                            if (username == users)
                            {
                                alreadyConnected = true;
                                break;
                            }
                        }

                        //If the user is eligible to connect
                        if (exists && !alreadyConnected)
                        {
                            Byte[] buffer2 = Encoding.Default.GetBytes("Aapproved");
                            string cnnctMsg = " has connected.\n";
                            richTextBox.AppendText(cnnctMsg);


                            var friends = getFriendsAsString(File.ReadLines(@"../../friendships-db.txt"), username);
                            Byte[] friendsBuffer = Encoding.Default.GetBytes(friends);
                            
                            //Sends approved message to client
                            thisClient.Send(buffer2);
                            thisClient.Send(friendsBuffer);

                            //Adds the user into online users
                            connectedClients.Add(username);
                        }

                        //If the client is not eligible to connect
                        else
                        {
                            //If the user does not exist in the database
                            if (!exists)
                            {
                                richTextBox.AppendText(" tried to connect to the server but cannot because is not in the database!\n");

                                //Sends invalid message to client
                                Byte[] buffer3 = Encoding.Default.GetBytes("Ainvalid");
                                thisClient.Send(buffer3);
                            }

                            //If the user is already online
                            else
                            {
                                richTextBox.AppendText(" tried to connect to the server but cannot because is already online!\n");

                                //Sends already online message to client
                                Byte[] buffer5 = Encoding.Default.GetBytes("AalreadyOnline");
                                thisClient.Send(buffer5);
                            }

                            //Since all clients are directly accepted in order to check their username, if the client is not eligible to connect,
                            //disconnect it from the server
                            connected = false;
                            thisClient.Close();
                            clientSockets.Remove(thisClient);
                            break;
                        }
                    }

                    //New post request
                    else if(request == "S")
                    {
                        //Prints the post onthe rich text box
                        richTextBox.AppendText(username + " has sent a post:\n");
                        richTextBox.AppendText(message.Substring(1,message.Length - 1));
                        richTextBox.AppendText(" \n");
                        
                        //Reads the post database
                        var lines4 = File.ReadLines(@"../../post-db.txt");
                        
                        //Counts the number of posts in order to give the new post an ID
                        int AllPostsCount = 0;
                        foreach(string allPosts in lines4)
                        {
                            AllPostsCount++;
                        }
                        AllPostsCount++;

                        //Gets the time of the post
                        string currentTime = DateTime.Now.ToString("yyyyMMddHHmmss");

                        //Creates the new post with the following information: username, ID, content, time
                        string newPost = username + "---" + AllPostsCount.ToString() + "+++" + message.Substring(1, message.Length - 1) + "***" + currentTime;
                        
                        //Writes the new post5 into post-db.txt
                        using (StreamWriter file = new StreamWriter(@"../../post-db.txt", append: true))
                        {
                            file.WriteLine(newPost);
                        }
                    }

                    //All posts request
                    else if(request == "A")
                    {
                        
                        //Reads posts database
                        var lines3 = File.ReadLines(@"../../post-db.txt");
                        
                        int postCount = 0;

                        richTextBox.AppendText("Showed all posts for ");
                        richTextBox.AppendText(username);
                        richTextBox.AppendText("\n");

                        foreach (string post in lines3)
                        {

                            //If the owner of the post is not the user who requested all posts
                            //Post prefix notation: P X X
                            //                      | | |-> 0 if there are no posts in the database to show that user, 1 if there is at least one post that can be shown
                            //                      | |->0 if this is the first post to be sent in this request
                            //                      |->Indicates that the response is about all posts request

                            if (post.Substring(0, post.IndexOf("---")) != username)
                            {
                                if(postCount == 0)
                                {
                                    string toBeSent = "P01" + post;
                                    Byte[] buffer6 = Encoding.Default.GetBytes(toBeSent);
                                    thisClient.Send(buffer6);
                                    postCount++;
                                }
                                else
                                {
                                    string toBeSent = "P11" + post;
                                    Byte[] buffer8 = Encoding.Default.GetBytes(toBeSent);
                                    thisClient.Send(buffer8);
                                    postCount++;
                                }
                            }
                        }
                        if(postCount == 0)
                        {
                            string toBeSent = "P00";
                            Byte[] buffer7 = Encoding.Default.GetBytes(toBeSent);
                            thisClient.Send(buffer7);
                        }
                    }
                    else if (request == "F")
                    {
                        string remainingInfo = message.Substring(0, message.IndexOf(":"));
                        List<string> involvedParties = message.Substring(message.IndexOf(":") + 1).Split('*').ToList();
                        username = involvedParties[0];
                        string friend = involvedParties[1];

                        var friendsDB = File.ReadLines(@"../../friendships-db.txt").ToList();

                        if(!validateUser(friend))
                        {
                            Byte[] invalidFriendsBuffer = Encoding.Default.GetBytes("AinvalidFriendName*"+friend);
                            thisClient.Send(invalidFriendsBuffer);
                        }
                        else if (remainingInfo == "FA")
                        {
                            List<string> newFriendsDB = new List<string>();
                            for (int i = 0; i < friendsDB.Count(); i++)
                            {
                                string line = friendsDB[i];
                                List<string> splitLine = line.Split(':').ToList();
                                splitLine.Remove("");

                                if (splitLine[0] == username)
                                {
                                    string formattedLine = username + ":";
                                    if (splitLine.Count() == 2)
                                    {
                                        List<string> splitFriends = splitLine[1].Split('*').ToList();
                                        if (!splitFriends.Contains(friend))
                                        {
                                            splitFriends.Add(friend);
                                        }
                                        string reJoinedFriends = String.Join("*", splitFriends.ToArray());
                                        formattedLine += reJoinedFriends;
                                    }
                                    else
                                    {
                                        formattedLine += friend;
                                    }
                                    newFriendsDB.Add(formattedLine);
                                }
                                else if (splitLine[0] == friend)
                                {
                                    string formattedLine = friend + ":";
                                    if (splitLine.Count() == 2)
                                    {
                                        List<string> splitFriends = splitLine[1].Split('*').ToList();
                                        if (!splitFriends.Contains(username))
                                        {
                                            splitFriends.Add(username);
                                        }
                                        string reJoinedFriends = String.Join("*", splitFriends.ToArray());
                                        formattedLine += reJoinedFriends;
                                    }
                                    else
                                    {
                                        formattedLine += username;
                                    }
                                    newFriendsDB.Add(formattedLine);
                                }
                                else
                                {
                                    newFriendsDB.Add(line);
                                }
                            }
                            File.WriteAllText(@"../../friendships-db.txt", String.Join("\n", newFriendsDB.ToArray()));
                            var friends = getFriendsAsString(File.ReadLines(@"../../friendships-db.txt"), username);
                            Byte[] friendsBuffer = Encoding.Default.GetBytes(friends);
                            thisClient.Send(friendsBuffer);
                            Byte[] FriendAddedBuffer = Encoding.Default.GetBytes("AFriendAdded*" + friend);
                            thisClient.Send(FriendAddedBuffer);
                        }
                        else if (remainingInfo == "FR")
                        {
                            List<string> newFriendsDB = new List<string>();
                            for (int i = 0; i < friendsDB.Count(); i++)
                            {
                                string line = friendsDB[i];
                                List<string> splitLine = line.Split(':').ToList();
                                splitLine.Remove("");

                                if (splitLine[0] == username)
                                {
                                    string formattedLine = username + ":";
                                    List<string> splitFriends = splitLine[1].Split('*').ToList();
                                    splitFriends.Remove(friend);
                                    string reJoinedFriends = String.Join("*", splitFriends.ToArray());
                                    formattedLine += reJoinedFriends;
                                    newFriendsDB.Add(formattedLine);
                                }
                                else if (splitLine[0] == friend)
                                {
                                    string formattedLine = friend + ":";
                                    List<string> splitFriends = splitLine[1].Split('*').ToList();
                                    splitFriends.Remove(username);
                                    string reJoinedFriends = String.Join("*", splitFriends.ToArray());
                                    formattedLine += reJoinedFriends;
                                    newFriendsDB.Add(formattedLine);
                                }
                                else
                                {
                                    newFriendsDB.Add(line);
                                }
                            }
                            File.WriteAllText(@"../../friendships-db.txt", String.Join("\n", newFriendsDB.ToArray()));
                            var friends = getFriendsAsString(File.ReadLines(@"../../friendships-db.txt"), username);
                            Byte[] friendsBuffer = Encoding.Default.GetBytes(friends);
                            thisClient.Send(friendsBuffer);
                            Byte[] FriendRemovedBuffer = Encoding.Default.GetBytes("AFriendRemoved*" + friend);
                            thisClient.Send(FriendRemovedBuffer);
                        }
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        richTextBox.AppendText(username + " has disconnected\n");

                        //Remove client from online users
                        connectedClients.Remove(username);
                    }
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    connected = false;
                }
            }
        }
    }
}
