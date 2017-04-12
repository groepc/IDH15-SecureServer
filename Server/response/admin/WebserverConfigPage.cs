using Server.utils;

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
            content += "<form METHOD=\"POST\" ACTION=\"/admin/settings.html\">\n";
            content += "    <H3>Superserver!</H3>\n";
            content += "    <p>\n";
            content += "        <table>\n";
            content += "            <tr>\n";
            content += "                <td>Webport:</td>\n";
            content += "                <td><INPUT TYPE=\"TEXT\" NAME=\"webport\" VALUE=\"" + AppConfigProcessor.Get().WebPort + "\" SIZE=\"25\" MAXLENGTH=\"150\"></td>\n";
            content += "            </tr>\n";
            content += "            <tr>\n";
            content += "                <td>Webroot:</td>\n";
            content += "                <td><INPUT TYPE=\"TEXT\" NAME=\"webroot\" VALUE=\"" + AppConfigProcessor.Get().WebRoot + "\" SIZE=\"25\" MAXLENGTH=\"150\"></td>\n";
            content += "            </tr>\n";
            content += "            <tr>\n";
            content += "                <td>Default page:</td>\n";
            content += "                <td><INPUT TYPE=\"TEXT\" NAME=\"defaultpage\" VALUE=\"" + AppConfigProcessor.Get().DefaultPages + "\" SIZE=\"25\" MAXLENGTH=\"150\"></td>\n";
            content += "            </tr>\n";
            content += "            <tr>\n";
            content += "                <td>Directory browsing:</td>\n";
            if (AppConfigProcessor.Get().DirectoryBrowsing)
            {
                content += "                <td><input type='radio' name='directoryBrowsing' value='true' checked> Ja <br></td>";
                content += "                <td><input type='radio' name='directoryBrowsing' value='false' > Nee <br></td>";
            }
            else
            {
                content += "                <td><input type='radio' name='directoryBrowsing' value='true' > Ja <br></td>";
                content += "                <td><input type='radio' name='directoryBrowsing' value='false' checked> Nee <br></td>";
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