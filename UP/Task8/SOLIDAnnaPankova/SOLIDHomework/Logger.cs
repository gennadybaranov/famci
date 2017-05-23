using SOLIDHomework.Core.Loggers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework
{
    class Logger : ILogger
    {
        private readonly string filePath;
        public Logger()
        {
            filePath = Properties.Settings.Default.logPath;
        }
        public void Write(string text)
        {
            using (Stream file = File.OpenWrite(filePath))
            {
                using (StreamWriter writer = new StreamWriter(file))
                {
                    writer.WriteLine(text);
                    Console.WriteLine(text);
                }
            }
        }

    }
}
