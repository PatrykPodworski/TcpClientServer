using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace TcpClientServer
{
    public class TcpSimpleServer
    {
        private readonly TcpListener _listener;
        private readonly int _maxConnections;
        private readonly List<Task> _processTasks;

        public TcpSimpleServer(int port, int maxConnections)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _maxConnections = maxConnections;
            _processTasks = new List<Task>();
        }

        public void Start()
        {
            _listener.Start();
            while (_processTasks.Count < _maxConnections)
            {
                // Accepting clients
                var client = _listener.AcceptTcpClient();
                int clientId = _processTasks.Count;

                Log($"Client {clientId} connected.");

                var task = Task.Factory.StartNew(() => Proccess(client, clientId));
                _processTasks.Add(task);
            }

            Task.WaitAll(_processTasks.ToArray());
        }

        private void Proccess(TcpClient client, int clientId)
        {
            var stream = client.GetStream();

            for (int i = 0; i < 1000; i++)
            {
                // Creating data to send
                var dataType = new DataType
                {
                    Name = $"Michau {clientId}",
                    PositionX = i,
                    PositionY = 1000 - i
                };

                // Serializing data to bytes
                byte[] data;

                IFormatter formatter = new BinaryFormatter();
                using (var ms = new MemoryStream())
                {
                    formatter.Serialize(ms, dataType);
                    data = ms.ToArray();
                }

                // Sending data
                stream.Write(data, 0, data.Length);

                Log("Data sent.");

                Thread.Sleep(1000);
            }

            stream.Close();
            client.Close();
        }

        private void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
