using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
               
                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[1024 * 1024];
                String data = null;
                //Checksums.ReadChecksums(path);
                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;
                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;
                    string response = null;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                    }

                    // Process the data sent by the client.
                    Console.WriteLine("Sent: {0}", response);
                    byte[] msg = System.Text.Encoding.Unicode.GetBytes(response);

                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
                }

            }
            catch (SocketException e)
            {
            }
            catch (IOException ex)
            {
            }
            finally
            {
            }

        }
    }
}
