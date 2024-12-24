using System.ComponentModel.DataAnnotations;

namespace HomeworkPortal.ViewModels
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Display(Name = "Adı")]
        [Required(ErrorMessage = "Kategori Adı Giriniz!")]
        public string Name { get; set; }
        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
    }
}
