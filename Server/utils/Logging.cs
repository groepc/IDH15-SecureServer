using System;
using System.IO;
using System.Reflection;

namespace Server.utils
{

    public class Logging
    {
        public void LogStart(string logMessage)
        {
            string logsDirectory = @"..\..\setup-log\log.txt";
           
            // This text is added only once to the file.
            if (!File.Exists(logsDirectory))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(logsDirectory))
                {
                    sw.WriteLine(logMessage);
                }
            }

            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (StreamWriter sw = File.AppendText(logsDirectory))
            {
                sw.WriteLine(logMessage);
            }

            // Open the file to read from.
            using (StreamReader sr = File.OpenText(logsDirectory))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }

        }
    }
}