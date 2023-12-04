using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Weather.DTOS;

namespace Weather.Models
{
    public class WeatherModel
    {
        [Display(Name = "Выберите месяц")]
        public int? Month { get; set; }
        [Display(Name = "Выберите год")]
        public int? Year { get; set; }
        public List<GetWeatherDTO>? GetWeathers { get; set; }
        public int CurrentPage { get; set; }
        public int CoutRows { get; set; }
        public int CountPages { get; set; }
        public int CoutData { get; set; }
    }
}
