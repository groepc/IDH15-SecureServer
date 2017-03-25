using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.request
{
    class BadRequestException
    {
        private String message;

        BadRequestException(String message)
        {
            this.message = message;
        }

        public String getMessage()
        {
            return message;
        }

    }
}
