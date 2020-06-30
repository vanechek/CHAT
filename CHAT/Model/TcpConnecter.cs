using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace CHAT.Model
{
    class TcpConnecter
    {
        private Client client;

        public TcpConnecter(Client client)
        {
            if (client != null)
            {
                this.client = client;
            }
        }
        public string Connect(string address, int port)
        {
            string message ;
            try
            {
                client.TcpClient.BeginConnect(address, port, (requestCallback) =>
                {
                    client.TcpClient = requestCallback.AsyncState as TcpClient;
                    if (client.TcpClient.Connected)
                    {
                        client.TcpClient.EndConnect(requestCallback);
                    }
                }, client.TcpClient);
                return message = "Подключен"; ;
            }
            catch(Exception ex)
            {
                message = "Ошибка подключения";
            }
            return message;
        }
    }
}
