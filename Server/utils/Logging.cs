using System;
using System.IO;
using System.Net;
using System.Reflection;

namespace Server.utils
{

    public class Logging
    {
        public void LogStart(string remoteIpEndPoint)
        {
            string logsDirectory = @"..\..\setup-log\log.txt";

            // This text is added only once to the file.
            if (!File.Exists(logsDirectory))
            {
                StreamWriter sw = File.CreateText(logsDirectory);
            }

            using (StreamWriter sw = File.AppendText(logsDirectory))
            {
                string datumTijd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string clientIp = remoteIpEndPoint;
                sw.WriteLine(@"/################# Start Logging ####################\");
                sw.WriteLine(@"Date and Time: "+ datumTijd);
                sw.WriteLine(@"Client IP-Adress: " + clientIp);
            }
        }

        public void LogWrite(string logMessage)
        {
            string logsDirectory = @"..\..\setup-log\log.txt";
            using (StreamWriter sw = File.AppendText(logsDirectory))
            {
                sw.WriteLine(logMessage);
            }
        }
        public void LogRead()
        {
            string logsDirectory = @"..\..\setup-log\log.txt";
            using (StreamReader sr = File.OpenText(logsDirectory))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    // Console.WriteLine(s);
                }
            }
        }
        public void LogEnd()
        {
            string logsDirectory = @"..\..\setup-log\log.txt";
            using (StreamWriter sw = File.AppendText(logsDirectory))
            {
                sw.WriteLine(@"/################# End Logging ####################\");
            }
        }
    }
}