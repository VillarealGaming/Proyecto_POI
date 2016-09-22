using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.InteropServices;
namespace EasyPOI
{
    //Clase que contendra la información que nosotros queramos enviar
    [Serializable]
    public class Packet
    {
        public Packet(PacketType type)
        {
            tag = new Dictionary<string, object>();
            this.type = type;
        }
        public byte[] ToBytes()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream finalStream = new MemoryStream())
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, this);
                int headerInfo = (int)memoryStream.Length + HeaderSize;
                finalStream.Write(BitConverter.GetBytes(headerInfo), 0, HeaderSize);
                finalStream.Write(memoryStream.ToArray(), 0, (int)memoryStream.Length);
                return finalStream.ToArray();
            }
        }
        public PacketType Type { get { return type; } }
        private PacketType type;
        public Dictionary<string, Object> tag;
        public const int HeaderSize = 4;
    }
    //Super hardcoded udp packet class...
    public class UdpPacket
    {
        private MemoryStream data;
        private byte[] dataBytes;
        private UdpPacketType packetType;
        public UdpPacketType PacketType{
            get { return packetType; }
        }
        public byte[] Data
        {
            get{
                if(dataBytes == null)
                {
                    dataBytes = data.ToArray();
                }
                return dataBytes;
            }
        }

        public byte[] ReadData(int lenght, int position) { return dataBytes.Skip(position).Take(lenght).ToArray(); }
        public int ReadInt(int position) { return BitConverter.ToInt32(dataBytes, position); }
        public void WriteData(byte[] bytes) { data.Write(bytes, 0, bytes.Length); }
        public void WriteData(byte[] bytes, int offset) { data.Write(bytes, offset, bytes.Length - offset); }

        public UdpPacket(UdpPacketType packetType)
        {
            data = new MemoryStream();
            this.packetType = packetType;
        }
        protected UdpPacket(UdpPacketType packetType, byte[] bytes)
        {
            data = new MemoryStream();
            dataBytes = bytes;
            this.packetType = packetType;
        }
        public byte[] ToBytes()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(BitConverter.GetBytes((int)packetType), 0, sizeof(int));
                if (dataBytes == null)
                {
                    byte[] bytes = data.ToArray();
                    stream.Write(bytes, 0, bytes.Length);
                }
                else
                {
                    stream.Write(dataBytes, 0, dataBytes.Length);
                }
                return stream.ToArray();
            }
        }
        public static UdpPacket CreateFromStream(byte[] bytes)
        {
            UdpPacketType packetType = (UdpPacketType)BitConverter.ToInt32(bytes, 0);
            UdpPacket packet = new UdpPacket(packetType, bytes.Skip(4).ToArray());
            return packet;
        }
    }
    //Tal vez esto vaya mejor en el archivo de Client.cs
    public enum UserConnectionState
    {
        Available,      //Disponible
        NotAvailable,   //No disponible
        Busy,           //Ocupado
        Offline         //Desconectado
    }
    public enum PacketType
    {
        TextMessage,
        PrivateTextMessage,
        SessionBegin,
        SessionEnd,
        Fail,
        Register,
        ConnectionState,
        CreatePublicConversation,
        CreatePrivateConversation,
        GetUserConversations,
        GetPrivateConversations,
        GetUsers,
        SetUserState,
        CreatePublicConversationFail,
        Buzz,
        PrivateBuzz,
        FileSendChat,
        FileSendPrivate,
        WebCamRequest,
        WebCamResponse,
        WebCamFrame,
        UdpLocalEndPoint
        //
    }
    public enum UdpPacketType
    {
        AudioStream
    }
    //Referencia
    //https://msdn.microsoft.com/en-us/library/bew39x2a(v=vs.110).aspx
    //Reading data from a client socket requires a state object that passes values between asynchronous calls. 
    //The following class is an example state object for receiving data from a client socket. 
    //It contains a field for the client socket, a buffer for the received data, and a StringBuilder to hold 
    //the incoming data string. Placing these fields in the state object allows their values to be preserved 
    //across multiple calls to read data from the client socket.
    public class StateObject
    {
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 512;//8192;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public MemoryStream stream = new MemoryStream();
        //public StringBuilder sb = new StringBuilder();
        //Tamaño del paquete
        public int packetSize;
    }
    public class UdpState
    {
        public UdpClient client = null;
        public IPEndPoint ipEndPoint = null;
    }
    //similar a la clase de packete normal, solo que omitira
}