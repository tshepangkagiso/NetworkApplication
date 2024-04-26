using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace ClientSide1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set the IP address and port of the server
            IPAddress serverIP = IPAddress.Parse("127.0.0.1"); // Localhost
            int serverPort = 8080;

            // Create a TCP client
            TcpClient client = new TcpClient();

            // Connect to the server
            client.Connect(serverIP, serverPort);
            Console.WriteLine("Connected to the server!");
            Thread.Sleep(1000);

                                        // Handle server communication here
            // Client code
            try
            {
                NetworkStream stream = client.GetStream();

                // Send data to the server
                Console.Write("Send message to server: ");
                string sendData = Console.ReadLine();
                SendMessageToServer(sendData, stream);

                // Receive data from the server
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("\nReceived data from server: " + receivedData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally 
            {
                // Close the connection
                client.Close(); 
            }
        }

        public static void SendMessageToServer(string sendData, NetworkStream stream)
        {
            byte[] dataBytes = Encoding.ASCII.GetBytes(sendData);
            stream.Write(dataBytes, 0, dataBytes.Length);
            Console.WriteLine("\nSent data to server.\n");
        }

    }
}
