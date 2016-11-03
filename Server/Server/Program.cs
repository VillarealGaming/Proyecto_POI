using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EasyPOI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
namespace Server {
    class Program {
        private static EasyPOI.Server server;
        private static ServerDataSet database;
        private static Dictionary<string, Socket> connectedUsers;
        private static Dictionary<string, IPEndPoint> connectedUsersUdp;
        private static Dictionary<int, List<string>> connectedPlayers;
        private const string databaseFile = "Database.xml";
        private static Mutex databaseFileMutex = new Mutex(false, "databaseWritting");
        static void Main(string[] args) {
            database = new ServerDataSet();
            database.ReadXml("Database.xml");
            connectedUsers = new Dictionary<string, Socket>();
            connectedUsersUdp = new Dictionary<string, IPEndPoint>();
            connectedPlayers = new Dictionary<int, List<string>>();
            server = new EasyPOI.Server();
            server.SetOnClientDisconnectFunc(OnClientDisconnect);
            server.SetPacketReceivedFunc(OnPacketReceived);
            server.SetOnUdpPacketReceived(OnUdpPacket);
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
                                var user = queryResult.First();
                                packetSend = new Packet(PacketType.SessionBegin);
                                packetSend.tag["state"] = user.Estado;
                                packetSend.tag["enemiesKilled"] = user.EnemigosAbatidos;
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
                            usuarioRow.Correo = packet.tag["email"] as string;
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
                case PacketType.TextMessage:
                    {
                        ServerDataSet.MensajeDataTable mensajeTable = database.Mensaje;
                        ServerDataSet.UsuarioConversacionDataTable usuarioConversacionTable = database.UsuarioConversacion;
                        ServerDataSet.MensajeRow mensajeRow = database.Mensaje.NewMensajeRow();
                        if ((bool)packet.tag["encriptado"]) {
                            mensajeRow.Mensaje = EncryptString(packet.tag["text"] as string, packet.tag["sender"] as string);
                        }
                        else {
                            mensajeRow.Mensaje = packet.tag["text"] as string;
                        }
                        //mensajeRow.Mensaje = packet.tag["text"] as string;
                        mensajeRow.Usuario = packet.tag["sender"] as string;
                        mensajeRow.Conversacion = (int)packet.tag["chatID"];
                        mensajeRow.Date = (DateTime)packet.tag["date"];
                        mensajeRow.Encriptado = (bool)packet.tag["encriptado"];
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
                case PacketType.PrivateTextMessage:
                    {
                        ServerDataSet.UsuarioPrivadoDataTable usuarioPrivado = database.UsuarioPrivado;
                        ServerDataSet.MensajePrivadoDataTable mensajePrivado = database.MensajePrivado;
                        ServerDataSet.MensajePrivadoRow mensajePrivadoRow = database.MensajePrivado.NewMensajePrivadoRow();
                        //List<string> users = packet.tag["users"] as List<string>;
                        if((bool)packet.tag["encriptado"])
                        {
                            mensajePrivadoRow.Mensaje = EncryptString(packet.tag["text"] as string, packet.tag["sender"] as string);
                        }
                        else
                        {
                            mensajePrivadoRow.Mensaje = packet.tag["text"] as string;
                        }
                        mensajePrivadoRow.Usuario = packet.tag["sender"] as string;
                        mensajePrivadoRow.ConversacionPrivada = (int)packet.tag["chatID"];
                        mensajePrivadoRow.Date = (DateTime)packet.tag["date"];
                        mensajePrivadoRow.Encriptado = (bool)packet.tag["encriptado"];
                        mensajePrivado.AddMensajePrivadoRow(mensajePrivadoRow);
                        database.WriteXml(databaseFile);
                        //usuarios a mandar
                        var queryResult = from user in usuarioPrivado
                                          where user.ConversacionPrivada == (int)packet.tag["chatID"]
                                          select user;
                        foreach (var user in queryResult)
                        {
                            if (connectedUsers.ContainsKey(user.Usuario))
                                server.SendPacket(packet, connectedUsers[user.Usuario]);
                        }
                    }
                    break;
                case PacketType.CreatePrivateConversation:
                    {
                        ServerDataSet.UsuarioPrivadoDataTable usuarioPrivado = database.UsuarioPrivado;
                        ServerDataSet.ConversacionPrivadaRow conversacionPrivadaRow = database.ConversacionPrivada.NewConversacionPrivadaRow();
                        database.ConversacionPrivada.AddConversacionPrivadaRow(conversacionPrivadaRow);
                        List<string> users = packet.tag["users"] as List<string>;
                        foreach(var user in users)
                        {
                            ServerDataSet.UsuarioPrivadoRow usuarioPrivadoRow = database.UsuarioPrivado.NewUsuarioPrivadoRow();
                            usuarioPrivadoRow.Usuario = user;
                            usuarioPrivadoRow.ConversacionPrivada = database.ConversacionPrivada.Last().ID;
                            database.UsuarioPrivado.AddUsuarioPrivadoRow(usuarioPrivadoRow);
                        }
                        database.WriteXml(databaseFile);
                        packet.tag["id"] = database.ConversacionPrivada.Last().ID;
                        //Usuarios a mandar
                        foreach (var toSendUser in users)
                        {
                            if (connectedUsers.ContainsKey(toSendUser))
                                server.SendPacket(packet, connectedUsers[toSendUser]);
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
                                //server.SendPacket(packet, client);
                                //Usuarios a mandar
                                foreach (var toSendUser in users)
                                {
                                    if (connectedUsers.ContainsKey(toSendUser))
                                        server.SendPacket(packet, connectedUsers[toSendUser]);
                                }
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
                case PacketType.GetPrivateConversations:
                    {
                        ServerDataSet.UsuarioPrivadoDataTable usuarioPrivado = database.UsuarioPrivado;
                        ServerDataSet.MensajePrivadoDataTable mensajePrivado = database.MensajePrivado;
                        ServerDataSet.ConversacionPrivadaDataTable conversacionPrivada = database.ConversacionPrivada;
                        packetSend = new Packet(PacketType.GetPrivateConversations);
                        Dictionary<int, List<string>> conversations = new Dictionary<int, List<string>>();
                        Dictionary<int, List<Tuple<string, string>>> text = new Dictionary<int, List<Tuple<string, string>>>();
                        Dictionary<int, DateTime> lastMessageDate = new Dictionary<int, DateTime>();
                        //Conversaciones
                        var queryResult = from conversation in usuarioPrivado
                                          join chat in conversacionPrivada
                                          on conversation.ConversacionPrivada equals chat.ID
                                          select conversation;
                        var chats = from chat in queryResult
                                    where chat.Usuario == packet.tag["user"] as string
                                    group chat by new { chat.ConversacionPrivada } into uniqueChat
                                     select uniqueChat;
                        foreach(var chat in chats)
                        {
                            conversations.Add(chat.Key.ConversacionPrivada, new List<string>());
                            text.Add(chat.Key.ConversacionPrivada, new List<Tuple<string, string>>());
                            //Mensajes
                            var queryResultMessages = from mensaje in mensajePrivado
                                                      where mensaje.ConversacionPrivada == chat.Key.ConversacionPrivada
                                                      select mensaje;
                            foreach (var result in queryResultMessages)
                            {
                                string mensaje = result.Encriptado ?  DecryptString(result.Mensaje, result.Usuario) : result.Mensaje;
                                text[chat.Key.ConversacionPrivada].Add(new Tuple<string, string>(mensaje, result.Usuario));
                            }
                            if (queryResultMessages.Count() > 0)
                            {
                                //ultimo mensaje
                                lastMessageDate.Add(chat.Key.ConversacionPrivada, queryResultMessages.Last().Date);
                            }
                        }
                        //users
                        foreach (var c in queryResult)
                        {
                            if (conversations.ContainsKey(c.ConversacionPrivada))
                                conversations[c.ConversacionPrivada].Add(c.Usuario);
                        }
                        packetSend.tag["lastDate"] = lastMessageDate;
                        packetSend.tag["messages"] = text;
                        packetSend.tag["conversations"] = conversations;
                        server.SendPacket(packetSend, client);
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
                            foreach (var result in queryResultMessages) {
                                string mensaje = result.Encriptado ? DecryptString(result.Mensaje, result.Usuario) : result.Mensaje;
                                text[c.ID].Add(new Tuple<string, string>(mensaje, result.Usuario));
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
                case PacketType.Buzz:
                case PacketType.FileSendChat:
                    {
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
                case PacketType.PrivateBuzz:
                case PacketType.FileSendPrivate:
                case PacketType.WebCamFrame:
                case PacketType.WebCamRequest:
                case PacketType.WebCamResponse:
                    {
                        ServerDataSet.UsuarioPrivadoDataTable usuarioPrivado = database.UsuarioPrivado;
                        //usuarios a mandar
                        var queryResult = from user in usuarioPrivado
                                          where user.ConversacionPrivada == (int)packet.tag["chatID"]
                                          select user;
                        foreach (var user in queryResult)
                        {
                            if (connectedUsers.ContainsKey(user.Usuario))
                                server.SendPacket(packet, connectedUsers[user.Usuario]);
                        }
                    }
                    break;
                case PacketType.UdpLocalEndPoint:
                    {
                        string username = packet.tag["username"] as string;
                        if (!connectedUsersUdp.ContainsKey(username))
                            connectedUsersUdp.Add(username, packet.tag["endPoint"] as IPEndPoint);
                    }
                    break;
                case PacketType.SendMail:
                    {
                        ServerDataSet.UsuarioDataTable usuarioTable = database.Usuario;
                        var queryResultFrom = from usuario in usuarioTable
                                              where usuario.NombreUsuario == packet.tag["from"] as string
                                              select usuario;
                        var queryResultTo = from usuario in usuarioTable
                                          where usuario.NombreUsuario == packet.tag["to"] as string
                                          select usuario;
                        server.SendMailText(
                            //queryResultFrom.First().Correo,
                            queryResultTo.First().Correo,
                            packet.tag["from"] as string + ": " + packet.tag["subject"] as string,
                            packet.tag["body"] as string);
                        Console.WriteLine("Correo enviado de " + packet.tag["from"] as string + " a " + packet.tag["to"]);
                    }
                    break;
                case PacketType.GameStart:
                    {
                        ServerDataSet.UsuarioPrivadoDataTable usuarioPrivado = database.UsuarioPrivado;
                        //usuarios a mandar
                        int chatID = (int)packet.tag["chatID"];
                        var queryResult = from user in usuarioPrivado
                                          where user.ConversacionPrivada == chatID
                                          select user;
                        if(!connectedPlayers.ContainsKey(chatID))
                        {
                            connectedPlayers[chatID] = new List<string>();
                            connectedPlayers[chatID].Add(packet.tag["sender"] as string);
                            Packet sendPacket = new Packet(PacketType.GameFirstPlayer);
                            server.SendPacket(sendPacket, connectedUsers[packet.tag["sender"] as string]);
                        }
                        else
                        {
                            connectedPlayers[chatID].Add(packet.tag["sender"] as string);
                            Packet sendPacket = new Packet(PacketType.GameSecondPlayer);
                            sendPacket.tag["sender"] = packet.tag["sender"];
                            foreach (var user in queryResult)
                            {
                                if (connectedUsers.ContainsKey(user.Usuario))
                                    server.SendPacket(sendPacket, connectedUsers[user.Usuario]);
                            }
                        }
                    }
                    break;
                case PacketType.LevelData:
                case PacketType.BeginGame:
                    {
                        ServerDataSet.UsuarioPrivadoDataTable usuarioPrivado = database.UsuarioPrivado;
                        //usuarios a mandar
                        int chatID = (int)packet.tag["chatID"];
                        var queryResult = from user in usuarioPrivado
                                          where user.ConversacionPrivada == chatID
                                          select user;
                        foreach (var user in queryResult)
                        {
                            if (connectedUsers.ContainsKey(user.Usuario) && user.Usuario != packet.tag["sender"] as string)
                                server.SendPacket(packet, connectedUsers[user.Usuario]);
                        }
                    }
                    break;
                case PacketType.ExitGame:
                    {
                        ServerDataSet.UsuarioPrivadoDataTable usuarioPrivado = database.UsuarioPrivado;
                        //usuarios a mandar
                        int chatID = (int)packet.tag["chatID"];
                        var queryResult = from user in usuarioPrivado
                                          where user.ConversacionPrivada == chatID
                                          select user;
                        foreach (var user in queryResult)
                        {
                            if (connectedUsers.ContainsKey(user.Usuario) && user.Usuario != packet.tag["sender"] as string)
                                server.SendPacket(packet, connectedUsers[user.Usuario]);
                        }
                        connectedPlayers.Remove(chatID);
                    }
                    break;
                case PacketType.EnemyKilled:
                    {
                        ServerDataSet.UsuarioDataTable usuarioTable = database.Usuario;
                        var queryResult = from usuario in usuarioTable
                                          where usuario.NombreUsuario == packet.tag["user"] as string
                                          select usuario;
                        var username = packet.tag["user"] as string;
                        queryResult.First().EnemigosAbatidos += (int)packet.tag["enemiesKilled"];
                        while(true)
                        {
                            try
                            {
                                databaseFileMutex.WaitOne();
                                database.WriteXml(databaseFile);
                                databaseFileMutex.ReleaseMutex();
                                if (connectedUsers.ContainsKey(username))
                                    server.SendPacket(packet, connectedUsers[username]);
                                break;
                            }
                            catch { }
                        }
                    }
                    break;
            }
        }
        private static void OnUdpPacket(UdpPacket packet)
        {
            switch(packet.PacketType)
            {
                case UdpPacketType.AudioStream:
                    {
                        ServerDataSet.UsuarioPrivadoDataTable usuarioPrivado = database.UsuarioPrivado;
                        int chatID = BitConverter.ToInt32(packet.ReadData(4, 0), 0);
                        int audioStreamLenght = packet.ReadInt(4);
                        int stringLenght = packet.ReadInt(8 + audioStreamLenght);
                        string username = Encoding.Unicode.GetString(packet.ReadData(stringLenght, 12 + audioStreamLenght));
                        //usuarios a mandar
                        var queryResult = from user in usuarioPrivado
                                          where user.ConversacionPrivada == chatID
                                          select user;
                        foreach (var user in queryResult)
                        {
                            if (connectedUsersUdp.ContainsKey(user.Usuario) && user.Usuario != username)
                                server.SendUdpPacket(packet, connectedUsersUdp[user.Usuario]);
                        }
                    }
                    break;
                case UdpPacketType.PlayerInput:
                case UdpPacketType.PlayerShot:
                case UdpPacketType.RandomBotInput:
                    {
                        ServerDataSet.UsuarioPrivadoDataTable usuarioPrivado = database.UsuarioPrivado;
                        int chatID = BitConverter.ToInt32(packet.ReadData(4, 0), 0);
                        int player = packet.ReadInt(4) - 1;
                        //int direction = packet.ReadInt(4);
                        //usuarios a mandar
                        var queryResult = from user in usuarioPrivado
                                          where user.ConversacionPrivada == chatID
                                          select user;
                        foreach (var user in queryResult)
                        {
                            if (connectedUsersUdp.ContainsKey(user.Usuario) && user.Usuario != connectedPlayers[chatID][player])
                                server.SendUdpPacket(packet, connectedUsersUdp[user.Usuario]);
                        }
                    }
                    break;
            }
        }
        //Manejamos cada uno de los eventos que nos pueden llegar en los paquetes
        //http://tekeye.biz/2015/encrypt-decrypt-c-sharp-string
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private const string initVector = "pemgail9uzpgzl88";
        // This constant is used to determine the keysize of the encryption algorithm
        private const int keysize = 256;
        //Encrypt
        public static string EncryptString(string plainText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }
        //Decrypt
        public static string DecryptString(string cipherText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
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
            connectedUsersUdp.Remove(username);
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
                connectedUsersUdp.Remove(user.Key);
                foreach (var connectedUser in connectedUsers)
                {
                    server.SendPacket(packet, connectedUser.Value);
                }
                //disconnect players
                int playerSession = -1;
                foreach(var players in connectedPlayers)
                {
                    foreach(var player in players.Value)
                    {
                        if (player == user.Key)
                            playerSession = players.Key;
                    }
                }
                connectedPlayers.Remove(playerSession);
            }
            Console.WriteLine("Cliente en " + client.RemoteEndPoint + " desconectado");
            server.CloseClientConnection(client);
        }
    }
}