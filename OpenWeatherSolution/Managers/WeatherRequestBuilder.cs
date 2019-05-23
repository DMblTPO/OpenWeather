using System;
using System.Text;
using OpenWeatherSolution.Services;
using OpenWeatherSolution.StandartTypes;

namespace OpenWeatherSolution.Managers
{
    public class WeatherRequestBuilder
    {
        private const string AppId = "9a836d4e266c2960518c8a4dd73e4896";
        private const string BaseServiceUrl = "http://api.openweathermap.org/data/2.5/";

        public static RestUrl GetWeatherUri(string city, Units units = Units.Metric, Langs lang = Langs.En)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new Exception("City not found");
            }
            var req = new StringBuilder($"weather?appid={AppId}&q={city}");
            if (units != Units.Kelvin)
            {
                req.Append($"&units={units}");
            }
            if (lang != Langs.En)
            {
                req.Append($"&lang={lang}");
            }

            return new RestUrl {Base = BaseServiceUrl, Route = req.ToString()};
        }

        public static RestUrl GetForecastUri(string city, Units units = Units.Metric, Langs lang = Langs.En)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new Exception("City not found");
            }
            var req = new StringBuilder($"forecast?appid={AppId}&q={city}");
            if (units != Units.Kelvin)
            {
                req.Append($"&units={units}");
            }
            if (lang != Langs.En)
            {
                req.Append($"&lang={lang}");
            }
            return new RestUrl {Base = BaseServiceUrl, Route = req.ToString()};
        }
    }
}