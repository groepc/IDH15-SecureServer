using System;

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
