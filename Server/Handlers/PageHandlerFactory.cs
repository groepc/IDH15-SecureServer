using Server.Handlers;
using Server.Entities;

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
                case "/admin/log.html":
                    return new LogPageHandler();
            }

            return null;
        }
    }
}
