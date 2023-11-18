using Mojito.SimpleLogging.Configurations;
using Mojito.SimpleLogging.Extensions;

namespace Mojito.SimpleLogging
{
    internal static class LogConfigHelper
    {
        private static readonly XmlConfiguration configuration;

        private static readonly string target = "/configuration/logging/target";
        private static readonly string level = "/configuration/logging/level";
        private static readonly string pattern = "/configuration/logging/pattern";

        static LogConfigHelper()
        {
            configuration = new XmlConfiguration("App.config");
        }

        internal static string GetLog()
        {
            return configuration.Get(target, "file") ?? "Mojito.log";
        }

        internal static string GetTarget()
        {
            return configuration.Get(target, "value")?.ToLower() ?? "console";
        }

        internal static string GetLevel()
        {
            return configuration.Get(level, "value")?.ToLower() ?? "debug";
        }

        internal static string GetPattern()
        {
            return configuration.Get(pattern, "value")?.ToLower() ?? "%date [%level] %message%newline";
        }

        internal static int GetMaxRollBackups()
        {
            return configuration.Get(target, "maxRollBackups")?.ToInt() ?? 0;
        }

        internal static int GetRollTimeInMinutes()
        {
            return configuration.Get(target, "rollTimeInMinutes")?.ToInt() ?? 0;
        }

        internal static int GetMaxRollSizeInKB()
        {
            return configuration.Get(target, "rollSizeInKb")?.ToInt() ?? 0;
        }
    }
}
