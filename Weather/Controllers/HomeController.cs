using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Weather.DTOS;
using Weather.Interfaces.Repositories;
using Weather.Interfaces.Services;
using Weather.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Weather.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherRepository repository;
        private readonly IUserErrorService errorService;

        public HomeController(IWeatherRepository repository, IUserErrorService errorService)
        {
            this.repository = repository;
            this.errorService = errorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowWeather()
        {
            return View();
        }

        public IActionResult LoadWeather()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(FileModel fileModel)
        {
            if (!ModelState.IsValid)
            {
                return View("LoadWeather");
            }

            if (fileModel.FormFile != null)
            {
                await repository.SaveDataAsync(fileModel.FormFile);

                fileModel.Errors = errorService.Errors;
            }

            return View("LoadWeather", fileModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather(WeatherModel weatherModel)
        {
            List<GetWeatherDTO> getWeathers = await repository.GetWeathersAsync(weatherModel.Month, weatherModel.Year, weatherModel.CurrentPage, weatherModel.CoutRows);
            weatherModel.GetWeathers = getWeathers;
            weatherModel.CoutData = await repository.GetWeathersCountAsync(weatherModel.Month, weatherModel.Year);
            weatherModel.CountPages = (int)Math.Ceiling((double)weatherModel.CoutData / weatherModel.CoutRows);

            return View("ShowWeather", weatherModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}