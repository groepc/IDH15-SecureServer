using Server.request;
using Server.utils;
using System;
using System.Configuration;
using System.Net;

namespace Server.Handlers
{
    public class LoginHandler : IPageHandler
    {
        public MyFile HandleGet(Request request, string requestedFile)
        {            
            MyFile myfile = new MyFile(requestedFile);
            return myfile;
        }

        public void HandlePost(Request request, string requestedFile)
        {

            string path = "http://" + ConfigurationManager.AppSettings.Get("ipadress") + ":" 
                + ConfigurationManager.AppSettings.Get("webport") + "/admin/settings.html";

            throw new RedirectException(path);
           
        }
    }
}
