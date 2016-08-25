using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using EasyPOI;
namespace Server {
    class Program {
        private static EasyPOI.Server server;
        private static void Received(Packet packet)
        {
            Console.Write("Paquete recibido: ");
            if(packet.Type == PacketType.Text)
            {
                Console.WriteLine(packet.GetContent<string>());
            }
        }
        static void Main(string[] args) {
            server = new EasyPOI.Server();
            server.OnClientAccepted = () => { Console.WriteLine("Cliente conectado"); };
            server.OnPacketReceived = Received;
            Console.WriteLine("Buscando clientes");
            server.StartListening();
            Console.ReadLine();
        }
    }
}
