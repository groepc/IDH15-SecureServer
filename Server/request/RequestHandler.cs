using Server.response;
using Server.utils;
using Server.request;
using System;
using System.IO;
using System.Net.Sockets;

using static System.Net.WebRequestMethods;
using System.Text;
using System.Net;

namespace Server.request
{
    public class RequestHandler : TcpClient
    {
        private TcpClient socket;
        private NetworkStream socket_in;
        private NetworkStream socket_out;
        private Request request;
        private string webroot = "C:/webroot";

        public RequestHandler(TcpClient socket)
        {
            this.socket = socket;
        }

        public void Run()
        {
            try
            {
                socket_in = socket.GetStream();
                socket_out = socket.GetStream();
                request = new Request(socket_in);

                MyFile myfile = new MyFile(webroot + request.getPath());

                writeFile(myfile);
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
            Logging logging = new Logging();
           

            IPEndPoint remoteIpEndPoint = socket.Client.RemoteEndPoint as IPEndPoint;
            logging.LogStart(remoteIpEndPoint.ToString());

            string datumTijd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            logging.LogStart(datumTijd);
           
            socket.Close();
        }

        private void writeError(int status)
        {

            string text = status + " " + ResponseCodes.getMessage(status) + ":\r\n" + request;

           //System.err.println(text);

            byte[] buffer = Encoding.ASCII.GetBytes(text);
            try
            {
                writeHeader(status, "text/plain", buffer.Length);
                write(buffer);
            }
            catch (IOException e)
            {
                // ignore error
            }

        }

        private void writeHeader(int status, String contentType, long contentLength)
        {
            string message = ResponseCodes.getMessage(status);
            write("HTTP/1.0 " + status + " " + message + "\r\n");
            write("Content-Type: " + contentType + "\r\n");
            write("Content-Length: " + contentLength + "\r\n");
            write("Connection: close " + contentLength + "\r\n");
            write("\r\n"); // altijd met een lege regel eindigen
        }

        private void writeFile(MyFile myfile)
        {
            writeHeader(200, myfile.GetContentType(), myfile.GetLength());
            byte[] buffer = new byte[1024];
            while (myfile.Read(buffer, buffer.Length) > 0)
            {
                write(buffer);
            }
        }

        private void write(string text)
        {
            byte[] myWriteBuffer = Encoding.ASCII.GetBytes(text);
            write(myWriteBuffer);
        }

        private void write(byte[] data)
        {
            socket_out.Write(data, 0, data.Length);
        }
    }
}
