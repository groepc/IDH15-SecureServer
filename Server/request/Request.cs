﻿using System.Net.Sockets;
using Server.Entities;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;

namespace Server.request
{
    public class Request
    {
        public string line, command, path, protocol;
        public NameValueCollection formData;
        public List<string> rawRequest = new List<string>();
        public RequestMessage requestMessage = null;
        
        public Request(NetworkStream socket_in, string remoteIpEndPoint)
        {

            Logging logging = new Logging();
            logging.LogStart(remoteIpEndPoint);

            requestMessage = RequestMessage.Create(socket_in);
			logging.LogWrite(requestMessage.RawHeader);

            this.command = this.requestMessage.HttpMethod;
            this.path = "/" + this.requestMessage.Path;
			if (!Path.HasExtension(this.path)) {
				this.path = this.path + "/";
			}

	    this.protocol = this.requestMessage.HttpVersion;

			if (!command.Equals("GET") && !command.Equals("POST"))
            {
                throw (new BadRequestException("Unknown request: " + command));
            }

            if (command.Equals("POST"))
            {
                this.formData = this.requestMessage.FormData;
            }

            logging.LogEnd();
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
