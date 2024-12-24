using System.ComponentModel.DataAnnotations;

namespace HomeworkPortal.ViewModels
{
    public class LessonModel
    {
        public int Id { get; set; }

        [Display(Name = "Ders Adı")]
        [Required(ErrorMessage = "Ders Adı Giriniz!")]
        public string Name { get; set; }

        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }

        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Kategori Giriniz!")]
        public int CategoryId { get; set; }
    }
}
