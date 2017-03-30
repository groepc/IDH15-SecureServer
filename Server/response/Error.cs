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
	}
}