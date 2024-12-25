﻿using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using HomeworkPortal.Models;
using HomeworkPortal.Repositories;
using HomeworkPortal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HomeworkPortal.Controllers
{
    [Authorize(Roles = "Admin")]

    public class GradeController : Controller
    {
        private readonly GradeRepository _gradeRepository;
        private readonly LessonRepository _lessonRepository;
        private readonly INotyfService _notyf;
        private readonly IMapper _mapper;

        public GradeController(GradeRepository gradeRepository, INotyfService notyf, LessonRepository productRepository, IMapper mapper)
        {
            _gradeRepository = gradeRepository;
            _notyf = notyf;
            _lessonRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _gradeRepository.GetAllAsync();
            var gradeModels = _mapper.Map<List<GradeModel>>(categories);
            return View(gradeModels);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(GradeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var grade = _mapper.Map<Grade>(model);
            grade.Created = DateTime.Now;
            grade.Updated = DateTime.Now;
            await _gradeRepository.AddAsync(grade);
            _notyf.Success("Sınıf Eklendi...");
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Update(int id)
        {
            var grade = await _gradeRepository.GetByIdAsync(id);
            var gradeModel = _mapper.Map<GradeModel>(grade);
            return View(gradeModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(GradeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var grade = await _gradeRepository.GetByIdAsync(model.Id);
            grade.Name = model.Name;
            grade.IsActive = model.IsActive;
            grade.Updated = DateTime.Now;
            await _gradeRepository.UpdateAsync(grade);
            _notyf.Success("Sınıf Güncellendi...");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var grade = await _gradeRepository.GetByIdAsync(id);
            var gradeModel = _mapper.Map<GradeModel>(grade);
            return View(gradeModel);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(GradeModel model)
        {

            var lessons = await _lessonRepository.GetAllAsync();
            if (lessons.Count(c => c.GradeId == model.Id) > 0)
            {
                _notyf.Error("Üzerinde Ders Kayıtlı Olan Sınıf Silinemez!");
                return RedirectToAction("Index");
            }

            await _gradeRepository.DeleteAsync(model.Id);
            _notyf.Success("Sınıf Silindi...");
            return RedirectToAction("Index");

        }
    }
}