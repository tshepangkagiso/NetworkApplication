using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerSide
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set the IP address and port for the server
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); // Localhost
            int port = 8080;

            // Create a TCP listener
            TcpListener listener = new TcpListener(ipAddress, port);

            // Start listening for incoming connections
            listener.Start();
            Console.WriteLine("Server started. Waiting for connections...");
            Thread.Sleep(1000);

            // Accept incoming client connections
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Client connected!");
            Thread.Sleep(1000);

                                            // Handle client communication here
           // Server code
            try
            {
                NetworkStream stream = client.GetStream();

                // Receive data from the client
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received data from client: " + receivedData);

                // Send data to the client
                Console.Write("Enter a message: ");
                string sendData = Console.ReadLine();
                SendMessageToClient(sendData, stream);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                // Close the connection and stop listening
                client.Close();
                listener.Stop();
            }
        }

        public static void SendMessageToClient(string sendData, NetworkStream stream)
        {
            byte[] dataBytes = Encoding.ASCII.GetBytes(sendData);
            stream.Write(dataBytes, 0, dataBytes.Length);
            Console.WriteLine("\nSent data to Client.\n");
        }
    }
}
