using System;
using System.Net.Sockets;

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
            var buffer = new byte[1024];
            stream.Read(buffer, 0, buffer.Length);

            // Deserializing bytes
            var dataReceived = DataType.Deserialize(buffer);

            // Returning the result
            return dataReceived;
        }

        public void Run()
        {
            while (true)
            {
                // Receiving data
                var data = Receive();
                Log($"Game state: {data.GameState}, Name: {data.Name}, X: {data.PositionX}, Y: {data.PositionY}");

                // Checking if client was rejected
                if (data.GameState == GameState.Rejected)
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
