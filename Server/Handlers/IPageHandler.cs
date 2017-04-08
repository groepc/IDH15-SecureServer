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
        MyFile HandlePost(Request request, string requestedFile);
    }
}
