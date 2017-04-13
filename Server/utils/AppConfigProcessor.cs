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
			Directory.CreateDirectory(Path.GetDirectoryName(ConfigurationManager.AppSettings.Get("json")));
			File.WriteAllText(ConfigurationManager.AppSettings.Get("json"), json);
		}

		public static AppConfig Get() // singleton
		{
			if (config == null)
			{
				config = Load();
			}
			return config;
		}

		protected static AppConfig Load()
		{
			if (File.Exists(ConfigurationManager.AppSettings.Get("json")))
			{
				StreamReader r = new StreamReader(ConfigurationManager.AppSettings.Get("json"));
				string json = r.ReadToEnd();
				r.Close();
				return JsonConvert.DeserializeObject<AppConfig>(json);
			}

			//If json file is not found, use default config
			AppConfig defaultconfig = new AppConfig();

			defaultconfig.DefaultPages = "index.html";
			defaultconfig.WebPort = 13000;
			defaultconfig.DirectoryBrowsing = false;
			return defaultconfig;
		}

	}
}
