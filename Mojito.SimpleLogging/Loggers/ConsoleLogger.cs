using System;

namespace Mojito.SimpleLogging.Loggers
{
    public class ConsoleLogger : Logger
    {
        public override void Log(string message, LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case LogLevel.Info:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case LogLevel.Warn:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogLevel.Fatal:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }


            if (IsWriteable(level))
                Console.WriteLine(message);

            Console.ResetColor(); // finally reset
        }
    }
}
