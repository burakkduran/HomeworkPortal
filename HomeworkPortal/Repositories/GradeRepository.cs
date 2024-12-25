using AutoMapper;
using HomeworkPortal.Models;
using HomeworkPortal.ViewModels;

namespace HomeworkPortal.Repositories
{
    public class GradeRepository : GenericRepository<Grade>
    {
        public GradeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
