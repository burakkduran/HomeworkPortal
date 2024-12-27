using Microsoft.AspNetCore.Mvc;
using HomeworkPortal.Models;
using HomeworkPortal.Repositories;
using Microsoft.AspNetCore.Identity;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using HomeworkPortal.ViewModels;
using AutoMapper;

namespace HomeworkPortal.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentDashboardController : Controller
    {
        private readonly StudentGradeRepository _studentGradeRepository;
        private readonly AssignmentRepository _assignmentRepository;
        private readonly StudentSubmissionRepository _submissionRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;

        public StudentDashboardController(
            StudentGradeRepository studentGradeRepository,
            AssignmentRepository assignmentRepository,
            StudentSubmissionRepository submissionRepository,
            UserManager<AppUser> userManager,
            IMapper mapper,
            INotyfService notyf)
        {
            _studentGradeRepository = studentGradeRepository;
            _assignmentRepository = assignmentRepository;
            _submissionRepository = submissionRepository;
            _userManager = userManager;
            _mapper = mapper;
            _notyf = notyf;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            // Öğrencinin sınıflarını al
            var grades = await _studentGradeRepository.GetGradesByStudentId(user.Id);

            // Sınıflardaki dersleri al
            var lessonIds = grades.SelectMany(g => g.Lessons).Where(l => l.IsActive).Select(l => l.Id).ToList();

            // Derslere ait aktif ödevleri al
            var assignments = await _assignmentRepository
                .Where(a => a.IsActive && lessonIds.Contains(a.LessonId))
                .Include(a => a.Lesson)
                .ToListAsync();

            // Öğrencinin teslim ettiği ödevleri al
            var submissions = await _submissionRepository
                .Where(s => s.StudentId == user.Id)
                .ToListAsync();

            var assignmentModels = assignments.Select(a => new AssignmentViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                DueDate = a.DueDate,
                LessonName = a.Lesson.Name,
                Status = GetAssignmentStatus(a, submissions)
            }).ToList();

            return View(assignmentModels);
        }

        private string GetAssignmentStatus(Assignment assignment, List<StudentSubmission> submissions)
        {
            var submission = submissions.FirstOrDefault(s => s.AssignmentId == assignment.Id);
            if (submission == null)
            {
                return assignment.DueDate < DateTime.Now ? "Süresi Doldu" : "Teslim Edilmedi";
            }
            return submission.Status;
        }
    }
}