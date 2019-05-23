using System;
using OpenWeatherSolution.StandartTypes;

namespace OpenWeatherSolution.Extensions
{
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
}