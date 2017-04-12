namespace Server.response
{
    public class DirectoryList : HtmlPage
    {
        string path;
        string webroot;
        public DirectoryList(string webroot, string path)
        {
            this.webroot = webroot;
            this.path = path;

            createList();
        }

        protected void createList()
        {
            content += "<ul>";
            content += createFileList();
            content += "</ul>";
        }

        protected string createFileList()
        {
            string li = "";
            string[] fileNames = FileProcessor.GetFileNamesDirectory(webroot + path);
            foreach (string fileName in fileNames)
            {
                string url = fileName.Replace(webroot, "");
                string fileNameView = fileName.Replace(webroot + path + "/", "");
                li += "<li><a href=\"" + url + "\">" + fileNameView + "</li>";
            }
            return li;
        }
    }
}