using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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

        public void run()
        {
            try
            {
                socket_in = socket.; //socket.getInputStream();
                socket_out = socket.getOutputStream();
                request = new Request(socket_in);

                MyFile myfile = new MyFile(webroot + request.getPath());


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
