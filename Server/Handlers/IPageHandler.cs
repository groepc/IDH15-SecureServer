using Server.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.request
{
    public interface IPageHandler
    {
        MyFile HandleGet(Request request, string requestedFile);
        void HandlePost(Request request, string requestedFile);
    }
}
