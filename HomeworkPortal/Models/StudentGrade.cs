namespace HomeworkPortal.Models
{
    public class StudentGrade : BaseEntity
    {
        public string StudentId { get; set; }
        public AppUser Student { get; set; }

        public int GradeId { get; set; }
        public Grade Grade { get; set; }
    }
} 