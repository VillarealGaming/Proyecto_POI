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
            server.Listen(3);
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
            int received = client.EndReceive(AR);
            byte[] data = new byte[received];
            Buffer.BlockCopy(dataBuffer, 0, data, 0, received);
            string text = Encoding.ASCII.GetString(data);

            string response = string.Empty;
            if (text.ToLower() == "hola") {
                response = "Bienvenido";
            } else {
                response = "N";
            }
            byte[] responseData = Encoding.ASCII.GetBytes(response);
            client.BeginSend(responseData, 0, responseData.Length, SocketFlags.None, new AsyncCallback(SendCallback), client);
            client.BeginReceive(dataBuffer, 0, dataBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), client);
        }

        private static void SendCallback(IAsyncResult AR) {
            Socket client = (Socket)AR.AsyncState;
            client.EndSend(AR);
        } 
    }
}
