namespace SOLIDHomework.Core.Loggers
{
    public abstract class LoggerContext
    {
        private static ILogger current;
        private static readonly ILogger defaultLogger = new MyLogger();

        public static ILogger Current
        {
            get => current ?? (current = defaultLogger);
            set => current = value;
        }
    }
}