using System.Net;
using System.Text;

namespace CHAT.Extension_Methods
{
    class Extensions
    {
        public IPAddress ParseIPAddress(string ip)
        {
            IPAddress iPAddress;
            if (!string.IsNullOrWhiteSpace(ip))
            {
                byte[] data = Encoding.UTF8.GetBytes(ip);
                iPAddress = new IPAddress(data);
            }
            return new IPAddress(new long());
        }
    }
}
