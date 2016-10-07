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
        private const int listenTime = 10;
        internal const int TcpPort = 6666;
        internal const int UdpPort = 7777;
        internal const string Address = "192.168.0.9";
        public Server()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, Server.TcpPort));
        }
        //Comenzamos a buscar clientes
        public void StartListening()
        {
            //clients = new List<Socket>();
            socket.Listen(listenTime);
            socket.BeginAccept(new AsyncCallback(AcceptClient), null);
            //udp
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, UdpPort);
            //udpListener = new UdpClient(ipEndPoint);
            udpListener = new UdpClient();
            udpListener.ExclusiveAddressUse = false;
            udpListener.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpListener.Client.Bind(ipEndPoint);
            UdpState state = new UdpState();
            state.client = udpListener;
            state.ipEndPoint = ipEndPoint;
            udpListener.BeginReceive(new AsyncCallback(UdpReceive), state);
        }
        //https://msdn.microsoft.com/en-us/library/system.net.sockets.udpclient.beginreceive(v=vs.110).aspx
        private void UdpReceive(IAsyncResult ar)
        {
            UdpState state = ((UdpState)ar.AsyncState);
            UdpClient client = state.client;
            IPEndPoint ipEndPoint = state.ipEndPoint;
            byte[] bytes = client.EndReceive(ar, ref ipEndPoint);
            //read data
            if (OnUdpPacketReceived != null)
                OnUdpPacketReceived(UdpPacket.CreateFromStream(bytes));

            //
            ipEndPoint = new IPEndPoint(IPAddress.Any, UdpPort);
            //udpListener = new UdpClient();
            //udpListener.ExclusiveAddressUse = false;
            //udpListener.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            //udpListener.Client.Bind(ipEndPoint);
            state = new UdpState();
            state.client = udpListener;
            state.ipEndPoint = ipEndPoint;
            try
            {
                udpListener.BeginReceive(new AsyncCallback(UdpReceive), state);
            }
            catch
            {

            }
            //udpListener.BeginReceive(new AsyncCallback(UdpReceive), state);
        }
        public void SendUdpPacket(UdpPacket packet, IPEndPoint ipEndPoint)
        {
            //try
            //{
            byte[] data = packet.ToBytes();
            if (data.Length < 1024)
                udpListener.BeginSend(data, data.Length, ipEndPoint, new AsyncCallback(SendUdp), udpListener);
            //}
            //catch
            //{

            //}
        }
        private void SendUdp(IAsyncResult ar)
        {
            try
            {
                UdpClient client = (UdpClient)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
            }
            catch (SocketException exception)
            {
                //if (onConnectionFail != null) onConnectionFail();
                //connected = false;
            }
        }
        private void AcceptClient(IAsyncResult ar)
        {
            Socket client = socket.EndAccept(ar);
            //clients.Add(client);
            StateObject state = new StateObject();
            state.workSocket = client;
            socket.BeginAccept(new AsyncCallback(AcceptClient), null);
            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, SocketFlags.None, new AsyncCallback(Received), state);
            if (onClientAccepted != null)
                onClientAccepted(client);
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
                if (OnClientDisconnect != null) OnClientDisconnect(client);
                //if (onConnectionFail != null) onConnectionFail();
                // connected = false;
            }
        }
        //
        public void CloseClientConnection(Socket client)
        {
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
        private void Send(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            try
            {
                int bytesSent = client.EndSend(ar);
                int a = 0;
            }
            catch (SocketException exception)
            {
                if(OnClientDisconnect != null) OnClientDisconnect(client);
                //if (onConnectionFail != null) onConnectionFail();
                //connected = false;
            }
        }
        private async void Received(IAsyncResult ar)
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
                        await state.stream.WriteAsync(state.buffer, 0, bytesRead);
                        //state.sb.Append(Encoding.ASCII.GetString(
                        //state.buffer, 0, bytesRead));
                        //Leímos toda el paquete
                        if (state.stream.Length == state.packetSize)//(state.sb.Length  == state.packetSize)
                        {
                            // All the data has been read from the 
                            using (MemoryStream memoryStream = new MemoryStream(state.stream.ToArray(), Packet.HeaderSize, state.packetSize - Packet.HeaderSize))
                            {
                                BinaryFormatter binaryFormatter = new BinaryFormatter();
                                state.stream = new MemoryStream();
                                Packet packet = (Packet)binaryFormatter.Deserialize(memoryStream);
                                if (OnPacketReceived != null)
                                    OnPacketReceived(packet, client);
                                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, SocketFlags.None, new AsyncCallback(Received), state);
                                //SendPacket(packet, client);
                                //client.BeginReceive(state.buffer, 0, StateObject.BufferSize, SocketFlags.None, new AsyncCallback(Received), state);
                            }
                        }
                        else
                        {
                            //Seguimos leyendo los bytes
                            if (state.packetSize - state.stream.Length > StateObject.BufferSize)
                                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, SocketFlags.None, new AsyncCallback(Received), state);
                            else
                                client.BeginReceive(state.buffer, 0, state.packetSize - (int)state.stream.Length, SocketFlags.None, new AsyncCallback(Received), state);
                        }
                    }
                    else
                    {
                        //Seguimos leyendo el encabezado
                        //Seguimos leyendo los bytes y dejamos la información del header del paquete
                        if (state.packetSize - state.stream.Length > StateObject.BufferSize)
                            client.BeginReceive(state.buffer, bytesRead, StateObject.BufferSize - bytesRead, SocketFlags.None, new AsyncCallback(Received), state);
                        else
                            client.BeginReceive(state.buffer, bytesRead, state.packetSize - (int)state.stream.Length - bytesRead, SocketFlags.None, new AsyncCallback(Received), state);
                    }
                }
                else
                {
                    //Estamos continuando la lectura de un paquete
                    await state.stream.WriteAsync(state.buffer, 0, bytesRead);
                    //Checar si leímos los primeros 4 bytes del encabezado
                    if (state.stream.Length >= Packet.HeaderSize)
                    {
                        if (state.stream.Length == state.packetSize)//(state.sb.Length  == state.packetSize)
                        {
                            //Leímos todo el paquete, ya podemos deserializar
                            using (MemoryStream memoryStream = new MemoryStream(state.stream.ToArray(), Packet.HeaderSize, state.packetSize - Packet.HeaderSize))
                            {
                                BinaryFormatter binaryFormatter = new BinaryFormatter();
                                state.stream = new MemoryStream();
                                Packet packet = (Packet)binaryFormatter.Deserialize(memoryStream);
                                if (OnPacketReceived != null)
                                    OnPacketReceived(packet, client);
                                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, SocketFlags.None, new AsyncCallback(Received), state);
                            }
                        }
                        else
                        {
                            //Seguimos leyendo los bytes
                            if (state.packetSize - state.stream.Length > StateObject.BufferSize)
                                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, SocketFlags.None, new AsyncCallback(Received), state);
                            else
                                client.BeginReceive(state.buffer, 0, state.packetSize - (int)state.stream.Length, SocketFlags.None, new AsyncCallback(Received), state);
                        }
                    }
                    else
                    {
                        //Seguimos leyendo el encabezado
                        //Seguimos leyendo los bytes y dejamos la información del header del paquete
                        if (state.packetSize - state.stream.Length > StateObject.BufferSize)
                            client.BeginReceive(state.buffer, bytesRead, StateObject.BufferSize - bytesRead, SocketFlags.None, new AsyncCallback(Received), state);
                        else
                            client.BeginReceive(state.buffer, bytesRead, state.packetSize - (int)state.stream.Length - bytesRead, SocketFlags.None, new AsyncCallback(Received), state);
                    }
                }
            }
            catch (SocketException)
            {
                //Cliente desconectado
                if (OnClientDisconnect != null) OnClientDisconnect(client);
            }
        }
        //setter functions
        public void SetClientAcceptedFunc(Action<Socket> func)
        {
            onClientAccepted = func;
        }
        //private Action<string> DisconnectUser;
        //private Action<Socket> OnClientDisconnect;
        public void SetPacketReceivedFunc(Action<Packet, Socket> func)
        {
            OnPacketReceived = func;
        }
        public void SetOnClientDisconnectFunc(Action<Socket> func)
        {
            OnClientDisconnect = func;
        }
        public void SetOnUdpPacketReceived(Action<UdpPacket> func)
        {
            OnUdpPacketReceived = func;
        }
        //Manejamos cada uno de los eventos que nos pueden llegar en los paquetes
        private Action<Socket> onClientAccepted;
        private Action<Packet, Socket> OnPacketReceived;
        private Action<UdpPacket> OnUdpPacketReceived;
        //private Action<Socket> onClientDisconnect;
        private Action<Socket> OnClientDisconnect;
        private Socket socket;
        private UdpClient udpListener;
        //extensions
        //private List<Socket> clients;
    }
}
