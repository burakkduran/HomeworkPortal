namespace HomeworkPortal.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
