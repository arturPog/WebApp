using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService) 
        {
            _logger = logger;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index(string categoryName)
        {
            var category = await _categoryService.GetCategoryByNameAsync(categoryName);

            var vm = new PageViewModel();
            vm.CurrentCategory = category;
            vm.CategoryId = category.CategoryId;
            vm.CategoryNameUrl = categoryName;
            vm.Menu = await _categoryService.GetCategoryInMenuAsync();
            return View(vm);
        }
    }
}
