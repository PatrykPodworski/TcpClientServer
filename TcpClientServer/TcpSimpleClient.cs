using System.Net.Sockets;
using System.Text;

namespace TcpClientServer
{
    public class TcpSimpleClient
    {
        private readonly TcpClient _client;

        public TcpSimpleClient(string address, int port)
        {
            this._client = new TcpClient(address, port);
        }

        public string Receive()
        {
            var stream = _client.GetStream();
            byte[] buffer = new byte[1024];
            var bytesReceived = stream.Read(buffer, 0, buffer.Length);

            return Encoding.UTF8.GetString(buffer, 0, bytesReceived);
        }
    }
}
