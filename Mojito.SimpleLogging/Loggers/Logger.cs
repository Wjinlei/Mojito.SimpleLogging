namespace Mojito.SimpleLogging.Loggers
{
    public abstract class Logger
    {
        internal abstract void Log(string message, LogLevel level);

        internal virtual bool IsWriteable(LogLevel level)
        {
            switch (LogConfigHelper.GetLevel())
            {
                case "debug":
                    return true;
                case "info":
                    return level >= LogLevel.Info;
                case "warn":
                    return level >= LogLevel.Warn;
                case "error":
                    return level >= LogLevel.Error;
                case "fatal":
                    return level >= LogLevel.Fatal;
                default:
                    return true;
            }
        }
    }
}