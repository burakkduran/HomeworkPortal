using System.ComponentModel.DataAnnotations;

namespace HomeworkPortal.ViewModels
{
    public class CategoryModel : BaseModel
    {
    
        [Display(Name = "Adı")]
        [Required(ErrorMessage = "Sınıf Adı Giriniz!")]
        public string Name { get; set; }

    }
}
