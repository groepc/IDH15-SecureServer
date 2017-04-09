using System.IO;
using System.Net.Sockets;
using System.Text;
using Server.utils;
using System.Collections.Specialized;
using System;
using System.Web;
using System.Linq;

namespace Server.request
{
    public class Request
    {
        public string line, command, path, protocol, formdata;
        
        public Request(NetworkStream socket_in, string remoteIpEndPoint)
        {
            // Strings lezen gaat het gemakkelijkst via een BufferedReader
            TextReader read = new StreamReader(socket_in, Encoding.UTF8);
            // Lees de eerste regel, bijvoorbeeld "GET index.php HTTP/1.1"
            line = read.ReadLine();

            Logging logging = new Logging();
            logging.LogStart(remoteIpEndPoint);
            logging.LogWrite(line.ToString());

            // Lees de rest van de request header
            while (read.Peek() > -1)
            {            
                    logging.LogWrite(read.ReadLine());
            }

            // Volgens protocol bestaat regel 1 uit drie delen, gescheiden door spaties.
            // (de browser moet spaties in het derde deel netjes door %20 vervangen)
            string[] parts = line.Split(' ');

            if (parts.Length != 3)
            {
                throw (new BadRequestException("Syntax error in request: " + line));
            }

            command = parts[0];
            path = parts[1];
            protocol = parts[2];

			if (!command.Equals("GET") && !command.Equals("POST"))
            {
                throw (new BadRequestException("Unknown request: " + command));
            }

            if (command.Equals("POST"))
            {
                string formdata = Logging.ReadFormdata();
            }

            logging.LogEnd();
            readGetVariables(line);
        }

		protected void readGetVariables(string line)
		{
			Console.Write(line);
        }

        //@Override
        public string toString()
        {
            return line;
        }

        public string getCommand()
        {
            return command;
        }

        public string getPath()
        {
            return path;
        }

        public string getProtocol()
        {
            return protocol;
        }

        public string getFormData()
        {
            return formdata;
        }
    }
}
