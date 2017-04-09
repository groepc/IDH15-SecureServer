using Server.Handlers;
using Server.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.request
{
    public class PageHandlerFactory
    {
        private readonly UserHelper _userHelper;
        private readonly Authentication _authentication;

        public PageHandlerFactory(UserHelper userhelper, Authentication authentication)
        {
            _userHelper = userhelper;
            _authentication = authentication;
        }

        public IPageHandler Create(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            switch (path.ToLowerInvariant())
            {
                case "/admin/index.html":
                    return new LoginHandler(_authentication);
                case "/admin/settings.html":
                    return new WebserverconfigHandler();
            }

            return null;
        }
    }
}
