using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PuffyProject {
    // Class which communicate with the interface in C++ in Puffy
    public class TcpServer {

        Queue<int> messageQueue;

        public static TcpServer Instance = null;
        public TcpServer(Queue<int> messageQueue) {
            Instance = this;
            this.messageQueue = messageQueue;
            ThreadStart t = delegate {
                connectServer();
            };
            new Thread(t).Start();
        }


        // Message to be sent to the client
        protected string message = "1";
        //Creation of the Tcp client (empty for the moment)
        protected Socket client;

        //Function to connect the client and the server
        public void connectServer() {
            int recv;
            byte[] data = new byte[1024];
            // Creation of an Ip object connect to the port 22
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 22);
            // Creation of the Socket object : interface of the Server to the client
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Associate the Socket to the Ip
            newsock.Bind(ipep);
            //Listen to detect a Client
            newsock.Listen(10);
            Console.WriteLine("Waiting for a client...");
            //Connect the client
            client = newsock.Accept();
            //Creation of the Ip object of the client
            IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("Connected with {0} at port {1}", clientep.Address, clientep.Port);

            //Infinite while to listen the client's orders
            while (true) {
                data = new byte[1024];
                try {
                    //Take the data send by the client in the byte data and transform it in String in message
                    recv = client.Receive(data);
                    message = Encoding.ASCII.GetString(data, 0, recv);

                } catch (SocketException e) {
                    //Error: the client is disconnected : leave the while
                    Console.WriteLine("Disconnected from {0}", clientep.Address);
                    break;
                }
                //If the client click the startButton (client send a command = 1) on the PuffyInterface
                int messageInt = int.Parse(message);
                switch (messageInt) {
                    case 1:
                        //Send Start message to the Client : The message is arrived
                        lock (messageQueue) {
                            Program.eventQueue.Enqueue((int)EventList.PuffyStart);
                            data = Encoding.ASCII.GetBytes("StartMessage");
                            client.Send(data, data.Length, SocketFlags.None);
                        }
                        break;
                    case 2:
                        //The client click the StopButton (client send a command = 2) on the PuffyInterface
                        //Send Stop message to the Client : The message is arrived
                        lock (messageQueue) {
                            Program.eventQueue.Enqueue((int)EventList.PuffyStop);
                            data = Encoding.ASCII.GetBytes("StopMessage");
                            client.Send(data, data.Length, SocketFlags.None);
                        }
                        break;
                    case 3:
                        lock (messageQueue) {
                            Program.eventQueue.Enqueue((int)EventList.PuffyIntro);
                            data = Encoding.ASCII.GetBytes("IntroductionActivity");
                            client.Send(data, data.Length, SocketFlags.None);
                        }
                        break;
                    case 4:
                        lock (messageQueue) {
                            Program.eventQueue.Enqueue((int)EventList.PuffyStory);
                            data = Encoding.ASCII.GetBytes("StoryActivity");
                            client.Send(data, data.Length, SocketFlags.None);
                        }
                        break;
                    case 5:
                        lock (messageQueue) {
                            Program.eventQueue.Enqueue((int)EventList.PuffyTag);
                            data = Encoding.ASCII.GetBytes("TagActivity");
                            client.Send(data, data.Length, SocketFlags.None);
                        }
                        break;
                    case 0:
                        lock (messageQueue) {
                            data = Encoding.ASCII.GetBytes("Quit");
                            client.Send(data, data.Length, SocketFlags.None);
                            newsock.Close();
                            message = "";
                        }
                        break;
                }
            }
            //End of the while : the client is disconnected : click the QuitButton or close the App on the PuffyInterface
            Console.WriteLine("Disconnected from {0}", clientep.Address);
            //Close the Tcp Client
            client.Close();
            //newsock.Close();
            //message = "";
        }
        // Function sending message to the client 
        public void sendMessage(string message) {
            byte[] data = new byte[1024];
            data = Encoding.ASCII.GetBytes(message);
            client.Send(data, data.Length, SocketFlags.None);
        }

        public String getMessage() {
            return message;
        }
    }
}
