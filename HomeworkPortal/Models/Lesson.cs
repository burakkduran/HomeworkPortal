namespace HomeworkPortal.Models
{
    public class Lesson : BaseEntity
    {
        public string? Name { get; set; }
        public int GradeId { get; set; }
        public Grade Grade { get; set; }
    }
}
