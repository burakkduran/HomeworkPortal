using HomeworkPortal.Models;

namespace HomeworkPortal.Repositories
{
    public class StudentSubmissionRepository : GenericRepository<StudentSubmission>
    {
        public StudentSubmissionRepository(AppDbContext context) : base(context)
        {
        }
    }
} 