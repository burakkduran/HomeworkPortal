using Microsoft.AspNetCore.Mvc;
using HomeworkPortal.Repositories;
using HomeworkPortal.Models;
using HomeworkPortal.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace HomeworkPortal.Controllers
{
    [Authorize(Roles = "Admin")]

    public class LessonController : Controller
    {
        private readonly LessonRepository _lessonRepository;
        private readonly GradeRepository _gradeRepository;
        private readonly AssignmentRepository _assignmentRepository;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;

        public LessonController(LessonRepository lessonRepository, GradeRepository gradeRepository, IMapper mapper, INotyfService notyf, AssignmentRepository assignmentRepository)
        {
            _lessonRepository = lessonRepository;
            _gradeRepository = gradeRepository;
            _assignmentRepository = assignmentRepository;
            _mapper = mapper;
            _notyf = notyf;
        }

        public async Task<IActionResult>Index()
        {
            var lessons = await _lessonRepository.GetAllAsync();
            var lessonModels = _mapper.Map<List<LessonModel>>(lessons);
            return View(lessonModels);
        }

        public async Task<IActionResult> Add()
        {
            var grades = await _gradeRepository.GetAllAsync();
            var gradesSelectList = grades.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.Grades = gradesSelectList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(LessonModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var lesson = _mapper.Map<Lesson>(model);
            lesson.Created = DateTime.Now;
            lesson.Updated = DateTime.Now;
            await _lessonRepository.AddAsync(lesson);
            _notyf.Success("Ders Eklendi...");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var grades = await _gradeRepository.GetAllAsync();
            var gradesSelectList = grades.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.Grades = gradesSelectList;
            var lesson = await _lessonRepository.GetByIdAsync(id);
            var lessonModel = _mapper.Map<LessonModel>(lesson);
            return View(lessonModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(LessonModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var lesson = await _lessonRepository.GetByIdAsync(model.Id);
            lesson.Name = model.Name;
            lesson.IsActive = model.IsActive;
            lesson.GradeId = model.GradeId;
            lesson.Updated = DateTime.Now;
            await _lessonRepository.UpdateAsync(lesson);
            _notyf.Success("Ders Güncellendi...");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            var lessonModel = _mapper.Map<LessonModel>(lesson);
            return View(lessonModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(LessonModel model)
        {
            var assignments = await _assignmentRepository.GetAllAsync();
            if (assignments.Count(c => c.LessonId == model.Id) > 0)
            {
                _notyf.Error("Üzerinde Ödev Kayıtlı Olan Ders Silinemez!");
                return RedirectToAction("Index");
            }
            await _lessonRepository.DeleteAsync(model.Id);
            _notyf.Success("Ders Silindi...");
            return RedirectToAction("Index");
        }
    }
}
