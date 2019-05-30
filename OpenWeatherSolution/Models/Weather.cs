using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace OpenWeatherSolution.Models
{
    public class Weather
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

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

    public class WeatherContext : DbContext
    {
        public DbSet<Weather> Weathers { get; set; }

        public WeatherContext(DbContextOptions<WeatherContext> options)
            : base(options)
        {
        }
    }

}