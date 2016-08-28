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
        static void Main(string[] args) {
            database = new ServerDataSet();
            database.ReadXml("Database.xml");
            connectedUsers = new Dictionary<string, Socket>();
            server = new EasyPOI.Server();
            server.SetOnClientDisconnectFunc(OnClientDisconnect);
            server.SetPacketReceivedFunc(OnPacketReceived);
            //server.OnClientAccepted = () => { Console.WriteLine("Cliente conectado"); };
            //server.OnPacketReceived = Received;
            Console.WriteLine("Buscando clientes");
            server.StartListening();
            Console.ReadLine();
        }        
        //Manejamos cada uno de los eventos que nos pueden llegar en los paquetes
        private static void OnPacketReceived(Packet packet, Socket client)
        {
            switch (packet.Content.Type)
            {
                case PacketType.SessionBegin:
                    connectedUsers.Add(packet.Content.message, client);
                    break;
                case PacketType.Register:
                    {
                        ServerDataSet.UsuarioDataTable usuarioTable = database.Usuario;
                        var queryResult = from usuario in usuarioTable
                                    where usuario.NombreUsuario == packet.Content.message
                                    select usuario;
                        PacketContent packetContent;
                        if (queryResult.Count() == 0)
                        {
                            //Esto podría ir en una función
                            packetContent = new PacketContent(PacketType.RegisterSucessfull);
                            packetContent.message = packet.Content.message;
                            ServerDataSet.UsuarioRow usuarioRow = database.Usuario.NewUsuarioRow();
                            usuarioRow.NombreUsuario = packet.Content.message;
                            usuarioRow.Estado = UserConnectionState.Available.ToString();
                            usuarioRow.Contrasenia = (packet.Content as RegisterPacket).password;
                            usuarioRow.Carrera = (packet.Content as RegisterPacket).carrera;
                            database.Usuario.AddUsuarioRow(usuarioRow);
                            database.WriteXml(databaseFile);
                        }
                        else
                        {
                            packetContent = new PacketContent(PacketType.RegisterFail);
                            packetContent.message = packet.Content.message;
                        }
                        server.SendPacket(new Packet(packetContent), client);
                    }
                    break;
                case PacketType.TextMessage:
                    {
                        TextMessage textMessage = packet.Content as TextMessage;
                        foreach (var user in connectedUsers)
                        {
                            server.SendPacket(packet, user.Value);
                        }
                    }
                    break;
            }
        }
        //private Action<Socket> onClientDisconnect;
        private static void DisconnectUser(string username)
        {
            Socket user = connectedUsers[username];
            connectedUsers.Remove(username);
            server.CloseClientConnection(user);
        }
        private static void OnClientDisconnect(Socket client)
        {
            //Buscamos cual fue el usuario que se desconecto de manera abrupta
            KeyValuePair<string, Socket> user = new KeyValuePair<string, Socket>();
            foreach (var userTest in connectedUsers)
            {
                if (userTest.Value == client)
                {
                    user = userTest;
                    break;
                }
            }
            if (user.Key != null)
            {
                //Removemos el usuario de la lista de conectados
                connectedUsers.Remove(user.Key);
            }
            server.CloseClientConnection(client);
        }
        private static ServerDataSet database;
        private static Dictionary<string, Socket> connectedUsers;
        private const string databaseFile = "Database.xml";
    }
}
