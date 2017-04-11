using Server.request;
using Server.utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using Server.response.admin;

namespace Server.Handlers
{
	public class LoginHandler : IPageHandler
    {
        private readonly Authentication _authentication;

        public LoginHandler(Authentication authentication)
        {
            _authentication = authentication;
        }

        public string HandleGet(Request request, string requestedFile)
        {
			return (new IndexPage()).getHtmlPage();
        }

        public void HandlePost(Request request, string requestedFile)
        {
            string username = request.formData["Username"];
            string password = request.formData["Password"];

            if (_authentication.AuthenticateUser(username, password))
            {
                string path = "http://" + ConfigurationManager.AppSettings.Get("ipadress") + ":"
                    + ConfigurationManager.AppSettings.Get("webport") + "/admin/settings.html";

                throw new RedirectException(path);
            }
            else
            {
                string path = "http://" + ConfigurationManager.AppSettings.Get("ipadress") + ":"
                    + ConfigurationManager.AppSettings.Get("webport") + "/admin/index.html";

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
