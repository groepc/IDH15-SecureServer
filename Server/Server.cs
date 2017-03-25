using Server.request;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public class Server
    {
        private TcpListener listenSocket;

        //comment
        public Server()
        {
            Int32 port = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            listenSocket = new TcpListener(localAddr, port);
            listenSocket.Start();
        }

        public void Start()
        {
            try
            {
                while (true)
                {
                    // De blocking call is de accept-methode: Een request
                    // vanuit een browser resulteert hier in een communicatiesocket
                    TcpClient communicationSocket = listenSocket.AcceptTcpClient();
                    // Start een handler met deze socket
                    RequestHandler handler = new RequestHandler(communicationSocket);
                    handler.Run();
                    // En ga onmiddleijk klaar staan voor het volgende request
                }
            }
            catch (IOException e)
            {
                // TO DO: iets moois
            }
        }
    }
}
