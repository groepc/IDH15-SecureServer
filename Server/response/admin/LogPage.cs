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
			content += "    <a href=\"settings.html\">Terug naar instellingen</a>";
            content += "    <form method='post'>";
            content += "        <button type=\"submit\" value=\"Login\">Reset log</button>";
            content += "    </form>";
            content += "    <div>";
			content += getLogText();
			content += "    </div>";
            content += "</div>";
        }

		protected string getLogText() {
			return encodeText(File.ReadAllText(ConfigurationManager.AppSettings.Get("log")));
		}
	}
}