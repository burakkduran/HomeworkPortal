using Microsoft.AspNetCore.Mvc;
using HomeworkPortal.Repositories;
using HomeworkPortal.Models;
using HomeworkPortal.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using HomeworkPortal.Hubs;

namespace HomeworkPortal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AssignmentController : Controller
    {
        private readonly AssignmentRepository _assignmentRepository;
        private readonly LessonRepository _lessonRepository;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;
        private readonly IHubContext<GeneralHub> _generalHub;

        public AssignmentController(AssignmentRepository assignmentRepository,
            LessonRepository lessonRepository,
            IMapper mapper,
            INotyfService notyf,
            IHubContext<GeneralHub> generalHub)
        {
            _assignmentRepository = assignmentRepository;
            _lessonRepository = lessonRepository;
            _mapper = mapper;
            _notyf = notyf;
            _generalHub = generalHub;
        }

        public async Task<IActionResult> Index()
        {
            var assignments = await _assignmentRepository.GetAllAsync();
            var assignmentModels = _mapper.Map<List<AssignmentModel>>(assignments);

            var lessons = await _lessonRepository.GetAllAsync();
            ViewBag.Lessons = lessons.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return View(assignmentModels);
        }

        [HttpPost]
        public async Task<IActionResult> AddAjax([FromBody] AssignmentModel model)
        {
            if (!ModelState.IsValid)
            {
                _notyf.Error("Geçersiz veri!");
                return Json(new { success = false, message = "Geçersiz veri!" });
            }

            var assignment = _mapper.Map<Assignment>(model);
            assignment.Created = DateTime.Now;
            assignment.Updated = DateTime.Now;
            await _assignmentRepository.AddAsync(assignment);
            int assignmentCount = _assignmentRepository.Where(c => c.IsActive == true).Count();
            await _generalHub.Clients.All.SendAsync("onAssignmentAdd", assignmentCount);

            _notyf.Success("Ödev başarıyla eklendi.");
            return Json(new { success = true, message = "Ödev başarıyla eklendi.", id = assignment.Id });
        }

        public async Task<IActionResult> Update(int id)
        {
            var lessons = await _lessonRepository.GetAllAsync();
            var lessonsSelectList = lessons.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.Lessons = lessonsSelectList;

            var assignment = await _assignmentRepository.GetByIdAsync(id);
            var assignmentModel = _mapper.Map<AssignmentModel>(assignment);
            return View(assignmentModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AssignmentModel model)
        {
            if (!ModelState.IsValid)
            {
                var lessons = await _lessonRepository.GetAllAsync();
                ViewBag.Lessons = lessons.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
                return View(model);
            }

            var assignment = await _assignmentRepository.GetByIdAsync(model.Id);
            assignment.Name = model.Name;
            assignment.Description = model.Description;
            assignment.LessonId = model.LessonId;
            assignment.DueDate = model.DueDate;
            assignment.IsActive = model.IsActive;
            assignment.Updated = DateTime.Now;

            await _assignmentRepository.UpdateAsync(assignment);
            int assignmentCount = _assignmentRepository.Where(c => c.IsActive == true).Count();
            await _generalHub.Clients.All.SendAsync("onAssignmentUpdate", assignmentCount);
            _notyf.Success("Ödev Güncellendi...");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var assignment = await _assignmentRepository.GetByIdAsync(id);
            var assignmentModel = _mapper.Map<AssignmentModel>(assignment);
            return View(assignmentModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AssignmentModel model)
        {
            await _assignmentRepository.DeleteAsync(model.Id);
            int assignmentCount = _assignmentRepository.Where(c => c.IsActive == true).Count();
            await _generalHub.Clients.All.SendAsync("onAssignmentDelete", assignmentCount);
            _notyf.Success("Ödev Silindi...");
            return RedirectToAction("Index");
        }
    }
}