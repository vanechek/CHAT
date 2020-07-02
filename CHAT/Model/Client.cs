using System;
using System.Net.Sockets;

namespace CHAT.Model
{
    public class Client
    {
        public Client(string id, string name, TcpClient tcpClient)
        {
            Id = id;
            Name = name;
            TcpClient = tcpClient;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public TcpClient TcpClient { get; set; }

    }
}
