using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    class Program
    {


        static void Main(string[] args)
        {
            try
            {
                Server server = new Server();
                server.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("ServerException: {0}", e);
            }
        }
    }
}
