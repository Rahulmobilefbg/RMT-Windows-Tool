
using System;
using System.IO.Ports;

namespace RMTNative
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RMT Windows Tool");
            Console.WriteLine("----------------");

            Console.WriteLine("Available Serial Ports:");

            foreach (string port in SerialPort.GetPortNames())
            {
                Console.WriteLine(port);
            }

            Console.WriteLine();
            Console.WriteLine("RMT Tool started.");
            Console.ReadKey();
        }
    }
}
