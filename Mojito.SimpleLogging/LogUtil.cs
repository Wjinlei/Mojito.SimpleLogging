using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Mojito.SimpleLogging
{
    internal static class LogUtil
    {
        private static readonly Dictionary<string, long> SizeUnits;
        private static readonly Dictionary<string, long> TimeUnits;

        static LogUtil()
        {
            SizeUnits = new Dictionary<string, long>
            {
                { "B", 1 },
                { "KB", 1024 },
                { "MB", 1024 * 1024 },
                { "GB", 1024 * 1024 * 1024 },
            };

            TimeUnits = new Dictionary<string, long>
            {
                { "S", 1 },
                { "M", 60 },
                { "H", 60 * 60 },
                { "D", 60 * 60 * 24 },
            };
        }

        internal static long SizeUnitConvert(string input)
        {
            return UnitConvert(input, SizeUnits);
        }

        internal static long TimeUnitConvert(string input)
        {
            return UnitConvert(input, TimeUnits);
        }

        internal static long UnitConvert(string input, Dictionary<string, long> dict)
        {
            Match match = Regex.Match(input.ToUpper(), @"(\d+)\s*([A-Z]+)");
            if (!match.Success)
                return 0;

            _ = long.TryParse(match.Groups[1].Value, out long value);
            string unit = match.Groups[2].Value;
            if (dict.ContainsKey(unit))
                return value * dict[unit];

            return value;
        }
    }
}
