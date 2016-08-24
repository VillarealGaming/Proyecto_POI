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
            SendLoop();
            Console.ReadLine();
        }

        private static void Exit() {
            SendString("exit");
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            Environment.Exit(0);
        }

        private static void SendLoop() {
            while(true) {
                string msg = Console.ReadLine();
                SendString(msg);

                byte[] receivedBuffer = new byte[1024];
                int rec = client.Receive(receivedBuffer);
                byte[] data = new byte[rec];
                Buffer.BlockCopy(receivedBuffer, 0, data, 0, rec);
                Console.WriteLine(Encoding.ASCII.GetString(data));
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

        private static void SendString(string text) {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            client.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }
    }
}
