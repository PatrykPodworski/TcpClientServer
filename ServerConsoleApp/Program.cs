using TcpClientServer;

namespace ServerConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var server = new TcpSimpleServer(7777);
            server.Start();
        }
    }
}
