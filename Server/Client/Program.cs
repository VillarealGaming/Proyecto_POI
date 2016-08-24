using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client {
    class Program {
        private static Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static void Main(string[] args) {
            LoopConnect();
            Console.ReadLine();
        }

        private static void SendLoop() {
            while(true) {
                string msg = Console.ReadLine();
                byte[] buffer = Encoding.ASCII.GetBytes(msg);
                client.Send(buffer);

                byte[] receivedBuffer = new byte[1024];
                int rec = client.Receive(receivedBuffer);
                byte[] data = new byte[rec];
                Buffer.BlockCopy(receivedBuffer, 0, data, 0, rec);

            }
        }

        private static void LoopConnect() {
            int attempts = 0;
            while(!client.Connected) {
                try {
                    attempts++;
                    client.Connect(IPAddress.Loopback, 100);
                } catch (SocketException) {
                    Console.Clear();
                    Console.WriteLine("Connection attempts: {0}", attempts);
                }
            }
            Console.Clear();
            Console.WriteLine("Connected");
            
        }
    }
}
