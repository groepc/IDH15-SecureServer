using Server.response;
using Server.Entities;
using System;
using System.IO;
using System.Net.Sockets;
using System.Configuration;
using System.Text;
using System.Net;
using Server.utils;

namespace Server.request
{
    public class RequestHandler
    {
        private NetworkStream socket;
        private NetworkStream socket_in;
        private NetworkStream socket_out;
        private Request request;
        private IPEndPoint remoteIpEndPoint;

        private readonly Authentication _authentication;
        private readonly PageHandlerFactory _pageHandlerFactory;

        public RequestHandler(NetworkStream socket, IPEndPoint endpoint)
		{
			this.socket = socket;
		    this.remoteIpEndPoint = endpoint;

            UserHelper userHelper = new UserHelper();
            _authentication = new Authentication(userHelper);
            _pageHandlerFactory = new PageHandlerFactory(userHelper, _authentication);
        }

		public void Run()
        {
			MyFile myfile = null;
			String HtmlPage = null;
            try
            {
                socket_in = socket;
                socket_out = socket;

                request = new Request(socket_in, remoteIpEndPoint.ToString());

                string actualPath = request.path;
                IPageHandler _pageHandler = _pageHandlerFactory.Create(actualPath);

                if (_pageHandler != null)
                {
                    // Map and parse the relative path to get the actual file info
                    string requestedFile = ConfigurationManager.AppSettings.Get("configpath") + actualPath;

                    switch (request.command)
                    {
                        case "GET":
							HtmlPage = _pageHandler.HandleGet(request, requestedFile);
                            break;
                        case "POST":
                            _pageHandler.HandlePost(request, requestedFile);
                            break;
                    }
                }
                else
                {
                    myfile = new MyFile(AppConfigProcessor.Get().WebRoot + request.getPath());
                }

                //System.Console.WriteLine(myfile.indexPageExists());

				if (AppConfigProcessor.Get().DirectoryBrowsing == true && (myfile != null && myfile.indexPageExists() == true))
                {
                    HtmlPage directoryList = new DirectoryList(AppConfigProcessor.Get().WebRoot,
                        request.getPath().Substring(0, request.getPath().LastIndexOf('/')));
                    writeString(directoryList.getHtmlPage());
                }
                else
                {
					if (HtmlPage is string) { 
						writeString(HtmlPage);
					}
					else
					{
						writeFile(myfile);
					}
                }
            }
            catch (RedirectException e)
            {
                Console.WriteLine(e);
                writeRedirectString(e.getPath(), 302);
            }
            catch (BadRequestException e)
            {
                Console.WriteLine(e);
                myfile = new MyFile((new Error(400)).getHtmlPath(ConfigurationManager.AppSettings.Get("configpath")));
                writeFile(myfile, 400);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
                myfile = new MyFile((new Error(404)).getHtmlPath(ConfigurationManager.AppSettings.Get("configpath")));
                writeFile(myfile, 404);
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
                myfile = new MyFile((new Error(500)).getHtmlPath(ConfigurationManager.AppSettings.Get("configpath")));
                writeFile(myfile, 500);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (myfile != null)
            {
                myfile.Close();
            }
		}

		private void writeString(String content, int statusCode = 200)
		{
			byte[] buffer = Encoding.ASCII.GetBytes(content);

            writeHeader(statusCode, "text/html", buffer.Length);     
         
			write(buffer);
		}

        private void writeRedirectString(string path, int statusCode = 302)
        {
            writeRedirectHeader(statusCode, path);
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
			write("HTTP/1.1 " + status + " " + message + "\r\n");
			write("Content-Type: " + contentType + "\r\n");
			write("Content-Length: " + contentLength + "\r\n");
			write("Connection: close\r\n");
			write("\r\n"); // altijd met een lege regel eindigen
		}

        private void writeRedirectHeader(int status, string path)
        {
            string message = ResponseCodes.getMessage(status);
            write("HTTP/1.1 " + status + " " + message + "\r\n");
            write("Location: " + path + "\r\n");
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
