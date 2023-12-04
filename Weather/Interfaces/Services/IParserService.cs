using Weather.DTOS;

namespace Weather.Interfaces.Services
{
    public interface IParserService
    {
        List<WeatherDTO> WeatherExcelParse(List<FileModelDTO> Files);
        List<WeatherDTO> WeatherExcelParse(IFormFile FormFile);
    }
}
