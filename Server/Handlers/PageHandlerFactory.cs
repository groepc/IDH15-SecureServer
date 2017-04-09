using Server.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.request
{
    public class PageHandlerFactory
    {

        public PageHandlerFactory()
        {

        }

        public static IPageHandler Create(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            switch (path.ToLowerInvariant())
            {
                case "/admin/index.html":
                    return new LoginHandler();
//                case "webserverconfig":
//                    return new WebserverconfigHandler();

            }

            return null;
        }
    }
}
