namespace HomeworkPortal.Models
{
    public class Lesson : BaseEntity
    {
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
