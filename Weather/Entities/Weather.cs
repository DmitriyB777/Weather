using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Weather.Entities
{
    [PrimaryKey(nameof(Date), nameof(Time))]
    public class Weather
    {
        public DateTime Date { get; set; }

        [MaxLength(5)]
        public string? Time { get; set; }

        public double AirTemperature { get; set; }

        public int RelAirHumidity { get; set; }

        public double DewPoint { get; set; }

        public int AtmPressure { get; set; }

        [MaxLength(20)]
        [Required]
        public string? WindDirection { get; set; }

        public int WindSpeed { get; set; }

        public int CloudCover { get; set; }

        public int LowerCloudLimit { get; set; }

        public int HorizontalVisibility { get; set; }

        [MaxLength(100)]
        [Required]
        public string? WeatherPhenomena { get; set; }
    }
}
