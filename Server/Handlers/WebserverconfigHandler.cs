using Server.request;
using System;
using Server.response.admin;

namespace Server.Handlers
{
    public class WebserverconfigHandler : IPageHandler
    {
        public String HandleGet(Request request, string requestedFile)
        {
            return (new WebserverConfigPage()).getHtmlPage();
        }

        public void HandlePost(Request request, string requestedFile)
        {
            throw new NotImplementedException();
        }
    }
}
