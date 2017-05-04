using System;
using TcpClientServer;

namespace ClientConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const int port = 7777;
            const string address = "localhost";
            Console.WriteLine("Starting client...");
            var client = new TcpSimpleClient(address, port);
            Console.WriteLine($"Connected to: {address} on port: {port}");
            client.Run();
            Console.ReadLine();
        }
    }
}
