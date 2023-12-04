using Weather.DTOS;

namespace Weather.Interfaces.Repositories
{
    public interface IWeatherRepository
    {
        Task SaveDataAsync(IFormFile FormFile);
        Task<List<GetWeatherDTO>> GetWeathersAsync(int? Month, int? Year, int CurrentPage, int CoutRows);
        Task<int> GetWeathersCountAsync(int? Month, int? Year);
    }
}
