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
    //Clase que encapsula toda la funcionalidad del cliente con algunas funciones multitarea 
    //TODO: hacer callbacks de cada evento que Client pueda presentar
    public class Client
    {
        public Client()
        {
        }
        //Comenzar a buscar una conexión
        public void BeginConnect()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.BeginConnect(IPAddress.Parse(Server.Address)/*IPAddress.Loopback*/, Server.Port, new AsyncCallback(TryConnection), socket);
        }
        //Cerramos la conexion
        public void QuitConnection()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            //connected = false;
        }
        public void Disconnect()
        {
            socket.BeginDisconnect(false, new AsyncCallback(DisconnectCallback), socket);
        }
        private void DisconnectCallback(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            try
            {
                client.EndDisconnect(ar);
            }
            catch(SocketException)
            {

            }
        }
        public void ShutdownConnection()
        {
            socket.Shutdown(SocketShutdown.Both);
        }
        private void TryConnection(IAsyncResult ar)
        {
            // Retrieve the socket from the state object.
            Socket client = (Socket)ar.AsyncState;
            try
            {
                client.EndConnect(ar);
                //connected = true;
                if (onConnection != null) onConnection();
                //Comenzamos a recibir paquetes del servidor
                StateObject state = new StateObject();
                state.workSocket = client;
                socket.BeginReceive(state.buffer, 0, StateObject.BufferSize, SocketFlags.None, new AsyncCallback(Received), state);
            }
            catch (SocketException exception)
            {
                if (onConnectionFail != null) onConnectionFail();
                client.BeginConnect(IPAddress.Loopback, Server.Port, new AsyncCallback(TryConnection), client);
            }
        }
        //Envía un paquete al servidor la información del paquete.
        public void SendPacket(Packet packet)
        {
            //if (socket.Connected)
            //{
                try
                {
                    byte[] data = packet.ToBytes();
                    socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(Send), socket);
                    //socket.Send(packet.ToBytes());
                }
                catch (SocketException exception)
                {
                    //if (onConnectionFail != null) onConnectionFail();
                    //connected = false;
                }
            //}
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
            //if (socket.Connected)
            //{
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
                                    if (onPacketReceived != null) onPacketReceived(packet);
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
                                    if (onPacketReceived != null) onPacketReceived(packet);
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
            //}
        }
        //getters y setters
        public bool Connected
        {
            get { return socket.Connected; }
        }
        public Action OnConnection
        {
            set { onConnection = value; }
        }
        public Action OnConnectionFail
        {
            set { onConnectionFail = value; }
        }
        public Action OnConnectionLost
        {
            set { onConnectionLost = value; }
        }
        public void OnPacketReceivedFunc(Action<Packet> func)
        {
            onPacketReceived = func;
        }
        //private bool connected;
        private Socket socket;
        //callbacks
        private Action onConnection;
        private Action onConnectionFail;
        private Action onConnectionLost;
        private Action<Packet> onPacketReceived;
    }
}
