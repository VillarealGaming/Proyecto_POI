using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EasyPOI;
namespace Server {
    class Program {
        private static EasyPOI.Server server;
        //private static void Received(Packet packet)
        //{
        //    Console.Write("Paquete recibido: ");
        //    if(packet.Type == PacketType.TextMessage)
        //    {
        //        Console.WriteLine(packet.GetContent<string>());
        //    }
        //}
        static void Main(string[] args) {
            server = new EasyPOI.Server();
            //server.OnClientAccepted = () => { Console.WriteLine("Cliente conectado"); };
            //server.OnPacketReceived = Received;
            Console.WriteLine("Buscando clientes");
            server.StartListening();
            //ServerDataSet database = new ServerDataSet();
            ////database.ReadXml("Database.xml");

            //ServerDataSet.UsuarioRow usuarioEntry;
            ////DataRowCollection usuario = database.Usuario.Rows;
            //usuarioEntry = database.Usuario.NewUsuarioRow();
            //usuarioEntry.Nombre = "Juan";
            //usuarioEntry.Contrasenia = "123";
            //database.Usuario.AddUsuarioRow(usuarioEntry);
            //usuarioEntry = database.Usuario.NewUsuarioRow();
            //usuarioEntry.Nombre = "Pedro";
            //usuarioEntry.Contrasenia = "abc";
            //database.Usuario.AddUsuarioRow(usuarioEntry);

            ////ServerDataSet.UsuarioDataTable usuarioTable = database.Usuario;
            ////var query = from usuario in usuarioTable
            ////            where usuario.ID == 1
            ////            select usuario;
            
            //database.WriteXml("Database.xml");
            Console.ReadLine();
        }
    }
}
