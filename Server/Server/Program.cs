using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server {
    class Program {
        private static Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static List<Socket> clients = new List<Socket>();
        private static byte[] dataBuffer = new byte[1024];

        static void Main(string[] args) {
            SetUp();
            Console.ReadLine();
        }

        private static void SetUp() {
            Console.WriteLine("Setting up server...");
            server.Bind(new IPEndPoint(IPAddress.Any, 100));
            server.Listen(30);
            server.BeginAccept(new AsyncCallback(AcceptCallback), null);
     
        }

        private static void AcceptCallback(IAsyncResult AR) {
            Socket client = server.EndAccept(AR);
            clients.Add(client);
            client.BeginReceive(dataBuffer, 0, dataBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), client);
            server.BeginAccept(new AsyncCallback(AcceptCallback), null);

        }

        private static void ReceiveCallback(IAsyncResult AR) {
            Socket client = (Socket)AR.AsyncState;
            int received;//
            try {
                received = client.EndReceive(AR);
            } catch (SocketException) {
                Console.WriteLine("Client forcefully diconnected");
                client.Close();
                clients.Remove(client);
                return;
            }
            byte[] data = new byte[received];
            Buffer.BlockCopy(dataBuffer, 0, data, 0, received);
            string text = Encoding.ASCII.GetString(data);

            string response = string.Empty;
           if (text.ToLower() == "exit") {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                clients.Remove(client);
                Console.WriteLine("Client disconnected");
                return;
            } else {
                response = text;
            }
            byte[] responseData = Encoding.ASCII.GetBytes(response);
            foreach (Socket chatMember in clients) {
                chatMember.BeginSend(responseData, 0, responseData.Length, SocketFlags.None, new AsyncCallback(SendCallback), chatMember);
                chatMember.BeginReceive(dataBuffer, 0, dataBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), chatMember);
            }
        }

        private static void SendCallback(IAsyncResult AR) {
            Socket client = (Socket)AR.AsyncState;
            client.EndSend(AR);
        } 
    }
}
