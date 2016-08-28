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
    [Serializable]
    public class Packet
    {
        public Packet(PacketContent content)
        {
            this.content = content;
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
        public PacketContent Content
        {
            get { return content; }
        }
        private PacketContent content;
        public const int HeaderSize = 4;
    }
    [Serializable]
    public class PacketContent
    {
        protected PacketType type;
        //public PacketContent() { }
        public PacketContent(PacketType type) { this.type = type; }
        public PacketType Type { get { return type; } }
        public string message { get; set; }
    }
    [Serializable]
    public class SessionBegin : PacketContent
    {
        public SessionBegin() : base(PacketType.SessionBegin) { }
        public string password { get; set; }
    }
    [Serializable]
    public class TextMessage : PacketContent
    {
        public TextMessage() : base(PacketType.TextMessage) { }
        //public string message { get; set; }
        public string destination { get; set; }
        public string sender { get; set; }
    }
    [Serializable]
    public class UserState : PacketContent
    {
        public UserState() : base(PacketType.ConnectionState){ }
        //public UserConnectionState connectionState { get; set; }
    }
    [Serializable]
    public class RegisterPacket : PacketContent
    {
        public RegisterPacket() : base(PacketType.Register) { }
        //public string username { get; set; }
        public string password { get; set; }
        public string carrera { get; set; }
        public bool encrypt { get; set; }
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
        SessionBegin,
        SessionEnd,
        Register,
        ConnectionState,
        RegisterFail,
        RegisterSucessfull
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