using HomeworkPortal.Models;

namespace HomeworkPortal.Repositories
{
    public class AssignmentRepository: GenericRepository<Assignment>
    {
        public AssignmentRepository(AppDbContext context) : base(context)
        {}
    }
}
