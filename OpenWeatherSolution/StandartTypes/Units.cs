using System;

namespace OpenWeatherSolution.StandartTypes
{
    public enum Units
    {
        Kelvin, // by default in OW API
        Metric, // Celsius by default in my API
        Imperial
    }

    public static class UnitsEx
    {
        public static Units UnitsMap(this string metrics)
            => metrics.EqualsIgnoreCase("Kelvin")
                ? Units.Kelvin
                : metrics.EqualsIgnoreCase("Celsius")
                    ? Units.Metric
                    : metrics.EqualsIgnoreCase("Fahrenheit")
                        ? Units.Imperial
                        : throw new Exception("Wrong temperature metrics");
    }

    public static class StringEx
    {
        public static bool EqualsIgnoreCase(this string s, string s1)
            => s.Equals(s1, StringComparison.InvariantCultureIgnoreCase);
    }

    public enum Langs
    {
        En, // English
        Ua, // Ukraine
        Ru // Russian
    }
}