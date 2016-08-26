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
            ServerDataSet database = new ServerDataSet();
            database.ReadXml("DataBase.xml");

            ServerDataSet.UsuarioRow usuarioEntry = database.Usuario.NewUsuarioRow();
            //DataRowCollection usuario = database.Usuario.Rows;
            database.Usuario.AddUsuarioRow(usuarioEntry);

            ServerDataSet.UsuarioDataTable usuarioTable = database.Usuario;
            var query = from usuario in usuarioTable
                        where usuario.ID == 1
                        select usuario;
            //usuarioEntry.Nombre = "Jaime";
            //usuarioEntry.Contrasenia = "1234";
            //usuario.Add(usuarioEntry);
            //dataBase.WriteXml("DataBase.xml");
            Console.ReadLine();
        }
    }
}
