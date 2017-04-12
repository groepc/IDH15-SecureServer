using System;

namespace Server.request
{
    public interface IPageHandler
    {
        String HandleGet(Request request, string requestedFile);
        void HandlePost(Request request, string requestedFile);
    }
}
