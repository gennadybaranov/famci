using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core.Loggers
{
    public abstract class LoggerContext
    {
        private static ILogger current;
        private static readonly ILogger defaultLogger = new MyLogger();

        public static ILogger Current
        {
            get
            {
                if (current == null)
                    current = defaultLogger;
                return current;
            }
            set
            {
                current = value;
            }
        }
    }
}
