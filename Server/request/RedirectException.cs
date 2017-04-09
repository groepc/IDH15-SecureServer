using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
