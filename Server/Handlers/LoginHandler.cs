using Server.request;
using Server.utils;
using System;
using System.Configuration;
using System.Net;

namespace Server.Handlers
{
    public class LoginHandler : IPageHandler
    {
        public MyFile HandleGet(Request request, string requestedFile)
        {
               
            MyFile myfile = new MyFile(requestedFile);
            return myfile;
        }

        public MyFile HandlePost(Request request, string requestedFile)
        {
            string username = request.formdata;
            //string password = request.FormData["Password"];

            MyFile myfile = new MyFile(ConfigurationManager.AppSettings.Get("webroot") + "/settings.html");
            Authentication.AuthenticateUser("Mieke", "123", myfile);

            //if ()
            //{

            //}
            //else
            // {
            //    throw new Exception();
            //}
           
            return myfile;
        }
    }
}
