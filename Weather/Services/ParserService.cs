using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Weather.DTOS;
using Weather.Interfaces.Services;

namespace Weather.Services
{
    public class ParserService : IParserService
    {
        private readonly IUserErrorService errorService;

        public ParserService(IUserErrorService errorService)
        {
            this.errorService = errorService;
        }

        public List<WeatherDTO> WeatherExcelParse(List<FileModelDTO> Files)
        {
            var result = new List<WeatherDTO>();

            foreach (FileModelDTO file in Files)
            {
                if (file.Bytes != null)
                {
                    using (IWorkbook workbook = new XSSFWorkbook(new MemoryStream(file.Bytes)))
                    {
                        for (int sheetnum = 0; sheetnum < workbook.NumberOfSheets; sheetnum++)
                        {
                            ISheet sheet = workbook.GetSheetAt(sheetnum);
                            for (int row = 4; row <= sheet.LastRowNum; row++)
                            {
                                IRow currentRow = sheet.GetRow(row);
                                if (currentRow != null)
                                {
                                    result.Add(ParseRow(currentRow, file.FullName, sheet.SheetName));
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        public List<WeatherDTO> WeatherExcelParse(IFormFile FormFile)
        {
            List<FileModelDTO> Files = new List<FileModelDTO>();

            using (MemoryStream memory = new MemoryStream())
            using (var stream = FormFile.OpenReadStream())
            {
                stream.CopyTo(memory);
                Files.Add(new FileModelDTO { FullName = FormFile.FileName, Bytes = memory.ToArray() });
            }

            return WeatherExcelParse(Files);
        }

        private WeatherDTO ParseRow(IRow row, string? fullname, string? sheetname)
        {
            WeatherDTO weather = new WeatherDTO
            {
                Date = row.GetCell(0)?.ToString(),
                Time = row.GetCell(1)?.ToString(),
                AirTemperature = row.GetCell(2)?.ToString(),
                RelAirHumidity = row.GetCell(3)?.ToString(),
                DewPoint = row.GetCell(4)?.ToString(),
                AtmPressure = row.GetCell(5)?.ToString(),
                WindDirection = row.GetCell(6)?.ToString(),
                WindSpeed = row.GetCell(7)?.ToString(),
                CloudCover = row.GetCell(8)?.ToString(),
                LowerCloudLimit = row.GetCell(9)?.ToString(),
                HorizontalVisibility = row.GetCell(10)?.ToString(),
                WeatherPhenomena = row.GetCell(11)?.ToString(),
                FileFullName = fullname,
                SheetName = sheetname,
                RowNum = row.RowNum + 1
            };

            return weather;
        }
    }
}
