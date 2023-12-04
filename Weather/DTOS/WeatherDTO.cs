namespace Weather.DTOS
{
    public class WeatherDTO
    {
        public string? Date { get; set; }

        public string? Time { get; set; }

        public string? AirTemperature { get; set; }

        public string? RelAirHumidity { get; set; }

        public string? DewPoint { get; set; }

        public string? AtmPressure { get; set; }

        public string? WindDirection { get; set; }

        public string? WindSpeed { get; set; }

        public string? CloudCover { get; set; }

        public string? LowerCloudLimit { get; set; }

        public string? HorizontalVisibility { get; set; }

        public string? WeatherPhenomena { get; set; }

        public string? FileFullName { get; set; }

        public string? SheetName { get; set; }

        public int RowNum { get; set; }
    }
}
