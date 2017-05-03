using System.Net;
using System.Net.Sockets;
using System.Text;
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

                Task.Factory.StartNew(() => this.Proccess(client));
            }
        }

        private void Proccess(TcpClient client)
        {
            var stream = client.GetStream();
            byte[] data = Encoding.UTF8.GetBytes("Hello from server!");
            stream.Write(data, 0, data.Length);
            stream.Close();
            client.Close();
        }
    }
}
