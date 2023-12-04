using Weather.DTOS;

namespace Weather.Interfaces.Services
{
    public interface IUnarchivingService
    {
        Task<List<FileModelDTO>> UnarchivingZipForExcelAsync(IFormFile File);
    }
}
