using System.Xml.XPath;

namespace Mojito.SimpleLogging
{
    public static class LogConfigHelper
    {
        private static readonly XPathNavigator xPathNavigator;

        static LogConfigHelper()
        {
            try
            {
                xPathNavigator = new XPathDocument("App.config").CreateNavigator();
            }
            catch
            {
                xPathNavigator = null;
            }
        }

        public static string GetLogTarget()
        {
            if (xPathNavigator == null)
                return "console";
            var target = GetTarget();
            return target.MoveToAttribute("value", "") ? target.Value.ToLower() : "console";
        }

        public static string GetLogPath()
        {
            if (xPathNavigator == null)
                return "Mojito.log";
            var target = GetTarget();
            return target.MoveToAttribute("file", "") ? target.Value : "Mojito.log";
        }

        public static int GetMaxRollBackups()
        {
            if (xPathNavigator == null)
                return 0;
            var target = GetTarget();
            return target.MoveToAttribute("maxRollBackups", "") ? ParseInt(target.Value) : 0;
        }

        public static int GetRollTimeInMinutes()
        {
            if (xPathNavigator == null)
                return 0;
            var target = GetTarget();
            return target.MoveToAttribute("rollTimeInMinutes", "") ? ParseInt(target.Value) : 0;
        }

        public static int GetMaxRollSizeInKB()
        {
            if (xPathNavigator == null)
                return 0;
            var target = GetTarget();
            return target.MoveToAttribute("rollSizeInKb", "") ? ParseInt(target.Value) : 0;
        }

        public static string GetLogLevel()
        {
            if (xPathNavigator == null)
                return "debug";
            var level = GetLevel();
            return level.MoveToAttribute("value", "") ? level.Value.ToLower() : "debug";
        }

        public static string GetLogPattern()
        {
            if (xPathNavigator == null)
                return "%date [%level] %message%newline";
            var pattern = GetPattern();
            return pattern.MoveToAttribute("value", "")
                ? pattern.Value.ToLower() : "%date [%level] %message%newline";
        }

        private static XPathNavigator GetTarget()
        {
            return xPathNavigator.SelectSingleNode("/configuration/logging/target");
        }

        private static XPathNavigator GetPattern()
        {
            return xPathNavigator.SelectSingleNode("/configuration/logging/pattern");
        }

        private static XPathNavigator GetLevel()
        {
            return xPathNavigator.SelectSingleNode("/configuration/logging/level");
        }

        private static int ParseInt(string key)
        {
            _ = int.TryParse(key, out int result);
            return result;
        }
    }
}
