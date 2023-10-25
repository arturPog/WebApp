using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.Helpers;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) 
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        // Akcja do wyświetlenia formularza dodawania nowej kategorii
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Category category, IFormFile? image)
        {
            
                category.NazwaUrl = Link.NameUrl(category.Nazwa);
                await _categoryService.AddCategoryAsync(category, image);
                return RedirectToAction(nameof(Index)); 
            
            return View(category);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategoryByIdAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category, IFormFile? image)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

           
                try
                {
                    category.NazwaUrl = Link.NameUrl(category.Nazwa);
                    await _categoryService.UpdateCategoryAsync(category, image);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // Zakładając, że istnieje akcja Index
           
            return View(category);
        }

        private async Task<bool> CategoryExists(int id)
        {
            return await _categoryService.GetCategoryByIdAsync(id) != null;
        }
    }
}
