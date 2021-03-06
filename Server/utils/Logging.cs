﻿using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Server.Entities
{
    public class Logging
    {
        public void LogStart(string remoteIpEndPoint)
        {
            string logsDirectory = ConfigurationManager.AppSettings.Get("log");

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
                sw.WriteLine(@"Date and Time: " + datumTijd);
                sw.WriteLine(@"Client IP-Adress: " + clientIp);
            }
        }

        public void LogWrite(string logMessage)
        {
            string logsDirectory = ConfigurationManager.AppSettings.Get("log");
            using (StreamWriter sw = File.AppendText(logsDirectory))
            {
                sw.WriteLine(logMessage);
            }
        }

        public void LogRead()
        {
            string logsDirectory = ConfigurationManager.AppSettings.Get("log");
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
            string logsDirectory = ConfigurationManager.AppSettings.Get("log");
            using (StreamWriter sw = File.AppendText(logsDirectory))
            {
                sw.WriteLine(@"/################# End Logging ####################\");
            }
        }
    }
}