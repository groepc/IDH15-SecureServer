using Server.request;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Server
{


    public class Server
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
                RequestHandler handler = new RequestHandler(server);
                // Start listening for client requests.
                server.Start();

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");



                    Console.WriteLine("Connected!");

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
