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
        private static EasyPOI.Client cliente;
        static void Recibir(Packet packet)
        {
            Console.WriteLine("Recibiste un paquete de tipo: {0}", packet.Type);
        }
        static void Main(string[] args) {
            cliente = new EasyPOI.Client();
            cliente.OnConnection = () => { Console.WriteLine("conectado"); };//Conectado;
            cliente.OnConnectionFail = () => { Console.WriteLine("conexion fallida"); };//ConexionFallida;
            cliente.OnPacketReceived = Recibir;
            cliente.BeginConnect();
            SendLoop();
        }

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
                }
            }
        }

        private static void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            if (buffer.Length > 0)
            {
                Packet packet = new Packet(PacketType.Text, text);
                cliente.SendPacket(packet);
            }
        }
    }
}
