using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TcpClientServer
{
    public class TcpSimpleClient
    {
        private readonly TcpClient _client;

        public TcpSimpleClient(string address, int port)
        {
            _client = new TcpClient(address, port);
        }

        public DataType Receive()
        {
            // Receiving data bytes from server
            var stream = _client.GetStream();
            byte[] buffer = new byte[1024];
            var bytesReceived = stream.Read(buffer, 0, buffer.Length);

            // Deserializing bytes
            IFormatter formatter = new BinaryFormatter();
            DataType dataReceived;
            using (var ms = new MemoryStream(buffer))
            {
                dataReceived = formatter.Deserialize(ms) as DataType;
            }

            // Returning the result
            return dataReceived;
        }
    }
}
