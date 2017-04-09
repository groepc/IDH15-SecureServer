using Server.request;
namespace Server.response
{
	class Error : HtmlPage
	{
		readonly int status;
		public Error(int status)
		{
			this.status = status;

			createErrorMessage();
		}

		protected void createErrorMessage()
		{
			content = status + " " + ResponseCodes.getMessage(status) + ":\r\n";

		}

	    public string getHtmlPath(string docroot)
	    {
	        return  docroot + "/error/" + this.status + ".html";
	    }
	}
}