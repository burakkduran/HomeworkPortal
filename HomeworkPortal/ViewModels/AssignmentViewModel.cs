using System.ComponentModel.DataAnnotations;

namespace HomeworkPortal.ViewModels
{
    public class AssignmentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Ödev Adı")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Son Teslim Tarihi")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Ders")]
        public string LessonName { get; set; }

        [Display(Name = "Durum")]
        public string Status { get; set; }
    }
} 