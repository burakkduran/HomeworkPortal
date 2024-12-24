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
    [Authorize]

    public class LessonController : Controller
    {
        private readonly LessonRepository _lessonRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;

        public LessonController(LessonRepository lessonRepository, CategoryRepository categoryRepository, IMapper mapper, INotyfService notyf)
        {
            _lessonRepository = lessonRepository;
            _categoryRepository = categoryRepository;
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
            var categories = await _categoryRepository.GetAllAsync();
            var categoriesSelectList = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.Categories = categoriesSelectList;
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
            var categories = await _categoryRepository.GetAllAsync();
            var categoriesSelectList = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.Categories = categoriesSelectList;
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
            lesson.CategoryId = model.CategoryId;
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
            await _lessonRepository.DeleteAsync(model.Id);
            _notyf.Success("Ders Silindi...");
            return RedirectToAction("Index");
        }
    }
}
