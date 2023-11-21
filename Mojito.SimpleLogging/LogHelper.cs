using Mojito.SimpleLogging.Loggers;

namespace Mojito.SimpleLogging
{
    public static class LogHelper
    {
        /// <summary>
        /// 获得日志记录器
        /// </summary>
        public static LoggerWrapper GetLogger(System.Type loggerType)
        {
            switch (LogConfigHelper.GetTarget())
            {
                case "console":
                    return new LoggerWrapper(new ConsoleLogger(), loggerType);
                case "file":
                    return new LoggerWrapper(new FileLogger(), loggerType);
                default:
                    return null;
            }
        }
    }
}
