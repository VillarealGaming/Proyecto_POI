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
    //Me apoye del código de Luis para las siguiente clases
    //Clase que contendra la información que nosotros queramos enviar
    class Packet
    {
        //Toda la información util del paquete
        [Serializable]
        public class Data
        {
            public string packetType;
            public int port;
            public byte[] content;
        }
        public string PacketType
        {
            get { return data.packetType; }
        }
        public byte[] PacketContent
        {
            get { return data.content; }
        }
        public Packet(string packetType, Object data)
        {
            this.data.packetType = packetType;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, data);
                this.data.content = memoryStream.ToArray();
            }
        }
        public void LoadData(byte[] bytes)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                data = (Data)binaryFormatter.Deserialize(memoryStream);
            }
        }
        public byte[] GetDataBytes()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, data);
                return memoryStream.ToArray();
            }
        }
        private Data data;
    }
    //Clase que encapsula toda la funcionalidad del cliente con algunas funciones multitarea 
    //TODO: hacer callbacks de cada evento que Client pueda presentar
    //Referencia
    //https://msdn.microsoft.com/en-us/library/bew39x2a(v=vs.110).aspx
    class Client
    {
        //Comenzar a buscar una conexión
        public void BeginConnect(int port = DefaultPort)
        {
            //Cerramos la conexión antes de crear una conexión nueva
            if (connected)
                CloseConnection();
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.BeginConnect(IPAddress.Loopback, port, new AsyncCallback(TryConnection), socket);
            this.port = port;
        }
        //Cerramos la conexion
        public void CloseConnection()
        {
            if(connected)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                connected = false;
            }
        }
        //Envía un paquete al servidor, la información del paquete.
        //Esta función no es multitarea.
        public void SendPacket(Packet packet)
        {
            try
            {
                socket.Send(packet.GetDataBytes());
            }
            catch (SocketException exception)
            {
                if (onConnectionFail != null) onConnectionFail();
                connected = false;
            }
        }
        private void TryConnection(IAsyncResult ar)
        {
            // Retrieve the socket from the state object.
            Socket client = (Socket)ar.AsyncState;
            try
            {
                client.EndConnect(ar);
                connected = true;
                if (onConnection != null) onConnection();
                //Comenzamos a recibir paquetes del servidor
                socket.BeginReceive(new byte[256], 0, 256, SocketFlags.None, new AsyncCallback(Receive), client);
            }
            catch (SocketException exception)
            {
                if (onConnectionFail != null) onConnectionFail();
                client.BeginConnect(IPAddress.Loopback, port, new AsyncCallback(TryConnection), client);
            }
        }
        private void Receive(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            try
            {
                int bytesRead = client.EndReceive(ar);
                //TODO: Continuar leyendo bytes
                int i = 0; //testear los bytes enviados;
            }
            catch (SocketException exception)
            {
                if (onConnectionFail != null) onConnectionFail();
                connected = false;
            }
        }
        //getters y setters
        public bool Connected
        {
            get { return connected; }
        }
        public int Port
        {
            get { return port; }
        }
        public Action OnConnection
        {
            set { onConnection = value; }
        }
        public Action OnConnectionFail
        {
            set { onConnectionFail = value; }
        }
        public Action<Packet> OnReceive
        {
            set { onReceive = value; }
        }
        private bool connected;
        private int port;
        private Socket socket;
        //callbacks
        private Action onConnection;
        private Action onConnectionFail;
        private Action<Packet> onReceive;
        private const int DefaultPort = 100;
    }
}