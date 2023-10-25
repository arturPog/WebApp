using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISliderItemService _sliderService;
        public HomeController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService, ISliderItemService sliderService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new PageViewModel();
            vm.SliderItems = await _sliderService.GetAllActiveSliderAsync();
            vm.FirstPageCategories = await _categoryService.GetCategoryOnFirstPageAsync();
            vm.FirstPageProducts = await _productService.GetProductsOnFirstPageAsync();
            vm.Menu = await _categoryService.GetCategoryInMenuAsync();
            return View(vm);
        }

        public async Task<IActionResult> Privacy()
        {
            var vm = new PageViewModel();
           
            vm.Menu = await _categoryService.GetCategoryInMenuAsync();
            return View(vm);
           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
