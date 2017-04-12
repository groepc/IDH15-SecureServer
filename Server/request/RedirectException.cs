using System;

namespace Server.request
{
    class RedirectException : Exception
    {
        private string path;

        public RedirectException(string path)
        {
            this.path = path;
        }

        public string getPath()
        {
            return path;
        }
    }
}
