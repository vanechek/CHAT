using System.Net;
using System.Text;

namespace CHAT.Extension_Methods
{
    static public class Extensions
    {
        public static IPAddress ParseIPAddress(this string ip)
        {
            IPAddress iPAddress;
            if (!string.IsNullOrWhiteSpace(ip))
            {
                byte[] data = Encoding.UTF8.GetBytes(ip);
                iPAddress = new IPAddress(data);
            }
            return new IPAddress(new long());
        }

        public static byte[] EncodingMessageInByte(this string message)
        {
           byte[] data = Encoding.Unicode.GetBytes(message);
            return data;
        }
        public static int ParseInt(this string line)
        {
            int result;
            if (int.TryParse(line, out result))
            {
                return result;
            }
            return result;
        }
    }
}
