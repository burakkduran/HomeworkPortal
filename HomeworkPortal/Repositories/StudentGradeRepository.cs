using HomeworkPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeworkPortal.Repositories
{
    public class StudentGradeRepository : GenericRepository<StudentGrade>
    {
        private readonly AppDbContext _context;

        public StudentGradeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<AppUser>> GetStudentsByGradeId(int gradeId)
        {
            return await _context.StudentGrades
                .Include(x => x.Student)
                .Where(x => x.GradeId == gradeId && x.IsActive)
                .Select(x => x.Student)
                .ToListAsync();
        }

        public async Task<List<Grade>> GetGradesByStudentId(string studentId)
        {
            return await _context.StudentGrades
                .Include(x => x.Grade)
                                    .ThenInclude(g => g.Lessons)
                .Where(x => x.StudentId == studentId && x.IsActive)
                .Select(x => x.Grade)
                .ToListAsync();
        }

        public async Task<bool> IsStudentInGrade(string studentId, int gradeId)
        {
            return await _context.StudentGrades
                .AnyAsync(x => x.StudentId == studentId && x.GradeId == gradeId && x.IsActive);
        }
    }
} 