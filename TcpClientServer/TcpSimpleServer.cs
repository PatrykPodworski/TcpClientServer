using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using TcpClientServer.DataTypes;

namespace TcpClientServer
{
    public class TcpSimpleServer
    {
        private readonly TcpListener _listener;
        private readonly int _maxConnections;
        private readonly List<Task> _processTasks;
        private GameData _gameData;

        public TcpSimpleServer(int port, int maxConnections)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _maxConnections = maxConnections;
            _processTasks = new List<Task>();
            _gameData = new GameData(maxConnections);
        }

        public void Start()
        {
            _listener.Start();
            int connectionCounter = 0;

            while (true)
            {
                // Accepting clients
                var client = _listener.AcceptTcpClient();
                int clientId = connectionCounter++;

                Log($"Client {clientId} connected.");

                // Chosing correct response
                if (_processTasks.Count < _maxConnections)
                {
                    var task = Task.Factory.StartNew(() => Accept(client, clientId));
                    _processTasks.Add(task);
                }
                else
                {
                    Task.Factory.StartNew(() => Reject(client, clientId));
                }
            }
        }

        private void Reject(TcpClient client, int clientId)
        {
            var stream = client.GetStream();

            var dataType = new DataToSend
            {
                ClientState = ClientState.Rejected,
                GameData = _gameData
            };

            // Serializing data to bytes
            byte[] data = dataType.Serialize();

            // Sending data
            stream.Write(data, 0, data.Length);

            Log($"Data sent to id: {clientId}.");

            // Closing the connection
            stream.Close();
            client.Close();

        }

        private void Accept(TcpClient client, int clientId)
        {
            var stream = client.GetStream();

            // Adding new player to game data
            var player = new PlayerData("Michau", clientId);
            _gameData.AddPlayer(player);

            // Creating data to send
            var dataType = new DataToSend
            {
                GameData = _gameData,
                ClientId = clientId,
                ClientState = ClientState.Accepted
            };

            for (int i = 0; i < 1000; i++)
            {
                // Changing data
                player.PositionX = i;
                player.PositionY = 1000 - i;
                // Serializing data to bytes
                var data = dataType.Serialize();

                // Sending data
                stream.Write(data, 0, data.Length);

                Log($"Data sent to id: {clientId}.");

                Thread.Sleep(1000);
            }

            // Closing the connection
            stream.Close();
            client.Close();
        }

        private static void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
