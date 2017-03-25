using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        

        static void Main(string[] args)
        {
            try
            {
                Server server = new Server(IPAddress, port);
                server.start();
            }
            catch (Exception e)
            {
                System.err.println(e.getMessage());
            }
        }
}
