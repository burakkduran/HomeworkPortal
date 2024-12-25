﻿using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using HomeworkPortal.Models;
using HomeworkPortal.Repositories;
using HomeworkPortal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HomeworkPortal.Controllers
{
    [Authorize]

    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly LessonRepository _lessonRepository;
        private readonly INotyfService _notyf;
        private readonly IMapper _mapper;

        public CategoryController(CategoryRepository categoryRepository, INotyfService notyf, LessonRepository productRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _notyf = notyf;
            _lessonRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryModels = _mapper.Map<List<CategoryModel>>(categories);
            return View(categoryModels);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var category = _mapper.Map<Category>(model);
            category.Created = DateTime.Now;
            category.Updated = DateTime.Now;
            await _categoryRepository.AddAsync(category);
            _notyf.Success("Sınıf Eklendi...");
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            var categoryModel = _mapper.Map<CategoryModel>(category);
            return View(categoryModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var category = await _categoryRepository.GetByIdAsync(model.Id);
            category.Name = model.Name;
            category.IsActive = model.IsActive;
            category.Updated = DateTime.Now;
            await _categoryRepository.UpdateAsync(category);
            _notyf.Success("Sınıf Güncellendi...");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            var categoryModel = _mapper.Map<CategoryModel>(category);
            return View(categoryModel);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(CategoryModel model)
        {

            var lessons = await _lessonRepository.GetAllAsync();
            if (lessons.Count(c => c.CategoryId == model.Id) > 0)
            {
                _notyf.Error("Üzerinde Ders Kayıtlı Olan Sınıf Silinemez!");
                return RedirectToAction("Index");
            }

            await _categoryRepository.DeleteAsync(model.Id);
            _notyf.Success("Sınıf Silindi...");
            return RedirectToAction("Index");

        }
    }
}