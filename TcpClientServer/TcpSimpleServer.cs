using System;
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

        public TcpSimpleServer(int port)
        {
            this._listener = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            _listener.Start();
            while (true)
            {
                var client = _listener.AcceptTcpClient();
                Log("Client connected.");

                Task.Factory.StartNew(() => this.Proccess(client));
            }
        }

        private void Proccess(TcpClient client)
        {
            var stream = client.GetStream();

            for (int i = 0; i < 1000; i++)
            {
                // Creating data to send
                var dataType = new DataSend()
                {
                    Name = "Michau",
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
