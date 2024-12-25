using System.ComponentModel.DataAnnotations;

namespace HomeworkPortal.ViewModels
{
    public class AssignmentModel: BaseModel
    {
        [Display(Name = "Ders")]
        [Required(ErrorMessage = "Ders Giriniz!")]
        public int LessonId { get; set; }

        [Display(Name = "Ödev Adı")]
        [Required(ErrorMessage = "Ödev Adı Giriniz!")]
        public string Name { get; set; }

        [Display(Name = "Ödev Açıklaması")]
        [Required(ErrorMessage = "Ödev Açıklaması Giriniz!")]
        public string Description { get; set; }

        [Display(Name = "Ödev Teslim Tarihi")]
        [Required(ErrorMessage = "Ödev Teslim Tarihi Giriniz!")]
        public DateTime DueDate { get; set; }
    }
}
