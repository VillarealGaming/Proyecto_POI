using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using EasyPOI;
namespace Client
{
    class Program {
        //private static Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private const int Port = 100;
        private static EasyPOI.Client cliente;
        ////https://msdn.microsoft.com/en-us/library/bew39x2a(v=vs.110).aspx
        //public class StateObject
        //{
        //    // Client socket.
        //    public Socket workSocket = null;
        //    // Size of receive buffer.
        //    public const int BufferSize = 256;
        //    // Receive buffer.
        //    public byte[] buffer = new byte[BufferSize];
        //    // Received data string.
        //    public StringBuilder sb = new StringBuilder();
        //}
        static void Recibir(Packet packet)
        {
            Console.WriteLine("Recibiste un paquete de tipo: {0}", packet.PacketType);
        }
        static void Main(string[] args) {
            cliente = new EasyPOI.Client();
            cliente.OnConnection = () => { Console.WriteLine("conectado"); };//Conectado;
            cliente.OnConnectionFail = () => { Console.WriteLine("conexion fallida"); };//ConexionFallida;
            cliente.BeginConnect();
            //LoopConnect();
            SendLoop();
        }

        //private static void Exit() {
        //    SendString("exit");
        //    client.Shutdown(SocketShutdown.Both);
        //    client.Close();
        //    Environment.Exit(0);
        //}

        private static void SendLoop() {
            while(true)
            {
                if (cliente.Connected)
                {

                    string msg = Console.ReadLine();
                    if (msg == "exit")
                    {
                        //Exit();
                    }
                    else
                    {
                        SendString(msg);
                    }
                    //string received = ReceiveString();
                    //Console.WriteLine(received);
                }
            }
        }

        //private static void LoopConnect() {
        //    int attempts = 0;
        //    //while(!client.Connected) {
        //    //    try {
        //    //        attempts++;
        //    //        client.Connect(IPAddress.Loopback, Port);
        //    //    } catch (SocketException) {
        //    //        Console.Clear();
        //    //        Console.WriteLine("Connection attempts: {0}", attempts);
        //    //    }
        //    //}
        //}

        //private static string ReceiveString()
        //{
        //    byte[] receivedBuffer = new byte[1024];
        //    int rec = client.Receive(receivedBuffer);
        //    byte[] data = new byte[rec];
        //    Buffer.BlockCopy(receivedBuffer, 0, data, 0, rec);
        //    return Encoding.ASCII.GetString(data);
        //}

        private static void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            if (buffer.Length > 0)
            {
                Packet packet = new Packet("message", buffer);
                cliente.SendPacket(packet);
            }
        }
    }
}
