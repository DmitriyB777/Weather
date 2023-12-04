using System.ComponentModel.DataAnnotations;

namespace Weather.Models
{
    public class FileModel
    {
        [Display(Name = "Выберите файл или zip архив для загрузки")]
        [Required(ErrorMessage = "Вам нужно выбрать файл или zip архив для загрузки")]
        public IFormFile? FormFile { get; set; }
        public List<string>? Errors { get; set; }
    }
}
