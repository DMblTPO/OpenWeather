using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenWeatherSolution.Controllers.Dtos;
using OpenWeatherSolution.Extensions;
using OpenWeatherSolution.Managers;
using OpenWeatherSolution.Models;
using OpenWeatherSolution.StandartTypes;

namespace OpenWeatherSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherManager _manager;

        public WeatherController(IWeatherManager manager)
        {
            _manager = manager;
        }

        [HttpGet("GetCurrent")]
        public async Task<ActionResult> GetCurrentAsync(
            [FromQuery]string city, 
            [FromQuery]string metrics="Celsius", 
            [FromQuery]string lang="En")
        {
            return Ok(await _manager.GetWeatherAsync(city, metrics.UnitsMap(), Enum.Parse<Langs>(lang)));
        }

        [HttpGet("GetWeather")]
        public async Task<WeatherDto[]> GetWeatherAsync(GetWeatherDto dto)
        {
            var weathers = await _manager.GetForecastAsync(dto.City, dto.Metrics.UnitsMap(),
                Enum.Parse<Langs>(dto.Lang), dto.SortBy);

            return weathers
                .GroupBy(x => x.Date.Day)
                .Select(x => x.First(xx => xx.Date.TimeOfDay >= TimeSpan.FromHours(12)))
                .Select(x => new WeatherDto
                {
                    City = x.City,
                    Date = x.Date.ToString("dd MMM yyyy"),
                    Wind = x.Wind,
                    Clouds = x.Clouds,
                    Temperature = x.Temperature,
                    TempMin = x.TempMin,
                    TempMax = x.TempMax,
                    Pressure = x.Pressure,
                    Humidity = x.Humidity,
                    Description = x.Description
                })
                .ToArray();
        }

        [HttpPost("SaveWeather")]
        public async Task<ActionResult> SaveWeatherAsync(Weather weather)
        {
            await _manager.SaveWeatherAsync(weather);
            return Ok();
        }

        [HttpGet("LoadWeather/{city}")]
        public async Task<ActionResult> LoadWeatherAsync([FromRoute] string city)
        {
            return Ok(await _manager.LoadWeatherAsync(city));
        }
    }
}
