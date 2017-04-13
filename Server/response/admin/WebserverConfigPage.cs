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
            content += "<form METHOD=\"POST\" ACTION=\"/admin/settings.html\">";
            content += "    <H3>Superserver!</H3>";
            content += "    <p>";
            content += "        <table>";
            content += "            <tr>";
            content += "                <td>Webport:</td>";
            content += "                <td><INPUT TYPE=\"TEXT\" NAME=\"webport\" VALUE=\"" + AppConfigProcessor.Get().WebPort + "\" SIZE=\"25\" MAXLENGTH=\"150\" disabled></td>";
            content += "            </tr>";
            content += "            <tr>";
            content += "                <td>Webroot:</td>";
            content += "                <td><INPUT TYPE=\"TEXT\" NAME=\"webroot\" VALUE=\"" + encodeText(AppConfigProcessor.Get().WebRoot) + "\" SIZE=\"25\" MAXLENGTH=\"150\"></td>";
            content += "            </tr>";
            content += "            <tr>";
            content += "                <td>Default page:</td>";
            content += "                <td><INPUT TYPE=\"TEXT\" NAME=\"defaultpage\" VALUE=\"" + encodeText(AppConfigProcessor.Get().DefaultPages) + "\" SIZE=\"25\" MAXLENGTH=\"150\"></td>";
            content += "            </tr>";
            content += "            <tr>";
            content += "                <td>Directory browsing:</td>";
            if (AppConfigProcessor.Get().DirectoryBrowsing)
            {
                content += "                <td><input type='radio' name='directoryBrowsing' value='true' checked> Ja ";
                content += "                <input type='radio' name='directoryBrowsing' value='false' > Nee <br></td>";
            }
            else
            {
                content += "                <td><input type='radio' name='directoryBrowsing' value='true' > Ja ";
                content += "                <input type='radio' name='directoryBrowsing' value='false' checked> Nee <br></td>";
            }
            content += "            </tr>";
            content += "        </table>";
            content += "    </p>";
            content += "    <p>";
            content += "        <a href='/admin/log.html'>Show log</a>";
            content += "        <INPUT TYPE=\"submit\" VALUE=\"Save\">";
            content += "        <INPUT TYPE=\"submit\" VALUE=\"Cancel\">";
            content += "    </p>";
            content += "</form>";
        }
    }
}