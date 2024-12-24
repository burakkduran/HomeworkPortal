using AutoMapper;
using HomeworkPortal.Models;
using HomeworkPortal.ViewModels;



namespace HomeworkPortal.Repositories
{
    public class LessonRepository : GenericRepository<Lesson>
    {
        public LessonRepository(AppDbContext context) : base(context)
        {
        }
    }
}
