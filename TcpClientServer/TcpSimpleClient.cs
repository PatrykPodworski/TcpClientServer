using System;
using System.Net.Sockets;
using TcpClientServer.DataTypes;

namespace TcpClientServer
{
    public class TcpSimpleClient
    {
        private readonly TcpClient _client;

        public TcpSimpleClient(string address, int port)
        {
            _client = new TcpClient(address, port);
        }

        public DataToSend Receive()
        {
            // Receiving data bytes from server
            var stream = _client.GetStream();
            var buffer = new byte[2048];
            stream.Read(buffer, 0, buffer.Length);

            // Deserializing bytes
            var dataReceived = DataToSend.Deserialize(buffer);

            // Returning the result
            return dataReceived;
        }

        public void Run()
        {
            while (true)
            {
                // Receiving data
                var data = Receive();
                Log(data.ToString());

                // Checking if client was rejected
                if (data.ClientState == ClientState.Rejected)
                {
                    Log("Client was rejected");
                    _client.Close();
                    break;
                }
            }
        }

        private static void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
