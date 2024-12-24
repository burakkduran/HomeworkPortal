using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using HomeworkPortal.Models;
using HomeworkPortal.Repositories;
using HomeworkPortal.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace HomeworkPortal.Controllers
{
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
            await _categoryRepository.AddAsync(category);
            _notyf.Success("Kategori Eklendi...");
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
            await _categoryRepository.UpdateAsync(category);
            _notyf.Success("Kategori Güncellendi...");
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

            var products = await _lessonRepository.GetAllAsync();
            if (products.Count(c => c.CategoryId == model.Id) > 0)
            {
                _notyf.Error("Üzerinde Ürün Kayıtlı Olan Kategori Silinemez!");
                return RedirectToAction("Index");
            }

            await _categoryRepository.DeleteAsync(model.Id);
            _notyf.Success("Kategori Silindi...");
            return RedirectToAction("Index");

        }
    }
}