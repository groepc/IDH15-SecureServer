using Server.request;
using Server.Entities;
using System.Configuration;
using Server.response.admin;
using Server.utils;

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
                    + AppConfigProcessor.Get().WebPort + "/admin/settings.html";

                throw new RedirectException(path);
            }
            else
            {
                string path = "http://" + ConfigurationManager.AppSettings.Get("ipadress") + ":"
                    + AppConfigProcessor.Get().WebPort + "/admin/index.html";

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
