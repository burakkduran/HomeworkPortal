using Microsoft.AspNetCore.Mvc;
using HomeworkPortal.Repositories;
using HomeworkPortal.Models;
using HomeworkPortal.ViewModels;

namespace HomeworkPortal.Controllers
{
    public class LessonController : Controller
    {
        private readonly LessonRepository _lessonRepository;

        public LessonController(LessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public IActionResult Index()
        {
            var lessons = _lessonRepository.GetList();
            return View(lessons);
        }

        public IActionResult Add()
        {
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
