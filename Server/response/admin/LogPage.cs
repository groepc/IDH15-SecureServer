using System.IO;
using System.Configuration;

namespace Server.response.admin
{
	public class LogPage : HtmlPage
	{
		public LogPage()
		{
			createForm();
		}

		protected void createForm()
		{
			content += "<div>";
			content += "    <h3>Log</h3>";
			content += "<a href=\"settings.html\">Terug naar instellingen</a>";
			content += "    <div>";
			content += getLogText();
			content += "    </div>";
			content += " <form>";
			content += "        <button type=\"submit\" value=\"Login\">Reset log<button>";
			content += "    </p>";
			content += "</form>";
		}

		protected string getLogText() {
			return encodeText(File.ReadAllText(ConfigurationManager.AppSettings.Get("log")));
		}
	}
}