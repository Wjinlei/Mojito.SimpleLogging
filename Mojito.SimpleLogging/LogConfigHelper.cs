using Microsoft.Extensions.Configuration;

namespace Mojito.SimpleLogging
{
    public static class LogConfigHelper
    {
        private static readonly IConfigurationRoot configuration;

        static LogConfigHelper()
        {
            configuration = new ConfigurationBuilder()
                .AddXmlFile("App.config", optional: true, reloadOnChange: true)
                .Build();
        }

        public static string GetLogPath()
        {
            return configuration["logging:target:file"] ?? "Mojito.log";
        }

        public static string GetLogTarget()
        {
            var target = configuration["logging:target:value"] ?? "Console";
            return target.ToLower();
        }

        public static string GetLogLevel()
        {
            var level = configuration["logging:level:value"] ?? "Debug";
            return level.ToLower();
        }

        public static string GetLogPattern()
        {
            return configuration["logging:pattern:value"] ?? "%date [%level] %message%newline";
        }

        public static int GetMaxRollBackups()
        {
            return GetInt("logging:target:maxRollBackups");
        }

        public static int GetMaxRollSizeInKB()
        {
            return GetInt("logging:target:rollSizeInKb");
        }

        public static int GetRollTimeInMinutes()
        {
            return GetInt("logging:target:rollTimeInMinutes");
        }

        private static int GetInt(string key)
        {
            var str = configuration[key] ?? "0";
            _ = int.TryParse(str, out int result);
            return result;
        }
    }
}
