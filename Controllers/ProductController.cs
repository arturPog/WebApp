using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public ProductController(ILogger<ProductController> logger, ICategoryService categoryService, IProductService productService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _productService = productService;   
        }
        public async Task<IActionResult> Index(string categoryName, int categoryId)
        {
            var category = await _categoryService.GetCategoryByNameAsync(categoryName);

            var vm = new PageViewModel();
            vm.ProductsList = await _productService.GetProductsByCategoryIdAsync(categoryId);
            vm.CurrentCategory = category;
            vm.CategoryId = categoryId;
            vm.CategoryNameUrl = categoryName;
            vm.Menu = await _categoryService.GetCategoryInMenuAsync();
            return View(vm);
        }
        public async Task<IActionResult> Detail(string categoryName, string productName, int productId)
        {
            var category = await _categoryService.GetCategoryByNameAsync(categoryName);

            var vm = new PageViewModel();
            vm.CurrentProduct= await _productService.GetByIdAsync(productId);
            vm.CurrentCategory = category;
            vm.CategoryId = category.CategoryId;
            vm.CategoryNameUrl = categoryName;
            vm.ProductNameUrl = productName;
            vm.Menu = await _categoryService.GetCategoryInMenuAsync();
            return View(vm);
        }
    }
}
