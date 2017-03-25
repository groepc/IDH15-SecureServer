using Server.utils;
using System;
using System.IO;
using System.Net.Sockets;
using static System.Net.WebRequestMethods;

namespace Server.request
{
    public class RequestHandler
    {
        private TcpListener socket;
        private StreamWriter socket_in;
        private StreamReader socket_out;

        private Stream stream;

        private String webroot = "C:/webroot";

        public RequestHandler(TcpListener socket)
        {
            this.socket = socket;
        }

        public void Run()
        {
            try
            {
                TcpClient client = socket.AcceptTcpClient();

                NetworkStream socket_in = client.GetStream();
                NetworkStream socket_out = client.GetStream();
                var request = new Request(socket_in);

                Myfile myfile = new File(webroot + request.getPath());


                writeFile(myfile);
                socket.close();
            }
            catch (BadRequestException e)
            {
                writeError(400);
            }
            catch (FileNotFoundException e)
            {
                writeError(404);
            }
            catch (IOException e)
            {
                writeError(500);
            }
        }

    }
}
