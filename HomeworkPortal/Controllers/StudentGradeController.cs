using Microsoft.AspNetCore.Mvc;
using HomeworkPortal.Models;
using HomeworkPortal.Repositories;
using Microsoft.AspNetCore.Identity;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace HomeworkPortal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentGradeController : Controller
    {
        private readonly StudentGradeRepository _studentGradeRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly GradeRepository _gradeRepository;
        private readonly INotyfService _notyf;

        public StudentGradeController(
            StudentGradeRepository studentGradeRepository,
            UserManager<AppUser> userManager,
            GradeRepository gradeRepository,
            INotyfService notyf)
        {
            _studentGradeRepository = studentGradeRepository;
            _userManager = userManager;
            _gradeRepository = gradeRepository;
            _notyf = notyf;
        }

        public async Task<IActionResult> ManageStudents(int gradeId)
        {
            var grade = await _gradeRepository.GetByIdAsync(gradeId);
            if (grade == null)
            {
                _notyf.Error("Sınıf bulunamadı!");
                return RedirectToAction("Index", "Grade");
            }

            ViewBag.Grade = grade;

            // Sınıftaki öğrencileri al
            var studentsInGrade = await _studentGradeRepository.GetStudentsByGradeId(gradeId);
            
            // Tüm öğrencileri al (Student rolünde olanlar)
            var allStudents = await _userManager.GetUsersInRoleAsync("Student");

            // Sınıfta olmayan öğrencileri bul
            var studentsNotInGrade = allStudents.Where(s => !studentsInGrade.Any(sig => sig.Id == s.Id)).ToList();

            ViewBag.StudentsInGrade = studentsInGrade;
            ViewBag.StudentsNotInGrade = studentsNotInGrade;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(string studentId, int gradeId)
        {
            var isAlreadyInGrade = await _studentGradeRepository.IsStudentInGrade(studentId, gradeId);
            if (isAlreadyInGrade)
            {
                _notyf.Warning("Öğrenci zaten bu sınıfta!");
                return RedirectToAction("ManageStudents", new { gradeId });
            }

            var studentGrade = new StudentGrade
            {
                StudentId = studentId,
                GradeId = gradeId,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                IsActive = true
            };

            await _studentGradeRepository.AddAsync(studentGrade);
            _notyf.Success("Öğrenci sınıfa eklendi.");

            return RedirectToAction("ManageStudents", new { gradeId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveStudent(string studentId, int gradeId)
        {
            var studentGrade = await _studentGradeRepository
                .Where(x => x.StudentId == studentId && x.GradeId == gradeId && x.IsActive)
                .FirstOrDefaultAsync();

            if (studentGrade != null)
            {
                studentGrade.IsActive = false;
                studentGrade.Updated = DateTime.Now;
                await _studentGradeRepository.UpdateAsync(studentGrade);
                _notyf.Success("Öğrenci sınıftan çıkarıldı.");
            }

            return RedirectToAction("ManageStudents", new { gradeId });
        }
    }
} 