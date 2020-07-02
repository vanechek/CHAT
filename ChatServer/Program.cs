using CHAT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class Program
    {
        private static List<Client> Clients = new List<Client>();
        private static TcpListener tcpListener;
        private static TcpClient tcpClient;
        static void Main(string[] args)
        {
            Console.WriteLine("Сервер запускается");
            StartServer();
        }

        private static void StartServer()
        {
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080);
            tcpListener.Start();
            while (true)
            {
                var newUser = tcpListener.AcceptTcpClient();
                Task.Run(() =>
                {
                    tcpClient = newUser;
                    var streamUser = newUser.GetStream();
                    ListenerMessage(streamUser);
                });

            }
        }

        private static void SendMessage()
        {
            Console.WriteLine("Введите сообщения для отправки в чат");
            var line = Console.ReadLine();
            if (!string.IsNullOrEmpty(line))
            {
                byte[] buffer = Encoding.Unicode.GetBytes(line);
                stream.Write(buffer, 0, buffer.Length);
            }
        }

        private static void ListenerMessage(NetworkStream networkStream)
        {
            while (true)
            {
                byte[] buffer = new byte[64];
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = networkStream.Read(buffer, 0, buffer.Length);
                    builder.Append(Encoding.Unicode.GetString(buffer, 0, buffer.Length));
                } while (networkStream.DataAvailable);
                string message = builder.ToString();
                CheckMessage(message);
                BroadCastingMessage(message);
                Console.WriteLine(message);
            }
        }
        private static void BroadCastingMessage(string message)
        {
            foreach (var client in Clients)
            {
                var stream = client.TcpClient.GetStream();
                byte[] buffer = Encoding.Unicode.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);
            }
        }
        private static void CheckMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                string userName = "";
                if (message.Contains(":Подключен"))
                {
                    userName = message.Replace(":Подключен", "");
                    if (Clients.FirstOrDefault((listClients) => listClients.Name == userName) == null)
                    {
                        Client client = new Client(Guid.NewGuid().ToString(), userName, tcpClient);
                        Clients.Add(client);
                    }
                }
            };
        }
    }
}
