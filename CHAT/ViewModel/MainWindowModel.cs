using CHAT.Extension_Methods;
using CHAT.Helpers;
using System;
using System.Net.Sockets;
using System.Windows.Input;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;

namespace CHAT.ViewModel
{
    class MainWindowModel : BaseViewModel
    {
        #region Binding
        private string _NameBox = "ivan";
        public string NameBox
        {
            get { return _NameBox; }
            set { _NameBox = value; OnPropertyChanged(); }
        }
        private string _IpText = "127.0.0.1";
        public string IpText
        {
            get { return _IpText; }
            set { _IpText = value; OnPropertyChanged(); }
        }
        private string _PortText = "8080";
        public string PortText
        {
            get { return _PortText; }
            set { _PortText = value; OnPropertyChanged(); }
        }
        private string _MessageText;
        public string MessageText
        {
            get { return _MessageText; }
            set { _MessageText = value; OnPropertyChanged(); }
        }
        private string _ChatPanel;
        public string ChatPanel
        {
            get { return _ChatPanel; }
            set { _ChatPanel = value; OnPropertyChanged(); }
        }
        #endregion
        private static NetworkStream _stream;
        private static TcpClient _tcpClient;
        public MainWindowModel()
        {
            _tcpClient = new TcpClient();
        }

        public ICommand ClickToConnect => new DelegateCommands((obj) =>
        {
            ConnectToServer(IpText, PortText.ParseInt(), NameBox);
            Task.Run(() => BroadCastingMessage());
        });


        public ICommand ClickToSendMessage => new DelegateCommands((obj) =>
        {
            SendMessageToServer($"{DateTime.Now.ToLongTimeString()} {NameBox}: {obj}");
            obj = null;
        });
        public ICommand ClickToDisconnect => new DelegateCommands((obj) =>
        {
            DisconnectToServer();
        });

        private void ConnectToServer(string ip, int port, string name)
        {
            try
            {
                if (!string.IsNullOrEmpty(ip) && port > 0)
                {
                    _tcpClient.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
                    _stream = _tcpClient.GetStream();
                    SendMessageToServer($"{name}:Подключен");
                }
            }
            catch
            {
                ChatPanel = "Ошибка подключение";
            }
        }

        public void BroadCastingMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = _stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (_stream.DataAvailable);

                    ChatPanel += builder.ToString() + "\n";
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        public static void SendMessageToServer(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                _stream.Write(message.EncodingMessageInByte(), 0, message.EncodingMessageInByte().Length);
            }
        }

        public  void DisconnectToServer()
        {
            if (_tcpClient != null)
            {
                SendMessageToServer($"{DateTime.Now.ToLongTimeString()} {NameBox}: пользователь вышел с сервера");
                _tcpClient.Close();
                ChatPanel = "Connection lost";
                Environment.Exit(1);
            }
        }
    }
}
