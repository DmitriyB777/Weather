using System.IO.Compression;
using Weather.DTOS;
using Weather.Interfaces.Services;

namespace Weather.Services
{
    public class UnarchivingService : IUnarchivingService
    {
        private readonly IUserErrorService errorService;

        public UnarchivingService(IUserErrorService errorService)
        {
            this.errorService = errorService;
        }

        public async Task<List<FileModelDTO>> UnarchivingZipForExcelAsync(IFormFile File)
        {
            List<FileModelDTO> fileModels = new List<FileModelDTO>();

            await using(var stream = File.OpenReadStream())
            using(var archive = new ZipArchive(stream, ZipArchiveMode.Read))
            {
                foreach (var entry in archive.Entries)
                {
                    string ext = Path.GetExtension(entry.FullName);

                    if (ext == ".xlsx" || ext == ".xls" || ext == ".csv")
                    {
                        await using (MemoryStream memory = new MemoryStream())
                        await using (Stream entryStream = entry.Open())
                        {
                            await entryStream.CopyToAsync(memory);
                            fileModels.Add(new FileModelDTO { FullName = entry.FullName, Bytes = memory.ToArray() });
                        }
                    }

                    if (ext != ".xlsx" && ext != ".xls" && ext != ".csv")
                    {
                        errorService.AddError($"Файл {entry.FullName} не является Excel.");
                    }
                }
            }

            return fileModels;
        }
    }
}
