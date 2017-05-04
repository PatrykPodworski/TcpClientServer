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

            var client = new TcpSimpleClient(address, port);
            client.Run();

            Console.ReadLine();
        }
    }
}
