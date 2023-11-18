using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Mojito.SimpleLogging
{
    internal static class LogUtil
    {
        private static readonly Dictionary<string, long> SizeUnits;

        static LogUtil()
        {
            SizeUnits = new Dictionary<string, long>
            {
                { "B", 1 },
                { "KB", 1024 },
                { "MB", 1024 * 1024 },
                { "GB", 1024 * 1024 * 1024 },
            };
        }

        internal static long SizeUnitConvert(string input)
        {
            Match match = Regex.Match(input.ToUpper(), @"(\d+)\s*([A-Z]+)");
            if (!match.Success)
                return 0;

            _ = long.TryParse(match.Groups[1].Value, out long value);
            string unit = match.Groups[2].Value;
            if (SizeUnits.ContainsKey(unit))
                return value * SizeUnits[unit];

            return value;
        }
    }
}
