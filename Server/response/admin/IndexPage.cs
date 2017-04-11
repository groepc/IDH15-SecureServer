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
			content += "<form method=\"post\" action=\"/admin/index.html\">\n        <H3>Superserver!</H3>\n        <p>\n            <table>\n                <tr>\n                    <td>Username:</td>\n                    <td><input name=\"Username\" type=\"text\" /></td>\n                </tr>\n                <tr>\n                    <td>Password:</td>\n                    <td><input name=\"Password\" type=\"password\" /></td>\n                </tr>\n            </table>\n            </p>\n            <p>\n                <INPUT TYPE=\"submit\" VALUE=\"Login\" name=\"Login\">\n            </p>\n        </form>";
		}	

	}
}