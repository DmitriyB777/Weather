using System.ComponentModel.DataAnnotations;

namespace Weather.DTOS
{
    public class GetWeatherDTO
    {
        public DateTime Date { get; set; }

        public string? Time { get; set; }

        public double AirTemperature { get; set; }

        public int RelAirHumidity { get; set; }

        public double DewPoint { get; set; }

        public int AtmPressure { get; set; }

        public string? WindDirection { get; set; }

        public int WindSpeed { get; set; }

        public int CloudCover { get; set; }

        public int LowerCloudLimit { get; set; }

        public int HorizontalVisibility { get; set; }

        public string? WeatherPhenomena { get; set; }
    }
}
