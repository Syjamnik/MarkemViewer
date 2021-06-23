using MarkemViewer_Library;
using System;

namespace MarkemViewer_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // declaration 
            string ipAddress; ;
            int port;
            string command; ;

            while (true)
            {
                try
                {
                    // initialization
                    Console.Write("Enter IP Address: ");
                    ipAddress = Console.ReadLine();

                    Console.Write("Enter port number (21000) : ");
                    port = int.Parse(Console.ReadLine());

                    Console.Write("Enter command: ");
                    command = Console.ReadLine();

                    // action
                    var response = TcpIpConnector.Instance.startCommunication(ipAddress, port, command);
                    Console.WriteLine(response+ "\n");
                }
                catch (Exception e)
                {
                    // exception handling 
                    Console.WriteLine("\n something went wrong. Here is brief description: \n");

                    ConsoleColor normalColor = Console.BackgroundColor;
                    Console.BackgroundColor = ConsoleColor.DarkRed;

                    Console.WriteLine(e);

                    Console.BackgroundColor = normalColor;
                    Console.ReadLine();
                }

                
            }
        }
   
    }
}
