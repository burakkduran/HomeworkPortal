namespace HomeworkPortal.Models
{
    public class Grade : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
    }
}
