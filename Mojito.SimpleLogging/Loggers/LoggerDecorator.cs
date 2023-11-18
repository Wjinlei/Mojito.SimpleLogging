using System;

namespace Mojito.SimpleLogging.Loggers
{
    internal class LoggerDecorator : Logger
    {
        private readonly Logger logger;

        internal LoggerDecorator(Logger logger)
        {
            this.logger = logger;
        }

        internal override void Log(string message, LogLevel level)
        {
            var strLevel = "";
            switch (level)
            {
                case LogLevel.Debug:
                    strLevel = "Debug";
                    break;
                case LogLevel.Info:
                    strLevel = "Info";
                    break;
                case LogLevel.Warn:
                    strLevel = "Warn";
                    break;
                case LogLevel.Error:
                    strLevel = "Error";
                    break;
                case LogLevel.Fatal:
                    strLevel = "Fatal";
                    break;
            }

            var newMessage = LogConfigHelper.GetPattern()
                .Replace("%date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Replace("%level", strLevel)
                .Replace("%message", message)
                .Replace("%newline", Environment.NewLine);

            logger.Log(newMessage, level);
        }
    }
}
