using Microsoft.AspNetCore.Mvc;
using HomeworkPortal.Repositories;
using HomeworkPortal.Models;
using HomeworkPortal.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeworkPortal.Controllers
{
    public class LessonController : Controller
    {
        private readonly LessonRepository _lessonRepository;
        private readonly CategoryRepository _categoryRepository;

        public LessonController(LessonRepository lessonRepository, CategoryRepository categoryRepository)
        {
            _lessonRepository = lessonRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var lessons = _lessonRepository.GetList();
            return View(lessons);
        }

        public IActionResult Add()
        {
            var categories = _categoryRepository.GetList().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public IActionResult Add(LessonModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _lessonRepository.Add(model);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var categories = _categoryRepository.GetList().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.Categories = categories;
            var lesson = _lessonRepository.GetById(id);
            return View(lesson);
        }

        [HttpPost]
        public IActionResult Update(LessonModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _lessonRepository.Update(model);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var lesson = _lessonRepository.GetById(id);
            return View(lesson);
        }

        [HttpPost]
        public IActionResult Delete(LessonModel model)
        {
            _lessonRepository.Delete(model.Id);
            return RedirectToAction("Index");
        }
    }
}
