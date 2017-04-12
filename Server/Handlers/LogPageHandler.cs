using Server.request;
using Server.response.admin;
using Server.utils;
using System.Configuration;

namespace Server.Handlers
{
    public class LogPageHandler : IPageHandler
    {
        public string HandleGet(Request request, string requestedFile)
        {
            return (new LogPage()).getHtmlPage();
        }

        public void HandlePost(Request request, string requestedFile)
        {
            /*@ todo Logfile leeg maken */
            System.IO.File.WriteAllText(ConfigurationManager.AppSettings.Get("log"), string.Empty);

            string path = "http://" + ConfigurationManager.AppSettings.Get("ipadress") + ":"
                   + AppConfigProcessor.Get().WebPort + "/admin/settings.html";
            throw new RedirectException(path);
        }
    }
}