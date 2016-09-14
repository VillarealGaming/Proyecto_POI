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
                                packetSend.tag["state"] = queryResult.First().Estado;
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
                            //usuarioRow.Encriptado = (bool)packet.tag["encrypt"];
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
                case PacketType.Buzz:
                    {
                        ServerDataSet.MensajeDataTable mensajeTable = database.Mensaje;
                        ServerDataSet.UsuarioConversacionDataTable usuarioConversacionTable = database.UsuarioConversacion;
                        //Usuarios a mandar
                        var queryResult = from usuario in usuarioConversacionTable
                                          where usuario.Conversacion == (int)packet.tag["chatID"]
                                          select usuario;
                        foreach (var toSendUser in queryResult)
                        {
                            if (connectedUsers.ContainsKey(toSendUser.Usuario))
                                server.SendPacket(packet, connectedUsers[toSendUser.Usuario]);
                        }
                    }
                    break;
                case PacketType.TextMessage:
                    {
                        ServerDataSet.MensajeDataTable mensajeTable = database.Mensaje;
                        ServerDataSet.UsuarioConversacionDataTable usuarioConversacionTable = database.UsuarioConversacion;
                        ServerDataSet.MensajeRow mensajeRow = database.Mensaje.NewMensajeRow();
                        mensajeRow.Mensaje = packet.tag["text"] as string;
                        mensajeRow.Usuario = packet.tag["sender"] as string;
                        mensajeRow.Conversacion = (int)packet.tag["chatID"];
                        mensajeRow.Date = (DateTime)packet.tag["date"];
                        mensajeTable.AddMensajeRow(mensajeRow);
                        database.WriteXml(databaseFile);
                        //Usuarios a mandar
                        var queryResult = from usuario in usuarioConversacionTable
                                          where usuario.Conversacion == (int)packet.tag["chatID"]
                                          select usuario;
                        foreach(var toSendUser in queryResult)
                        {
                            if(connectedUsers.ContainsKey(toSendUser.Usuario))
                                server.SendPacket(packet, connectedUsers[toSendUser.Usuario]);
                        }
                    }
                    break;
                case PacketType.CreatePublicConversation:
                    {
                        //Esto podría ir en una función
                        ServerDataSet.UsuarioConversacionDataTable usuarioConversacion = database.UsuarioConversacion;
                        List<string> users = packet.tag["users"] as List<string>;
                        var conversationsWithUsers = from conversacion in usuarioConversacion
                                                     where users.Contains(conversacion.Usuario)
                                                     group conversacion by new { conversacion.Usuario } into uniqueUsers
                                                     select uniqueUsers;
                        if (conversationsWithUsers.Count() < 5)
                        {
                                ServerDataSet.ConversacionRow conversacionRow = database.Conversacion.NewConversacionRow();
                                conversacionRow.Nombre = packet.tag["nombre"] as string;
                                database.Conversacion.AddConversacionRow(conversacionRow);
                                foreach (var user in users)
                                {
                                    ServerDataSet.UsuarioConversacionRow usuarioConversacionRow = database.UsuarioConversacion.NewUsuarioConversacionRow();
                                    usuarioConversacionRow.Usuario = user;
                                    usuarioConversacionRow.Conversacion = database.Conversacion.Last().ID;
                                    database.UsuarioConversacion.AddUsuarioConversacionRow(usuarioConversacionRow);
                                }
                                database.WriteXml(databaseFile);
                                packet.tag["id"] = database.Conversacion.Last().ID;
                                server.SendPacket(packet, client);
                        }
                        else
                        {
                            packetSend = new Packet(PacketType.Fail);
                            packetSend.tag["message"] = "Ya existe una conversación con estos usuarios";
                            packetSend.tag["case"] = "No se puede crear la conversación";
                            server.SendPacket(packetSend, client);
                        }
                    }
                    break;
                case PacketType.GetUserConversations:
                    {
                        ServerDataSet.UsuarioConversacionDataTable usuarioConversacion = database.UsuarioConversacion;
                        ServerDataSet.ConversacionDataTable conversacionDatatable = database.Conversacion;
                        ServerDataSet.MensajeDataTable mensajeTable = database.Mensaje;
                        packetSend = new Packet(PacketType.GetUserConversations);
                        Dictionary<int, string> conversations = new Dictionary<int, string>();
                        Dictionary<int, List<Tuple<string, string>>> text = new Dictionary<int, List<Tuple<string, string>>>();
                        Dictionary<int, List<string>> users = new Dictionary<int, List<string>>();
                        Dictionary<int, DateTime> lastMessageDate = new Dictionary<int, DateTime>();
                        //Conversaciones
                        var queryResult = from conversation in conversacionDatatable
                                          join userConversation in usuarioConversacion
                                          on conversation.ID equals userConversation.Conversacion
                                          where userConversation.Usuario == packet.tag["user"] as string
                                          select new
                                          {
                                              conversation.Nombre,
                                              conversation.ID,
                                              userConversation.Usuario
                                          };
                        foreach (var c in queryResult)
                        {
                            conversations.Add(c.ID, c.Nombre);
                            text.Add(c.ID, new List<Tuple<string, string>>());
                            users.Add(c.ID, new List<string>());
                            //Mensajes
                            var queryResultMessages = from mensaje in mensajeTable
                                                      where mensaje.Conversacion == c.ID
                                                      select mensaje;
                            foreach (var result in queryResultMessages)
                            {
                                text[c.ID].Add(new Tuple<string, string>(result.Mensaje, result.Usuario));
                            }
                            if(queryResultMessages.Count() > 0)
                            {
                                //ultimo mensaje
                                lastMessageDate.Add(c.ID, queryResultMessages.Last().Date);
                            }
                            //Participantes
                            var queryResultUsers = from user in usuarioConversacion
                                                   where user.Conversacion == c.ID
                                                   select user;
                            foreach(var user in queryResultUsers)
                            {
                                users[c.ID].Add(user.Usuario);
                            }
                        }
                        //ultimo mensaje
                        packetSend.tag["lastDate"] = lastMessageDate;
                        packetSend.tag["messages"] = text;
                        packetSend.tag["conversations"] = conversations;
                        packetSend.tag["users"] = users;
                        server.SendPacket(packetSend, client);
                    }
                    break;
                case PacketType.GetUsers:
                    {
                        Dictionary<string, Tuple<string, string>> userList = new Dictionary<string, Tuple<string, string>>();
                        ServerDataSet.UsuarioDataTable usuarioTable = database.Usuario;
                        var queryResult = from usuario in usuarioTable
                                          select usuario;
                        string estado;
                        foreach (var user in queryResult)
                        {
                            estado = connectedUsers.ContainsKey(user.NombreUsuario) ? user.Estado : UserConnectionState.Offline.ToString();
                            userList.Add(user.NombreUsuario, new Tuple<string, string>(estado, user.Carrera));
                        }
                        packet.tag["userList"] = userList;
                        server.SendPacket(packet, client);
                    }
                    break;
                case PacketType.SetUserState:
                    {
                        ServerDataSet.UsuarioDataTable usuarioTable = database.Usuario;
                        var queryResult = from usuario in usuarioTable
                                          where usuario.NombreUsuario == packet.tag["user"] as string
                                          select usuario;
                        queryResult.First().Estado = packet.tag["state"] as string;
                        foreach(var user in connectedUsers)
                        {
                            server.SendPacket(packet, user.Value);
                        }
                        database.WriteXml(databaseFile);
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
                //Mandamos a los usuario conectados la desconexión
                Packet packet = new Packet(PacketType.SetUserState);
                packet.tag["user"] = user.Key;
                packet.tag["state"] = UserConnectionState.Offline.ToString();
                //Removemos el usuario de la lista de conectados
                Console.WriteLine("Sesion de " + user.Key + " cerrada");
                connectedUsers.Remove(user.Key);
                foreach (var connectedUser in connectedUsers)
                {
                    server.SendPacket(packet, connectedUser.Value);
                }
            }
            Console.WriteLine("Cliente en " + client.RemoteEndPoint + " desconectado");
            server.CloseClientConnection(client);
        }
        private static ServerDataSet database;
        private static Dictionary<string, Socket> connectedUsers;
        private const string databaseFile = "Database.xml";
    }
}
