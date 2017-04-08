using Server.request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Server.utils;
using System.Configuration;

namespace Server.Handlers
{
    public class LoginHandler : IPageHandler
    {
        public MyFile HandleGet(Request request, string requestedFile)
        {
               
            MyFile myfile = new MyFile(requestedFile);
            return myfile;
        }

        public MyFile HandlePost(Request request, string requestedFile)
        {
            throw new NotImplementedException();
        }
    }
}
