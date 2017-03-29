
namespace Server
{
	class HtmlPage
	{
		protected string content;


		public string getHtmlPage(string pageName = "")
		{
			string html = "<!doctype html><html lang=\"en\"><head><meta charset=\"utf-8\"><title>" + pageName +  "</title></head><body>";

			html += this.content;

			html += "</body></html>";
			return html;
		}
	}
}
