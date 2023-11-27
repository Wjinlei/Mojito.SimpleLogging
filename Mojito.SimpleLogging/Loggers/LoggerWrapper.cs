using System;
using System.Diagnostics;
using System.Threading;

namespace Mojito.SimpleLogging.Loggers
{
    public class LoggerWrapper : Logger
    {
        private readonly Logger logger;
        private readonly Type loggerType;

        internal LoggerWrapper(Logger logger, Type loggerType)
        {
            this.logger = logger;
            this.loggerType = loggerType;
        }

        internal override void Log(string message, LogLevel level)
        {
            var newMessage = LogConfigHelper.GetPattern()
                .Replace("%date", DateTime.Now.ToString(LogConfigHelper.GetDateFormat())) // FormatException
                .Replace("%level", level.ToString().ToUpper())
                .Replace("%thread", $"{Thread.CurrentThread.Name ?? "Unnamed"}: {Thread.CurrentThread.ManagedThreadId}")
                .Replace("%logger", $"{loggerType}")
                .Replace("%stack", $"{new StackTrace(true)}")
                .Replace("%message", message)
                .Replace("%newline", Environment.NewLine);

            logger?.Log(newMessage, level);
        }

        public void Debug(string message)
        {
            Log(message, LogLevel.Debug);
        }

        public void Info(string message)
        {
            Log(message, LogLevel.Info);
        }

        public void Warn(string message)
        {
            Log(message, LogLevel.Warn);
        }

        public void Error(string message)
        {
            Log(message, LogLevel.Error);
        }

        public void Fatal(string message)
        {
            Log(message, LogLevel.Fatal);
        }
    }
}
