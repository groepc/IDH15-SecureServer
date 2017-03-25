using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.response
{
    class ResponseCodes
    {
        private static ResponseCodes instance; // Singleton
        private Dictionary<int, string> messages = new Dictionary<int, string>();

        public static string getMessage(int code)
        {
            if (instance == null)
            {
                instance = new ResponseCodes();
            }
            return instance._getMessage(code);
        }

        private string _getMessage(int code)
        {
            if (messages.ContainsKey(code))
            {
                return messages[code];
            }
            else
            {
                return "?";
            }
        }

        private ResponseCodes()
        {
            // Informational 1xx
            messages.Add(100, "Continue");
            messages.Add(101, "Switching Protocols");
            // Successful 2xx
            messages.Add(200, "OK");
            messages.Add(201, "Created");
            messages.Add(202, "Accepted");
            messages.Add(203, "Non-Authoritative Information");
            messages.Add(204, "No Content");
            messages.Add(205, "Reset Content");
            messages.Add(206, "Partial Content");
            // Redirection 3xx
            messages.Add(300, "Multiple Choices");
            messages.Add(301, "Moved Permanently");
            messages.Add(302, "Found");
            messages.Add(303, "See Other");
            messages.Add(304, "Not Modified");
            messages.Add(305, "Use Proxy");
            messages.Add(306, "(Unused)");
            messages.Add(307, "Temporary Redirect");
            // Client Error 4xx
            messages.Add(400, "Bad Request");
            messages.Add(401, "Unauthorized");
            messages.Add(402, "Payment Required");
            messages.Add(403, "Forbidden");
            messages.Add(404, "Not Found");
            messages.Add(405, "Method Not Allowed");
            messages.Add(406, "Not Acceptable");
            messages.Add(407, "Proxy Authentication Required");
            messages.Add(408, "Request Timeout");
            messages.Add(409, "Conflict");
            messages.Add(410, "Gone");
            messages.Add(411, "Length Required");
            messages.Add(412, "Precondition Failed");
            messages.Add(413, "Request Entity Too Large");
            messages.Add(414, "Request-URI Too Long");
            messages.Add(415, "Unsupported Media Type");
            messages.Add(416, "Requested Range Not Satisfiable");
            messages.Add(417, "Expectation Failed");
            // Server Error 5xx
            messages.Add(500, "Internal Server Error");
            messages.Add(501, "Not Implemented");
            messages.Add(502, "Bad Gateway");
            messages.Add(503, "Service Unavailable");
            messages.Add(504, "Gateway Timeout");
            messages.Add(505, "HTTP Version Not Supported");
        }

    }
}
