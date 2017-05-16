using System.Configuration;
using System.IO;

namespace SOLIDHomework.Core.Loggers
{
    public class MyLogger : ILogger
    {
        private readonly string filePath;
        public MyLogger()
        {
            filePath = ConfigurationManager.AppSettings["logPath"];
           // filePath = Properties.Settings.Default.logPath;
        }
        public void Write(string text)
        {
            using (Stream file = File.OpenWrite(filePath))
            {
                using (StreamWriter writer = new StreamWriter(file))
                {
                    writer.WriteLine(text);
                }
            }
        }
    }
}
