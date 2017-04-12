using Server.request;
using System;
using Server.response.admin;
using System.Configuration;

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
            string webport = request.formData["webport"];
            string webroot = request.formData["webroot"];
            string defaultpage = request.formData["defaultpage"];
            //string directoryBrowsing = request.formData["directoryBrowsing"];

            ReadAllSettings();
            AddUpdateAppSettings("webport", webport);
            AddUpdateAppSettings("webroot", webroot);
            AddUpdateAppSettings("defaultPage", defaultpage);
            //AddUpdateAppSettings("dbon", directoryBrowsing);

            ReadAllSettings();
            string path = "http://" + ConfigurationManager.AppSettings.Get("ipadress") + ":"
                   + ConfigurationManager.AppSettings.Get("webport") + "/admin/settings.html";
            throw new RedirectException(path);
        }

        static void ReadAllSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}