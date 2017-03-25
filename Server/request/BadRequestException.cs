using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.request
{
    public class BadRequestException : Exception
    {
        private string message;

        public BadRequestException(string message)
        {
            this.message = message;
        }

        public string getMessage()
        {
            return message;
        }

    }
}
