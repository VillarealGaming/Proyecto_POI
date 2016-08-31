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
            server.SetClientAcceptedFunc(OnClientAccepted);
            Console.WriteLine("Servidor iniciado");
            server.StartListening();
            Console.ReadLine();
        }        
        //Manejamos cada uno de los eventos que nos pueden llegar en los paquetes
        private static void OnPacketReceived(Packet packet, Socket client)
        {
            Packet packetSend;
            switch (packet.Type)
            {
                case PacketType.SessionBegin:
                    {
                        ServerDataSet.UsuarioDataTable usuarioTable = database.Usuario;
                        string username = packet.tag["username"] as string;
                        string password = packet.tag["password"] as string;
                        var queryResult = from usuario in usuarioTable
                                          where usuario.NombreUsuario == username
                                          && usuario.Contrasenia == password
                                          select usuario;
                        if (queryResult.Count() != 0)
                        {
                            //Validamos que el usuario no pueda conectarse dos veces
                            if (!connectedUsers.ContainsKey(username))
                            {
                                connectedUsers.Add(username, client);
                                packetSend = new Packet(PacketType.SessionBegin);
                                //packetContent.message = "Sesión iniciada";
                                Console.WriteLine("Sesion de " + username + " iniciada");
                            }
                            else
                            {
                                packetSend = new Packet(PacketType.Fail);
                                packetSend.tag["message"] = "El usuario ya inició sesión";
                            }
                        }
                        else
                        {
                            packetSend = new Packet(PacketType.Fail);
                            packetSend.tag["message"] = "Usuario o contraseña invalido";
                        }
                        server.SendPacket(packetSend, client);
                    }
                    break;
                case PacketType.Register:
                    {
                        ServerDataSet.UsuarioDataTable usuarioTable = database.Usuario;
                        string username = packet.tag["username"] as string;
                        string password = packet.tag["password"] as string;
                        var queryResult = from usuario in usuarioTable
                                          where usuario.NombreUsuario == username
                                          select usuario;
                        if (queryResult.Count() == 0)
                        {
                            //Esto podría ir en una función
                            packetSend = new Packet(PacketType.Register);
                            //packetContent.tag["message"] = username;
                            ServerDataSet.UsuarioRow usuarioRow = database.Usuario.NewUsuarioRow();
                            usuarioRow.NombreUsuario = username;
                            usuarioRow.Estado = UserConnectionState.Available.ToString();
                            usuarioRow.Contrasenia = password;
                            usuarioRow.Carrera = packet.tag["carrera"] as string;
                            usuarioRow.Encriptado = (bool)packet.tag["encrypt"];
                            database.Usuario.AddUsuarioRow(usuarioRow);
                            database.WriteXml(databaseFile);
                        }
                        else
                        {
                            packetSend = new Packet(PacketType.Fail);
                            //packetContent.message = packet.Content.message;
                        }
                        server.SendPacket(packetSend, client);
                    }
                    break;
                case PacketType.TextMessage:
                    {
                        //TextMessage textMessage = packet.Content as TextMessage;
                        ServerDataSet.MensajeDataTable mensajeTable = database.Mensaje;
                        ServerDataSet.MensajeRow mensajeRow = database.Mensaje.NewMensajeRow();
                        mensajeRow.Mensaje = packet.tag["text"] as string;
                        mensajeRow.Usuario = packet.tag["sender"] as string;
                        mensajeRow.Conversacion = (int)packet.tag["chatID"];
                        mensajeRow.Date = (DateTime)packet.tag["date"];
                        mensajeTable.AddMensajeRow(mensajeRow);
                        database.WriteXml(databaseFile);
                        foreach (var user in connectedUsers)
                        {
                            server.SendPacket(packet, user.Value);
                        }
                    }
                    break;
                case PacketType.CreatePublicConversation:
                    {
                        //Esto podría ir en una función
                        packetSend = new Packet(PacketType.CreatePublicConversation);
                        packetSend.tag["nombre"] = packet.tag["nombre"];
                        ServerDataSet.ConversacionRow conversacionRow = database.Conversacion.NewConversacionRow();
                        conversacionRow.Nombre = packet.tag["nombre"] as string;
                        conversacionRow.Encriptado = (bool)packet.tag["encriptado"];
                        database.Conversacion.AddConversacionRow(conversacionRow);
                        database.WriteXml(databaseFile);
                        packetSend.tag["id"] = database.Conversacion.Last().ID;
                        server.SendPacket(packetSend, client);
                    }
                    break;
                case PacketType.GetUserConversations:
                    {
                        ServerDataSet.UsuarioConversacionDataTable usuarioConversacion = database.UsuarioConversacion;
                        ServerDataSet.ConversacionDataTable conversacionDatatable = database.Conversacion;
                        packetSend = new Packet(PacketType.GetUserConversations);
                        Dictionary<int, string> conversations = new Dictionary<int, string>();
                        //var queryResult = from conversacion in usuarioConversacion
                        //                  join conversacionTable in conversacionDatatable on conversacion.Conversacion equals conversacionTable.ID
                        //                  where conversacion.Usuario == packet.tag["username"] as string
                        //                  select new
                        //                  {
                        //                      conversacion.Conversacion,
                        //                      conversacionTable.Nombre
                        //                  };
                        var queryResult = from conversation in conversacionDatatable
                                          select conversation;
                        foreach (var c in queryResult)
                        {
                            conversations.Add(c.ID, c.Nombre);
                        }
                        packetSend.tag["conversations"] = conversations;
                        server.SendPacket(packetSend, client);
                    }
                    break;
                case PacketType.GetChatConversation:
                    {
                        ServerDataSet.ConversacionDataTable conversacionTable = database.Conversacion;
                        ServerDataSet.MensajeDataTable mensajeTable = database.Mensaje;
                        List<Tuple<string,string>> text = new List<Tuple<string, string>>();
                        var queryResult = from mensaje in mensajeTable
                                          where mensaje.Conversacion == (int)packet.tag["chatID"]
                                          select mensaje;
                        foreach(var result in queryResult)
                        {
                            text.Add(new Tuple<string, string>(result.Mensaje, result.Usuario));
                        }
                        packet.tag["messages"] = text;
                        server.SendPacket(packet, client);
                    }
                    break;
            }
        }
        private static void OnClientAccepted(Socket client)
        {
            Console.WriteLine("Cliente en " + client.RemoteEndPoint + " conectado");
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
                Console.WriteLine("Sesion de " + user.Key + " cerrada");
                connectedUsers.Remove(user.Key);
            }
            Console.WriteLine("Cliente en " + client.RemoteEndPoint + " desconectado");
            server.CloseClientConnection(client);
        }
        private static ServerDataSet database;
        private static Dictionary<string, Socket> connectedUsers;
        private const string databaseFile = "Database.xml";
    }
}
