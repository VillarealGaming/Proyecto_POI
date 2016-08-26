using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace EasyPOI
{
    public class Server
    {
        public Server(int port = DefaultPort)
        {
            connectedUsers = new Dictionary<string, Socket>();
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
        }
        //Comenzamos a buscar clientes
        public void StartListening()
        {
            //clients = new List<Socket>();
            socket.Listen(listenTime);
            socket.BeginAccept(new AsyncCallback(AcceptClient), null);
            this.port = port;
        }
        private void Send()
        {

        }
        private void AcceptClient(IAsyncResult ar)
        {
            Socket client = socket.EndAccept(ar);
            //clients.Add(client);
            StateObject state = new StateObject();
            state.workSocket = client;
            socket.BeginAccept(new AsyncCallback(AcceptClient), null);
            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, SocketFlags.None, new AsyncCallback(Received), state);
            //if (onClientAccepted != null)
                onClientAccepted();
        }
        //Envía un paquete al cliente la información del paquete.
        public void SendPacket(Packet packet, Socket client)
        {
            try
            {
                byte[] data = packet.ToBytes();
                client.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(Send), client);
                //socket.Send(packet.ToBytes());
            }
            catch (SocketException exception)
            {
                //if (onConnectionFail != null) onConnectionFail();
                // connected = false;
            }
        }
        private void Send(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
            }
            catch (SocketException exception)
            {
                //if (onConnectionFail != null) onConnectionFail();
                //connected = false;
            }
        }
        private void Received(IAsyncResult ar)
        {
            //Estoy casí seguro que el siguiente código (desastrozo uso de if) se puede optimizar,
            //no obstante es algo que dejare así por el momento
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;
            try
            {
                int bytesRead = client.EndReceive(ar);
                //Checar si stream esta vacío, así sabemos si es el primer paquete
                if (state.stream.Length < 1)//(state.sb.Length < 1)
                {
                    //Checar si leímos los primeros 4 bytes del encabezado
                    if (bytesRead >= Packet.HeaderSize)
                    {
                        state.packetSize = BitConverter.ToInt32(state.buffer, 0);//.Take(4).ToArray()
                        state.stream.Write(state.buffer, 0, bytesRead);
                        //state.sb.Append(Encoding.ASCII.GetString(
                        //state.buffer, 0, bytesRead));
                        //Leímos toda el paquete
                        if (state.stream.Length == state.packetSize)//(state.sb.Length  == state.packetSize)
                        {
                            // All the data has been read from the 
                            BinaryFormatter binaryFormatter = new BinaryFormatter();
                            using (MemoryStream memoryStream = new MemoryStream(state.stream.ToArray(), Packet.HeaderSize, state.packetSize - Packet.HeaderSize))
                            {
                                state.stream = new MemoryStream();
                                Packet packet = (Packet)binaryFormatter.Deserialize(memoryStream);
                                //if (onPacketReceived != null)
                                    onPacketReceived(packet, client);
                                //SendPacket(packet, client);
                                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, SocketFlags.None, new AsyncCallback(Received), state);
                            }
                        }
                        else
                        {
                            //Seguimos leyendo los bytes
                            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, SocketFlags.None, new AsyncCallback(Received), state);
                        }
                    }
                    else
                    {
                        //Seguimos leyendo el encabezado
                        //Seguimos leyendo los bytes y dejamos la información del header del paquete
                        client.BeginReceive(state.buffer, bytesRead, StateObject.BufferSize - bytesRead, SocketFlags.None, new AsyncCallback(Received), state);
                    }
                }
                else
                {
                    //Estamos continuando la lectura de un paquete
                    state.stream.Write(state.buffer, 0, bytesRead);
                    //Checar si leímos los primeros 4 bytes del encabezado
                    if (state.stream.Length >= state.packetSize)
                    {
                        if (state.stream.Length == state.packetSize)//(state.sb.Length  == state.packetSize)
                        {
                            //Leímos todo el paquete, ya podemos deserializar
                            BinaryFormatter binaryFormatter = new BinaryFormatter();
                            using (MemoryStream memoryStream = new MemoryStream(state.stream.ToArray(), Packet.HeaderSize, state.packetSize - Packet.HeaderSize))
                            {
                                state.stream = new MemoryStream();
                                Packet packet = (Packet)binaryFormatter.Deserialize(memoryStream);
                                //if (onPacketReceived != null)
                                    onPacketReceived(packet, client);
                                SendPacket(packet, client);
                                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, SocketFlags.None, new AsyncCallback(Received), state);
                            }
                        }
                        else
                        {
                            //Seguimos leyendo los bytes
                            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, SocketFlags.None, new AsyncCallback(Received), state);
                        }
                    }
                    else
                    {
                        //Seguimos leyendo el encabezado
                        //Seguimos leyendo los bytes y dejamos la información del header del paquete
                        client.BeginReceive(state.buffer, bytesRead, StateObject.BufferSize - bytesRead, SocketFlags.None, new AsyncCallback(Received), state);
                    }
                }
            }
            catch (SocketException)
            {
                //Cliente desconectado
            }
        }
        private int port;
        private Socket socket;
        private void onClientAccepted()
        {

        }
        //Manejamos cada uno de los eventos que nos pueden llegar en los paquetes
        private void onPacketReceived(Packet packet, Socket client)
        {
            switch(packet.Content.Type)
            {
                case PacketType.SessionBegin:
                    {
                        SessionBegin sessionBegin = packet.Content as SessionBegin;
                        connectedUsers.Add(sessionBegin.username, client);
                    }
                    break;
                case PacketType.Register:
                    break;
                case PacketType.TextMessage:
                    {
                        TextMessage textMessage = packet.Content as TextMessage;
                        foreach(var user in connectedUsers)
                        {
                            SendPacket(packet, user.Value);
                        }
                    }
                    break;
            }
        }
        //private Action<Socket> onClientDisconnect;
        private void onClientDisconnect()
        {

        }
        private const int DefaultPort = 100;
        private const int listenTime = 10;
        //extensions
        private Dictionary<string, Socket> connectedUsers;
        //private List<Socket> clients;
    }
}
