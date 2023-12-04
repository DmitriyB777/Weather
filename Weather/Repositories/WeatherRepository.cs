using Microsoft.EntityFrameworkCore;
using NPOI.POIFS.FileSystem;
using System.Linq;
using Weather.Context;
using Weather.DTOS;
using Weather.Entities;
using Weather.Interfaces.Repositories;
using Weather.Interfaces.Services;

namespace Weather.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly WeatherContext context;
        private readonly IUnarchivingService unarchivingService;
        private readonly IParserService parserService;
        private readonly IUserErrorService errorService;

        public WeatherRepository(WeatherContext context, IUnarchivingService unarchivingService, IParserService parserService, IUserErrorService errorService)
        {
            this.context = context;
            this.unarchivingService = unarchivingService;
            this.parserService = parserService;
            this.errorService = errorService;
        }

        public async Task SaveDataAsync(IFormFile FormFile)
        {
            string ext = Path.GetExtension(FormFile.FileName);
            List<FileModelDTO> fileModels = new List<FileModelDTO>();
            List<WeatherDTO> weathers = new List<WeatherDTO>();

            if (ext == ".zip")
            {
                fileModels = await unarchivingService.UnarchivingZipForExcelAsync(FormFile);
                weathers = parserService.WeatherExcelParse(fileModels);
            }
            else if ((ext == ".xlsx" || ext == ".xls" || ext == ".csv"))
            {
                weathers = parserService.WeatherExcelParse(FormFile);
            }
            else
            {
                errorService.AddError($"Файл {FormFile.FileName} не является Excel или zip.");
                return;
            }

            List<WeatherDTO> data = FullData(ref weathers);

            List<WeatherDTO> trashdata = TrashData(ref data, ref weathers);

            errorService.AddError($"Загрузилось {data.Count} строк.");
            errorService.AddError($"Не загрузилось {trashdata.Count} строк.");

            foreach (WeatherDTO weather in data)
            {
                if (context.Weathers.Where(w => w.Date == DateTime.Parse(weather.Date) && w.Time == weather.Time).Count() == 0)
                {
                    await InsertFullData(weather);
                }
                else
                {
                    UpdateFullData(weather);
                }
            }

            foreach (var item in trashdata)
            {
                await InsertTrashData(item);
            }

            await context.SaveChangesAsync();
        }

        private async Task InsertFullData(WeatherDTO weather)
        {
            Entities.Weather insert = new Entities.Weather();

            insert.Date = DateTime.Parse(weather.Date);
            insert.Time = weather.Time;
            insert.AirTemperature = double.Parse(weather.AirTemperature);
            insert.RelAirHumidity = int.Parse(weather.RelAirHumidity);
            insert.DewPoint = double.Parse(weather.DewPoint);
            insert.AtmPressure = int.Parse(weather.AtmPressure);
            insert.WindDirection = weather.WindDirection;
            insert.WindSpeed = int.Parse(weather.WindSpeed);
            insert.CloudCover = int.Parse(weather.CloudCover);
            insert.LowerCloudLimit = int.Parse(weather.LowerCloudLimit);
            insert.HorizontalVisibility = int.Parse(weather.HorizontalVisibility);
            insert.WeatherPhenomena = weather.WeatherPhenomena;

            await context.AddAsync(insert);
        }

        private void UpdateFullData(WeatherDTO weather) 
        {
            Entities.Weather update = new Entities.Weather();

            update.Date = DateTime.Parse(weather.Date);
            update.Time = weather.Time;
            update.AirTemperature = double.Parse(weather.AirTemperature);
            update.RelAirHumidity = int.Parse(weather.RelAirHumidity);
            update.DewPoint = double.Parse(weather.DewPoint);
            update.AtmPressure = int.Parse(weather.AtmPressure);
            update.WindDirection = weather.WindDirection;
            update.WindSpeed = int.Parse(weather.WindSpeed);
            update.CloudCover = int.Parse(weather.CloudCover);
            update.LowerCloudLimit = int.Parse(weather.LowerCloudLimit);
            update.HorizontalVisibility = int.Parse(weather.HorizontalVisibility);
            update.WeatherPhenomena = weather.WeatherPhenomena;

            context.Weathers.Entry(update).State = EntityState.Detached;

            context.Update(update);
        }

        private List<WeatherDTO> FullData(ref List<WeatherDTO> weathers)
        {
            return weathers.Where(item => DateTime.TryParse(item.Date, out DateTime _) &&
                                          !string.IsNullOrEmpty(item.Time) &&
                                          double.TryParse(item.AirTemperature, out double _) &&
                                          int.TryParse(item.RelAirHumidity, out int _) &&
                                          double.TryParse(item.DewPoint, out double _) &&
                                          int.TryParse(item.AtmPressure, out int _) &&
                                          !string.IsNullOrEmpty(item.WindDirection) &&
                                          int.TryParse(item.WindSpeed, out int _) &&
                                          int.TryParse(item.CloudCover, out int _) &&
                                          int.TryParse(item.LowerCloudLimit, out int _) &&
                                          int.TryParse(item.HorizontalVisibility, out int _) &&
                                          !string.IsNullOrEmpty(item.WeatherPhenomena)).ToList();
        }

        private List<WeatherDTO> TrashData(ref List<WeatherDTO> fulldata, ref List<WeatherDTO> alldata)
        {
            List<WeatherDTO> result = (from ad in alldata
                                       join fd in fulldata on ad equals fd into g
                                       from fd in g.DefaultIfEmpty()
                                       where fd == null
                                       select ad).ToList();

            return result;
        }

        private async Task InsertTrashData(WeatherDTO trash)
        {
            Trash insert = new Trash
            {
                Date = trash.Date,
                Time = trash.Time,
                AirTemperature = trash.AirTemperature,
                RelAirHumidity = trash.RelAirHumidity,
                DewPoint = trash.DewPoint,
                AtmPressure = trash.AtmPressure,
                WindDirection = trash.WindDirection,
                WindSpeed = trash.WindSpeed,
                CloudCover = trash.CloudCover,
                LowerCloudLimit = trash.LowerCloudLimit,
                HorizontalVisibility = trash.HorizontalVisibility,
                WeatherPhenomena = trash.WeatherPhenomena,
                FileFullName = trash.FileFullName,
                SheetName = trash.SheetName,
                RowNum = trash.RowNum
            };

            await context.AddAsync(insert);
        }

        private IQueryable<GetWeatherDTO> Query(int? Month, int? Year)
        {
            var result = context.Weathers.Select(s => new GetWeatherDTO
            {
                Date = s.Date,
                Time = s.Time,
                AirTemperature = s.AirTemperature,
                RelAirHumidity = s.RelAirHumidity,
                DewPoint = s.DewPoint,
                AtmPressure = s.AtmPressure,
                WindDirection = s.WindDirection,
                WindSpeed = s.WindSpeed,
                CloudCover = s.CloudCover,
                LowerCloudLimit = s.LowerCloudLimit,
                HorizontalVisibility = s.HorizontalVisibility,
                WeatherPhenomena = s.WeatherPhenomena
            });

            if (Month != null)
            {
                result = result.Where(w => w.Date.Month == Month);
            }

            if (Year != null)
            {
                result = result.Where(w => w.Date.Year == Year);
            }

            return result;
        }

        public async Task<List<GetWeatherDTO>> GetWeathersAsync(int? Month, int? Year, int CurrentPage, int CoutRows)
        {
            return await Query(Month, Year).Skip(CurrentPage * CoutRows).Take(CoutRows).ToListAsync();
        }

        public async Task<int> GetWeathersCountAsync(int? Month, int? Year)
        {
            return await Query(Month, Year).CountAsync();
        }
    }
}
