using System.ComponentModel.DataAnnotations;

namespace HomeworkPortal.ViewModels
{
    public class GradeModel : BaseModel
    {
    
        [Display(Name = "Adı")]
        [Required(ErrorMessage = "Sınıf Adı Giriniz!")]
        public string Name { get; set; }

    }
}
