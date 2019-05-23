using System;

namespace OpenWeatherSolution.Extensions
{
    public static class StringEx
    {
        public static bool EqualsIgnoreCase(this string s, string s1)
            => s.Equals(s1, StringComparison.InvariantCultureIgnoreCase);
    }
}