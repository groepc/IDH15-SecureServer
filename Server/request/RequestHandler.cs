using Server.response;
using Server.utils;
using Server.request;
using System;
using System.IO;
using System.Net.Sockets;
using System.Configuration;
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
        private IPEndPoint remoteIpEndPoint;

		public RequestHandler(TcpClient socket)
		{
			this.socket = socket;
		}

		public void Run()
        {
			MyFile myfile = null;
			try
            {
                socket_in = socket.GetStream();
				socket_out = socket.GetStream();
                remoteIpEndPoint = socket.Client.RemoteEndPoint as IPEndPoint;

                request = new Request(socket_in, remoteIpEndPoint.ToString());

                string actualPath = request.path;
                IPageHandler pageHandler = PageHandlerFactory.Create(actualPath);
    
                if (pageHandler != null)
                {
                    // Map and parse the relative path to get the actual file info
                    string requestedFile = ConfigurationManager.AppSettings.Get("configpath") + actualPath;

                    switch (request.command)
                    {
                        case "GET":
                            myfile = pageHandler.HandleGet(request, requestedFile);
                            break;
                        case "POST":
                            myfile = pageHandler.HandlePost(request, requestedFile);
                            break;
                    }
                }
                else
                {
					myfile = new MyFile(ConfigurationManager.AppSettings.Get("webroot") + request.getPath());
				}

				System.Console.WriteLine(myfile.indexPageExists());

				if (ConfigurationManager.AppSettings.Get("dbon") == "true" && myfile.indexPageExists() == true)
				{
					HtmlPage directoryList = new DirectoryList(ConfigurationManager.AppSettings.Get("webroot"), request.getPath().Substring(0, request.getPath().LastIndexOf('/')));
					writeString(directoryList.getHtmlPage());
				}
				else
				{
					writeFile(myfile);
                }
			}
			catch (BadRequestException e)
			{
				writeString((new Error(400)).getHtmlPage(), 400);
			}
			catch (FileNotFoundException e)
			{
				writeString((new Error(404)).getHtmlPage(), 404);
			}
			catch (IOException e)
			{
				writeString((new Error(500)).getHtmlPage(), 500);
			}
			
			myfile.Close();
		}

		private void writeString(String content, int statusCode = 200)
		{
			byte[] buffer = Encoding.ASCII.GetBytes(content);
			writeHeader(statusCode, "text/html", buffer.Length);
			write(buffer);
		}

		private void writeFile(MyFile myfile, int statusCode = 200)
		{
			writeHeader(statusCode, myfile.GetContentType(), myfile.GetLength());
			byte[] buffer = new byte[1024];
			while (myfile.Read(buffer, buffer.Length) > 0)
			{
				write(buffer);
			}
			myfile.Close();
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
