using System.ComponentModel.DataAnnotations;

namespace HomeworkPortal.ViewModels
{
    public class StudentSubmissionModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Ödev seçimi zorunludur")]
        [Display(Name = "Ödev")]
        public int AssignmentId { get; set; }
        
        public string AssignmentName { get; set; }
        
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        
        [Display(Name = "Dosya")]
        public string SubmissionFilePath { get; set; }
        
        [Display(Name = "Teslim Tarihi")]
        public DateTime SubmissionDate { get; set; }
        
        [Display(Name = "Durum")]
        public string Status { get; set; }
        
        [Display(Name = "Not")]
        [Range(0, 100, ErrorMessage = "Not 0-100 arasında olmalıdır")]
        public int? Score { get; set; }
        
        [Display(Name = "Öğretmen Geri Bildirimi")]
        public string TeacherFeedback { get; set; }
    }
} 