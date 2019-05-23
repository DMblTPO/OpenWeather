using OpenWeatherSolution.StandartTypes;

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
}