using System;
using System.Net.Sockets;

namespace CHAT.Model
{
    class Client
    {
        public Client(Guid id, string name, TcpClient tcpClient)
        {
            Id = id;
            Name = name;
            TcpClient = tcpClient;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public TcpClient TcpClient { get; set; }

    }
}
