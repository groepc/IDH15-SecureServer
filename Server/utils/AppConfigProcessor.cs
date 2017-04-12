using Newtonsoft.Json;
using Server.Entities;
using System.Configuration;
using System.IO;

namespace Server.utils
{
    public class AppConfigProcessor
    {
        private static AppConfig config = null;

        public static void Save()
        {
            string json = JsonConvert.SerializeObject(config);

            //write string to file
            File.WriteAllText(ConfigurationManager.AppSettings.Get("json"), json);
        }

        public static AppConfig Get() // singleton
        {
            if (config == null) {
                config = Load();
            }
            return config;
        }

        protected static AppConfig Load()
        {
            StreamReader r = new StreamReader(ConfigurationManager.AppSettings.Get("json"));
            string json = r.ReadToEnd();
            r.Close();
            return JsonConvert.DeserializeObject<AppConfig>(json);
        }

    }
}
