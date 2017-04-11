using Server.request;
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
        private TcpListener listenSocket;

        //comment
        public Server()
        {
            Int32 port = Convert.ToInt32(ConfigurationManager.AppSettings.Get("webport"));
            IPAddress localAddr = IPAddress.Parse(ConfigurationManager.AppSettings.Get("ipadress"));
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

                    // When a new client is connected, handle the request on a new thread
                    Thread requestThread = new Thread(OnRequest)
                    {
                        Name = Guid.NewGuid().ToString()
                    };

                    requestThread.Start(communicationSocket);


                    // En ga onmiddleijk klaar staan voor het volgende request
                    // communicationSocket.Close();
                }
            }
            catch (IOException e)
            {
                // TO DO: iets moois
                Console.WriteLine(e.ToString());
            }
        }

        private void OnRequest(object clientSocketObj)
        {
            try
            {
                using (TcpClient clientSocket = (TcpClient) clientSocketObj)
                {

                    Console.WriteLine("Start RequestHandler");

                    // Start een handler met deze socket
                    RequestHandler handler = new RequestHandler(clientSocket);
                    handler.Run();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
