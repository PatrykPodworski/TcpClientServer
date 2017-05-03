using System;
using TcpClientServer;

namespace ServerConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            const int port = 7777;

            Console.WriteLine("Starting server...");
            var server = new TcpSimpleServer(port);
            Console.WriteLine($"Started listening on port: {port}");
            server.Start();
        }
    }
}
