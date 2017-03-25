using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.request
{
    public class Request
    {
        private string line, command, path, protocol;

        public Request(NetworkStream socket_in)
        {
            // Strings lezen gaat het gemakkelijkst via een BufferedReader
            TextReader read = new StreamReader(socket_in, Encoding.UTF8);

            // Lees de eerste regel, bijvoorbeeld "GET index.php HTTP/1.1"
            line = read.ReadLine();

            // Lees de rest van de request header
            while (!read.ReadLine().Equals(""))
            {
                // Doe ik nog niks mee!
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
            if (!command.Equals("GET"))
            {
                throw (new BadRequestException("Unknown request: " + command));
            }
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
    }
}
