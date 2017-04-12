namespace Server.response.admin
{
    public class IndexPage : HtmlPage
    {
        public IndexPage()
        {
            createForm();
        }

        protected void createForm()
        {
            content += "<form method=\"post\" action=\"/admin/index.html\">\n";
            content += "    <H3>Superserver!</H3>\n";
            content += "    <p>\n";
            content += "        <table>\n";
            content += "            <tr>\n ";
            content += "                <td>Username:</td>\n";
            content += "                <td><input name=\"Username\" type=\"text\" /></td>\n";
            content += "            </tr>\n";
            content += "            <tr>\n";
            content += "                <td>Password:</td>\n";
            content += "                <td><input name=\"Password\" type=\"password\" /></td>\n";
            content += "            </tr>\n";
            content += "        </table>\n";
            content += "    </p>\n";
            content += "    <p>\n";
            content += "        <INPUT TYPE=\"submit\" VALUE=\"Login\" name=\"Login\">\n";
            content += "    </p>\n";
            content += "</form>";
        }
    }
}