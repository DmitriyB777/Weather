using System.ComponentModel.DataAnnotations;

namespace Weather.Entities
{
    public class Trash
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string? Date { get; set; }

        [MaxLength(100)]
        public string? Time { get; set; }

        [MaxLength(100)]
        public string? AirTemperature { get; set; }

        [MaxLength(100)]
        public string? RelAirHumidity { get; set; }

        [MaxLength(100)]
        public string? DewPoint { get; set; }

        [MaxLength(100)]
        public string? AtmPressure { get; set; }

        [MaxLength(100)]
        public string? WindDirection { get; set; }

        [MaxLength(100)]
        public string? WindSpeed { get; set; }

        [MaxLength(100)]
        public string? CloudCover { get; set; }

        [MaxLength(100)]
        public string? LowerCloudLimit { get; set; }

        [MaxLength(100)]
        public string? HorizontalVisibility { get; set; }

        [MaxLength(100)]
        public string? WeatherPhenomena { get; set; }

        [MaxLength(100)]
        public string? FileFullName { get; set; }

        [MaxLength(100)]
        public string? SheetName { get; set; }

        [MaxLength(100)]
        public int RowNum { get; set; }
    }
}
