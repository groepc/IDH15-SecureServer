namespace Server
{
    public abstract class HtmlPage
    {
        protected string content;

        public string getHtmlPage(string pageName = "")
        {
            string html = "<!doctype html>";
            html += "<html lang=\"en\">";
            html += "   <head>";
            html += "       <meta charset=\"utf-8\">";
            html += "       <title>" + pageName + "</title>";
            html += "       <style type=\"text/css\">";
            html += "           body{font-family: Arial;font-size: 10pt;}";
            html += "           input[type=text],input[type=password]{width: 200px;}";
            html += "           table{border:1px solid #ccc;}";
            html += "           table th{background-color: #F7F7F7;color: #333;font-weight: bold;}";
            html += "           table th, table td {padding: 5px;border-color: #ccc;}";
            html += "       </style>";
            html += "   </head>";
            html += "   <body>";
            html += content;
            html += "   </body>";
            html += "</html>";
            return html;
        }
    }
}
