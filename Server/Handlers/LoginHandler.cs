using Server.request;
using Server.utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;

namespace Server.Handlers
{
    public class LoginHandler : IPageHandler
    {
        private readonly Authentication _authentication;

        public LoginHandler(Authentication authentication)
        {
            _authentication = authentication;
        }

        public MyFile HandleGet(Request request, string requestedFile)
        {
            MyFile myfile = new MyFile(requestedFile);
            return myfile;
        }

        public void HandlePost(Request request, string requestedFile)
        {
            string[] parts = null;
            parts = SplitFormdata(request.formdata);

            string username = parts[0].Substring(parts[0].IndexOf('=') + 1); 
            string password = parts[1].Substring(parts[0].IndexOf('=') + 1);

            if (!_authentication.AuthenticateUser(username, password))
            {
                string path = "http://" + ConfigurationManager.AppSettings.Get("ipadress") + ":"
                    + ConfigurationManager.AppSettings.Get("webport") + "/admin/login.html";

                throw new RedirectException(path);
            }
            else
            {
                string path = "http://" + ConfigurationManager.AppSettings.Get("ipadress") + ":"
                    + ConfigurationManager.AppSettings.Get("webport") + "/admin/settings.html";

                throw new RedirectException(path);
            }
        }

        public string[] SplitFormdata(string formdata)
        {
            string[] parts = formdata.Split('&');
            return parts;
        }
    }
}
