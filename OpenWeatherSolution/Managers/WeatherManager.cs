using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OpenWeatherSolution.Controllers.Dtos;
using OpenWeatherSolution.Extensions;
using OpenWeatherSolution.Managers.Dtos;
using OpenWeatherSolution.Models;
using OpenWeatherSolution.Services;
using OpenWeatherSolution.StandartTypes;
using WeatherDto = OpenWeatherSolution.Managers.Dtos.WeatherDto;

namespace OpenWeatherSolution.Managers
{
    public interface IWeatherManager
    {
        Task<Weather> GetWeatherAsync(string city, Units units, Langs lang);
        Task<Weather[]> GetForecastAsync(string city, Units units, Langs lang, SortParam sortBy);
        Task SaveWeatherAsync(Weather weather);
        Task<Weather> LoadWeatherAsync(string city);
    }

    public class WeatherManager : IWeatherManager
    {
        private readonly WeatherContext _dbContext;

        public WeatherManager(WeatherContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Weather> GetWeatherAsync(string city, Units units, Langs lang)
        {
            var reqUrl = WeatherRequestBuilder.GetWeatherUri(city, units, lang);
            var dto = await RestClient.GetAsync<WeatherDto>(reqUrl);
            return new Weather
            {
                City = city,
                Date = DateTime.Parse(dto.Day),
                Temperature = dto.Main.Temperature,
                TempMin = dto.Main.TempMin,
                TempMax = dto.Main.TempMax,
                Pressure = dto.Main.Pressure,
                Humidity = dto.Main.Humidity,
                Wind = dto.Wind.Speed,
                Clouds = dto.Clouds.All,
                Description = dto.Weather[0]?.Description
            };
        }

        public async Task<Weather[]> GetForecastAsync(string city, Units units, Langs lang, SortParam sortBy)
        {
            var reqUrl = WeatherRequestBuilder.GetForecastUri(city, units, lang);
            var forecastDto = await RestClient.GetAllAsync<ForecastDto>(reqUrl);
            var list = forecastDto.List
                .Select(dto => new Weather
                {
                    City = city,
                    Date = DateTime.Parse(dto.Day),
                    Temperature = dto.Main.Temperature,
                    TempMin = dto.Main.TempMin,
                    TempMax = dto.Main.TempMax,
                    Pressure = dto.Main.Pressure,
                    Humidity = dto.Main.Humidity,
                    Wind = dto.Wind.Speed,
                    Clouds = dto.Clouds.All,
                    Description = dto.Weather[0]?.Description
                });
            var orderedList = list;
            if (sortBy != null)
            {
                var propertyInfo = typeof(Weather).GetProperties()
                    .FirstOrDefault(x => x.Name.EqualsIgnoreCase(sortBy.Field));
                orderedList = sortBy.Asc
                    ? list.OrderBy(x => propertyInfo.GetValue(x, null))
                    : list.OrderByDescending(x => propertyInfo.GetValue(x, null));
            }

            return orderedList.ToArray();
        }

        public async Task SaveWeatherAsync(Weather weather)
        {
              var weatherDb = await _dbContext.Weathers
                  .Where(x => x.City.Equals(weather.City, StringComparison.InvariantCultureIgnoreCase) &&
                              x.Date == weather.Date)
                  .FirstOrDefaultAsync();
              if (weatherDb != null)
              {
                  return;
              }
              await _dbContext.Weathers.AddAsync(weather);
              await _dbContext.SaveChangesAsync();
        }

        public async Task<Weather> LoadWeatherAsync(string city)
            => await _dbContext
                .Weathers
                .OrderByDescending(w => w.Date)
                .FirstOrDefaultAsync(x => x.City.Equals(city, StringComparison.InvariantCultureIgnoreCase));
    }
}