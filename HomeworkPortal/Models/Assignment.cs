namespace HomeworkPortal.Models
{
    public class Assignment : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
