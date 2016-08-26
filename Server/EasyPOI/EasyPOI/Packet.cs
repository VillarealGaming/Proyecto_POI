using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
//TODO: Usar bools en las llamadas, así no podemos hacer 2 peticiones seguidas
namespace EasyPOI
{
    //Me apoye del código de Luis para las siguiente clases
    //Clase que contendra la información que nosotros queramos enviar
    [Serializable]
    public class Packet
    {
        public Packet(PacketType packetType, Object data)
        {
            this.packetType = packetType;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, data);
                this.content = memoryStream.ToArray();
            }
        }
        public void LoadData(byte[] bytes)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                Packet packet = (Packet)binaryFormatter.Deserialize(memoryStream);
                this.content = packet.content;
                this.content = packet.content;
                this.packetType = packet.packetType;
            }
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
        public PacketType Type
        {
            get { return packetType; }
        }
        public T GetContent<T>()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(this.content))
            {
                T content = (T)binaryFormatter.Deserialize(memoryStream);
                return (content);
            }
        }
        private PacketType packetType;
        private byte[] content;
        public const int HeaderSize = 4;
    }
    public enum PacketType
    {
        TextMessage,
        SessionBegin,
        SessionEnd,
        Register
    }
    [Serializable]
    public class SessionData
    {
        public string username { get; set; }
    }
    [Serializable]
    public class TextMessage
    {
        public string message { get; set; }
        //public string user { get; set; }
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
        public const int BufferSize = 8192;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public MemoryStream stream = new MemoryStream();
        //public StringBuilder sb = new StringBuilder();
        //Tamaño del paquete
        public int packetSize;
    }
}