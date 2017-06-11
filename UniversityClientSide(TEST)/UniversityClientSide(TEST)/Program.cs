using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;

namespace UniversityClientSide_TEST_
{
    class Program
    {
        private static Socket _clientSocket = null;

        static void Main(string[] args)
        {
            Console.Title = "CLIENT";
            LoopConnect();
            Console.ReadLine();
        }        

        private static void LoopConnect()   //server connection loop
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = ConfigurationManager.ConnectionStrings["GetIP"].ConnectionString;   //takes the server IP from App.config
            int connAttempt = 0;
            while (!_clientSocket.Connected)
            {
                try
                {
                    if (connAttempt == 10)
                    {
                        Console.WriteLine("Connection Timeout");
                        Console.WriteLine("Would you like to re-connect?");
                        Console.WriteLine("Y/N");
                        ConsoleKeyInfo key = Console.ReadKey();
                        while (key.KeyChar != 'y' || key.KeyChar != 'n')
                        {
                            Console.Write("\b \b");

                            Console.Write("\b \b");
                            if (key.KeyChar.Equals('y'))
                            {
                                LoopConnect();
                            }
                            else if (key.KeyChar.Equals('n'))
                            {
                                Console.WriteLine(":(");
                                Environment.Exit(1);
                            }
                            key = Console.ReadKey();
                        }
                    }
                    else
                    {
                        connAttempt++;
                        _clientSocket.Connect(IPAddress.Parse(IP), 6784);
                    }
                }
                catch (SocketException)
                {
                    Console.Clear();
                    Console.WriteLine("Attempt: " + connAttempt);
                }
            }
            Console.Clear();
            Console.WriteLine(string.Format("Connected! ({0} Attempts)", connAttempt));
            SendLoop();
        }

        private static void SendLoop()  //server request send loop
        {
            while (true)
            {
                try
                {
                    Console.Write("Request: ");
                    string request = Console.ReadLine();
                    byte[] buffer = Encoding.ASCII.GetBytes(request);
                    _clientSocket.Send(buffer);
                    byte[] recieveBuffer = new byte[1024];
                    int recieveData = _clientSocket.Receive(recieveBuffer);
                    byte[] data = new byte[recieveData];
                    Array.Copy(recieveBuffer, data, recieveData);
                    Console.WriteLine("Response: " + Encoding.ASCII.GetString(data));
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Would you like to re-connect?");
                    Console.WriteLine("Y/N");
                    ConsoleKeyInfo key = Console.ReadKey();
                    while (key.KeyChar != 'y' || key.KeyChar != 'n')
                    {
                        Console.Write("\b \b");

                        Console.Write("\b \b");
                        if (key.KeyChar.Equals('y'))
                        {
                            LoopConnect();
                        }
                        else if (key.KeyChar.Equals('n'))
                        {

                        }
                        key = Console.ReadKey();
                    }
                }
            }
        }
    }
}
