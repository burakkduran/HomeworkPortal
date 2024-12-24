using AutoMapper;
using HomeworkPortal.Models;
using HomeworkPortal.ViewModels;

namespace HomeworkPortal.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
