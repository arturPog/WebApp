using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.Helpers;
using WebApp.Models;
using WebApp.Services;
using static System.Net.Mime.MediaTypeNames;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService) 
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
            
        }
        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Przygotowanie SelectList dla kategorii
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategoryIsProductPageAsync(), "CategoryId", "Nazwa");
            return View(new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile image)
        {
            //if (ModelState.IsValid)
            //{
            var category = await _categoryService.GetByIdAsync(product.CategoryId);
                product.NazwaUrl = Link.NameUrl(product.Nazwa);
            product.CategoryName = category.Nazwa;
            

                await _productService.AddProductAsync(product, image);
                return RedirectToAction(nameof(Index)); // Przekieruj do listy produktów
            //}

            //// W przypadku błędu, ponownie przygotuj SelectList
            //ViewBag.CategoryId = new SelectList(await _categoryService.GetCategoryIsProductPageAsync(), "CategoryId", "Nazwa", product.CategoryId);
            //return View(product);
        }

        // Metoda do wyświetlenia formularza edycji produktu
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategoryIsProductPageAsync(), "CategoryId", "Nazwa", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile image)
        {
            if (id != product.ProduktId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
               
                try
                {
                var category = await _categoryService.GetByIdAsync(product.CategoryId);
                
                product.CategoryName = category.Nazwa;

                product.NazwaUrl = Link.NameUrl(product.Nazwa);
                    await _productService.UpdateProductAsync(product, image);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductExists(product.ProduktId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index)); // Przekieruj do listy produktów
           // }

            //// W przypadku błędu, ponownie przygotuj SelectList
            //ViewBag.CategoryId = new SelectList(await _categoryService.GetCategoryIsProductPageAsync(), "CategoryId", "Nazwa", product.CategoryId);
            //return View(product);
        }
        private async Task<bool> ProductExists(int id)
        {
            return await _productService.GetByIdAsync(id) != null;
        }
    }
}
