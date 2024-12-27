using Microsoft.AspNetCore.Mvc;
using HomeworkPortal.Repositories;
using HomeworkPortal.Models;
using HomeworkPortal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using HomeworkPortal.Hubs;

namespace HomeworkPortal.Controllers
{
    [Authorize]
    public class StudentSubmissionController : Controller
    {
        private readonly StudentSubmissionRepository _submissionRepository;
        private readonly AssignmentRepository _assignmentRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;
        private readonly IWebHostEnvironment _env;
        private readonly IHubContext<GeneralHub> _generalHub;

        public StudentSubmissionController(
            StudentSubmissionRepository submissionRepository,
            AssignmentRepository assignmentRepository,
            UserManager<AppUser> userManager,
            IMapper mapper,
            INotyfService notyf,
            IWebHostEnvironment env,
            IHubContext<GeneralHub> generalHub)
        {
            _submissionRepository = submissionRepository;
            _assignmentRepository = assignmentRepository;
            _userManager = userManager;
            _mapper = mapper;
            _notyf = notyf;
            _env = env;
            _generalHub = generalHub;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var submissions = _submissionRepository.Where(x => x.StudentId == user.Id).ToList();
            var submissionModels = _mapper.Map<List<StudentSubmissionModel>>(submissions);
            return View(submissionModels);
        }

        public async Task<IActionResult> Submit(int assignmentId)
        {
            var assignment = await _assignmentRepository.GetByIdAsync(assignmentId);
            if (assignment == null)
            {
                _notyf.Error("Ödev bulunamadı!");
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Assignment = assignment;
            return View(new StudentSubmissionModel { AssignmentId = assignmentId });
        }

        [HttpPost]
        public async Task<IActionResult> Submit(StudentSubmissionModel model, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "Lütfen bir dosya seçiniz!");
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            var assignment = await _assignmentRepository.GetByIdAsync(model.AssignmentId);

            if (assignment == null)
            {
                _notyf.Error("Ödev bulunamadı!");
                return RedirectToAction("Index", "Home");
            }

            // Dosya yükleme işlemi
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var submission = new StudentSubmission
            {
                AssignmentId = model.AssignmentId,
                StudentId = user.Id,
                SubmissionFilePath = uniqueFileName,
                SubmissionDate = DateTime.Now,
                Status = DateTime.Now > assignment.DueDate ? "Late" : "Submitted",
                Created = DateTime.Now,
                Updated = DateTime.Now,
                IsActive = true
            };

            await _submissionRepository.AddAsync(submission);
            _notyf.Success("Ödev başarıyla yüklendi.");
            
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Grade(int id)
        {
            var submission = await _submissionRepository.GetByIdAsync(id);
            if (submission == null)
            {
                _notyf.Error("Ödev teslimi bulunamadı!");
                return RedirectToAction("Index");
            }

            var submissionModel = _mapper.Map<StudentSubmissionModel>(submission);
            return View(submissionModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Grade(StudentSubmissionModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var submission = await _submissionRepository.GetByIdAsync(model.Id);
            if (submission == null)
            {
                _notyf.Error("Ödev teslimi bulunamadı!");
                return RedirectToAction("Index");
            }

            submission.Score = model.Score;
            submission.TeacherFeedback = model.TeacherFeedback;
            submission.Status = "Graded";
            submission.Updated = DateTime.Now;

            await _submissionRepository.UpdateAsync(submission);
            _notyf.Success("Not ve geri bildirim kaydedildi.");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Download(int id)
        {
            var submission = await _submissionRepository.GetByIdAsync(id);
            if (submission == null)
            {
                _notyf.Error("Dosya bulunamadı!");
                return RedirectToAction("Index");
            }

            var filePath = Path.Combine(_env.WebRootPath, "uploads", submission.SubmissionFilePath);
            if (!System.IO.File.Exists(filePath))
            {
                _notyf.Error("Dosya bulunamadı!");
                return RedirectToAction("Index");
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, "application/octet-stream", submission.SubmissionFilePath);
        }
    }
} 