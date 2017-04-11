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
			content += " <form METHOD=\"POST\" ACTION=\"/admin/settings.html\">\n        <H3>Superserver!</H3>\n        <p>\n            <table>\n                <tr>\n                    <td>Webport:</td>\n                    <td><INPUT TYPE=\"TEXT\" NAME=\"webport\" VALUE=\"\" SIZE=\"20\" MAXLENGTH=\"150\"></td>\n                </tr>\n                <tr>\n                    <td>Webroot:</td>\n                    <td><INPUT TYPE=\"TEXT\" NAME=\"webroot\" VALUE=\"\" SIZE=\"25\" MAXLENGTH=\"150\"></td>\n                </tr>\n                <tr>\n                    <td>Default page:</td>\n                    <td><INPUT TYPE=\"TEXT\" NAME=\"defaultpage\" VALUE=\"\" SIZE=\"25\" MAXLENGTH=\"150\"></td>\n                </tr>\n            </table>\n        </p>\n        <p>\n            Directory browsing:<BR>\n            <INPUT TYPE=\"CHECKBOX\" NAME=\"info\" VALUE=\"dbon\"><BR>\n        </p>\n        <p>\n            <input type=\"submit\" VALUE=\"Show log\">\n            <INPUT TYPE=\"submit\" VALUE=\"Save\">\n            <INPUT TYPE=\"submit\" VALUE=\"Cancel\">\n        </p>\n    </form>";
		}

	}
}