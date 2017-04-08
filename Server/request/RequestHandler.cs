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

                // Map and parse the relative path to get the actual file info
                string requestedFile = ConfigurationManager.AppSettings.Get("webroot") + actualPath;
    
                if (pageHandler != null)
                {
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


    //            if (request.getPath().Contains("webserverconfig")) {

				//	myfile = new MyFile(ConfigurationManager.AppSettings.Get("configpath")+ request.getPath().Replace("webserverconfig", ""));
				//} 
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
				writeString((new Error(400)).getHtmlPage());
			}
			catch (FileNotFoundException e)
			{
				writeString((new Error(404)).getHtmlPage());
			}
			catch (IOException e)
			{
				writeString((new Error(500)).getHtmlPage());
			}
			
			myfile.Close();
		}

		private void writeString(String content)
		{
			byte[] buffer = Encoding.ASCII.GetBytes(content);
			writeHeader(200, "text/html", buffer.Length);
			write(buffer);
		}

		private void writeFile(MyFile myfile)
		{
			writeHeader(200, myfile.GetContentType(), myfile.GetLength());
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
