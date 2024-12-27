using System.ComponentModel.DataAnnotations;

namespace HomeworkPortal.Models
{
    public class StudentSubmission : BaseEntity
    {
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        public string StudentId { get; set; }
        public AppUser Student { get; set; }

        [Required]
        public string SubmissionFilePath { get; set; }

        public DateTime SubmissionDate { get; set; }

        public string Status { get; set; } // Submitted, Late, Graded

        public int? Score { get; set; }

        public string? TeacherFeedback { get; set; }
    }
} 