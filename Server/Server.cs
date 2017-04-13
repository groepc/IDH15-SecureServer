using Server.request;
using Server.utils;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public class Server
    {
        private Socket listenSocket;

        public Server()
        {
            Int32 port = AppConfigProcessor.Get().WebPort;
            IPAddress localAddr = IPAddress.Parse(ConfigurationManager.AppSettings.Get("ipadress"));
            IPEndPoint endPoint = new IPEndPoint(localAddr, port);
            listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(endPoint);
            Thread listenerThread = new Thread(Start);
            listenerThread.Start();
        }

        public void Start()
        {
            listenSocket.Listen(int.MaxValue);
            try
            {
                Socket communicationSocket;
                while (true)
                {
                    // De blocking call is de accept-methode: Een request
                    // vanuit een browser resulteert hier in een communicatiesocket
                    communicationSocket = listenSocket.Accept();
                    // When a new client is connected, handle the request on a new thread
                    Thread requestThread = new Thread(OnRequest)
                    {
                        Name = Guid.NewGuid().ToString()
                    };
                    requestThread.Start(communicationSocket);
                }
                communicationSocket.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void OnRequest(object clientSocketObj)
        {
            try
            {
                using (Socket clientSocket = (Socket)clientSocketObj)
                {
                    IPEndPoint endPoint = (IPEndPoint)clientSocket.RemoteEndPoint;

                    using (NetworkStream networkStream = new NetworkStream(clientSocket))
                    {
                        Console.WriteLine("Start RequestHandler");

                        // Start een handler met deze socket
                        RequestHandler handler = new RequestHandler(networkStream, endPoint);
                        handler.Run();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
