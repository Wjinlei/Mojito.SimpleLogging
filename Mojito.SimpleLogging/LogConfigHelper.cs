﻿using Mojito.SimpleLogging.Configurations;
using Mojito.SimpleLogging.Extensions;

namespace Mojito.SimpleLogging
{
    internal static class LogConfigHelper
    {
        private static readonly XmlConfiguration configuration;

        private static readonly string target = "/configuration/logging/target";
        private static readonly string level = "/configuration/logging/level";
        private static readonly string pattern = "/configuration/logging/pattern";
        private static readonly string dateFormat = "/configuration/logging/dateFormat";

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

        internal static string GetDateFormat()
        {
            return configuration.Get(dateFormat, "value") ?? "yyyy-MM-dd HH:mm:ss";
        }

        internal static string GetPattern()
        {
            return configuration.Get(pattern, "value")?.ToLower() ?? "%date [%thread] %level %logger - %message%newline";
        }

        internal static int GetMaxRollBackups()
        {
            return configuration.Get(target, "maxRollBackups")?.ToInt() ?? 0;
        }

        internal static long GetMaxRollTime()
        {
            var strMaxRollTime = configuration.Get(target, "maxRollTime");
            if (strMaxRollTime != null)
                return LogUtil.TimeUnitConvert(strMaxRollTime);
            return 0;
        }

        internal static long GetMaxRollSize()
        {
            var strMaxRollSize = configuration.Get(target, "maxRollSize");
            if (strMaxRollSize != null)
                return LogUtil.SizeUnitConvert(strMaxRollSize);
            return 0;
        }
    }
}
