namespace OpenWeatherSolution.Controllers.Dtos
{
    public class SortParam
    {
        public string Field { get; set; }
        public bool Asc { get; set; }
    }

    public class GetWeatherDto
    {
        public string City { get; set; }
        public string Metrics { get; set; }
        public string Lang { get; set; }
        public SortParam SortBy { get; set; }
    }

    public class WeatherDto
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public string City { get; set; }

        public double Wind { get; set; } // .Speed

        public double Clouds { get; set; } // .All

        public double Temperature { get; set; }

        public double TempMin { get; set; }

        public double TempMax { get; set; }
        
        public double Pressure { get; set; }
        
        public double Humidity { get; set; }

        public string Description { get; set; }
    }
}