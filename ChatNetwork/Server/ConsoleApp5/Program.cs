using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net.Http;
using System.Net.Sockets;
using System.Net;
using System.Text;
 
namespace ConsoleApp5


public class Program
{
    private const int BufferSize = 1024;

    public static void Main()
    {
        int port;

        Console.WriteLine("Please enter the port number to listen on:");
        port = Int32.Parse(Console.ReadLine());

        try
        {
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            TcpListener tcpServer = new TcpListener(localAddr, port);
            tcpServer.Start();

            Console.WriteLine("Server started. Listening for incoming connections...");

            while (true)
            {
                TcpClient client = tcpServer.AcceptTcpClient();
                Console.WriteLine("Client connected.");

                using (NetworkStream networkStream = client.GetStream())
                using (FileStream fileStream = File.Create("received_file.dat"))
                {
                    byte[] buffer = new byte[BufferSize];
                    int bytesRead;

                    while ((bytesRead = networkStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                    }

                    Console.WriteLine("File received and saved as received_file.dat.");
                }

                client.Close();
                Console.WriteLine("Client disconnected.");
            }
        }
        catch (SocketException)
        {
            Console.WriteLine("An error occurred while listening for connections.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}   public class StateObject
    {
        // Client  socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }

