using System;
using System.Configuration;

namespace Server.response.admin
{
	public class WebserverConfigPage : HtmlPage
	{
		public WebserverConfigPage()
		{
			createForm();
		}

		protected void createForm()
		{
            string appWebRoot = ConfigurationManager.AppSettings.Get("webroot");
            string appWebPort = ConfigurationManager.AppSettings.Get("webport");
            string appDefaultPage = ConfigurationManager.AppSettings.Get("defaultPage");
            bool appDirectoryBrowsing = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("directoryBrowsing"));
            
            content += "<form METHOD=\"POST\" ACTION=\"/admin/settings.html\">\n";
            content += "    <H3>Superserver!</H3>\n";
            content += "    <p>\n";
            content += "        <table>\n";
            content += "            <tr>\n";
            content += "                <td>Webport:</td>\n";
            content += "                <td><INPUT TYPE=\"TEXT\" NAME=\"webport\" VALUE=\"" + appWebPort + "\" SIZE=\"25\" MAXLENGTH=\"150\"></td>\n";
            content += "            </tr>\n";
            content += "            <tr>\n";
            content += "                <td>Webroot:</td>\n";
            content += "                <td><INPUT TYPE=\"TEXT\" NAME=\"webroot\" VALUE=\"" + appWebRoot + "\" SIZE=\"25\" MAXLENGTH=\"150\"></td>\n";
            content += "            </tr>\n";
            content += "            <tr>\n";
            content += "                <td>Default page:</td>\n";
            content += "                <td><INPUT TYPE=\"TEXT\" NAME=\"defaultpage\" VALUE=\"" + appDefaultPage + "\" SIZE=\"25\" MAXLENGTH=\"150\"></td>\n";
            content += "            </tr>\n";
            content += "            <tr>\n";
            content += "                <td>Directory browsing:</td>\n";
            if (appDirectoryBrowsing) {
                content += "                <td><INPUT TYPE=\"CHECKBOX\" NAME=\"info\" VALUE=\"dbon\" checked></td>\n";
            } else {
                content += "                <td><INPUT TYPE=\"CHECKBOX\" NAME=\"info\" VALUE=\"dbon\"></td>\n";
            }
            content += "            </tr>\n";
            content += "        </table>\n";
            content += "    </p>\n";
            content += "    <p>\n";
            content += "        <input type=\"submit\" VALUE=\"Show log\">\n";
            content += "        <INPUT TYPE=\"submit\" VALUE=\"Save\">\n";
            content += "        <INPUT TYPE=\"submit\" VALUE=\"Cancel\">\n";
            content += "    </p>\n";
            content += "</form>";
		}

	}
}