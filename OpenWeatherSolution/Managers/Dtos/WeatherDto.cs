using Newtonsoft.Json;

namespace OpenWeatherSolution.Managers.Dtos
{
    public class ForecastDto
    {
        [JsonProperty(PropertyName = "list")]
        public WeatherDto[] List { get; set; }
    }

    public class WeatherDto
    {
        public class WindDto
        {
            [JsonProperty(PropertyName = "speed")]
            public double Speed { get; set; }
        }

        public class CloudsDto
        {
            [JsonProperty(PropertyName = "all")]
            public double All { get; set; }
        }

        public class MainDto
        {
            [JsonProperty(PropertyName = "temp")]
            public double Temperature { get; set; }
            [JsonProperty(PropertyName = "temp_min")]
            public double TempMin { get; set; }
            [JsonProperty(PropertyName = "temp_max")]
            public double TempMax { get; set; }
            [JsonProperty(PropertyName = "pressure")]
            public double Pressure { get; set; }
            [JsonProperty(PropertyName = "humidity")]
            public double Humidity { get; set; }
        }

        public class InnerWeatherDto
        {
            [JsonProperty(PropertyName = "description")]
            public string Description { get; set; }
        }

        [JsonProperty(PropertyName = "dt_txt")]
        public string Day { get; set; }

        [JsonProperty(PropertyName = "main")]
        public MainDto Main { get; set; }

        [JsonProperty(PropertyName = "wind")]
        public WindDto Wind { get; set; }

        [JsonProperty(PropertyName = "clouds")]
        public CloudsDto Clouds { get; set; }

        [JsonProperty(PropertyName = "weather")]
        public InnerWeatherDto[] Weather { get; set; }
    }
}