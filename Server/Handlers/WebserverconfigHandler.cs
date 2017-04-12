using Server.request;
using System;
using Server.response.admin;
using System.Configuration;
using Newtonsoft.Json;
using Server.Entities;
using System.Collections.Generic;
using Server.utils;

namespace Server.Handlers
{
    public class WebserverconfigHandler : IPageHandler
    {
        public string HandleGet(Request request, string requestedFile)
        {
            return (new WebserverConfigPage()).getHtmlPage();
        }

        public void HandlePost(Request request, string requestedFile)
        {
            AppConfigProcessor.Get().WebPort = int.Parse(request.formData["webport"]);
            AppConfigProcessor.Get().WebRoot = request.formData["webroot"];
            AppConfigProcessor.Get().DefaultPages = request.formData["defaultpage"];
            AppConfigProcessor.Get().DirectoryBrowsing = Convert.ToBoolean(request.formData["directoryBrowsing"]);
            AppConfigProcessor.Save();
        
            string path = "http://" + ConfigurationManager.AppSettings.Get("ipadress") + ":"
                   + ConfigurationManager.AppSettings.Get("webport") + "/admin/settings.html";
            throw new RedirectException(path);
        }
    }
}