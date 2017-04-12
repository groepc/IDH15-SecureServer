using System;

namespace Server
{
    public class Program
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
