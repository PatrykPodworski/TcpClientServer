using System;
using TcpClientServer;

namespace ClientConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new TcpSimpleClient("localhost", 7777);
            var message = client.Receive();

            Console.WriteLine(message);

            Console.ReadLine();
        }
    }
}
