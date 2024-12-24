using HomeworkPortal.Models;

namespace HomeworkPortal.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}