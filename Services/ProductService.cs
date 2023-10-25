using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Services
{
    public class ProductService : CrudService<Product>, IProductService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        public ProductService(AppDbContext context, ILogger<CrudService<Product>> logger, IWebHostEnvironment webHostEnvironment)
            : base(context, logger) 
        {
            _webHostEnvironment = webHostEnvironment;
        }

       


        public async Task AddProductAsync(Product product, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                product.ImageUrl = await SaveImageAsync(imageFile);
            }

            await AddAsync(product);
        }

        public async Task UpdateProductAsync(Product product, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                product.ImageUrl = await SaveImageAsync(imageFile);
            }

            await UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                throw new InvalidOperationException($"Produkt o ID {id} nie został znaleziony.");

            await DeleteAsync(product);
        }

        
        //     public async Task<IEnumerable<Product>> GetAllProductAsync()
        //{
        //    return await _context.Products.ToListAsync();
        //}

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {

            try
            { 
                var products = await _context.Products
                                 .Where(p => p.CategoryId == categoryId)
                                 .ToListAsync();
                return products;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Błąd podczas pobierania produktów.");
                throw;  // Rzucanie wyjątku dalej po zalogowaniu błędu
            }
               
        }

        public async Task<IEnumerable<Product>> GetProductsOnFirstPageAsync()
        {
           

            try
            {
                var products = await _context.Products
                                 .Where(p => p.IsFirstPage)
                                 .ToListAsync();
                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas pobierania produktów.");
                throw;  // Rzucanie wyjątku dalej po zalogowaniu błędu
            }

        }
        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            try
            {
                // Tworzenie nazwy pliku (unikalnej, aby uniknąć konfliktów)
                var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

                // Tworzenie folderu images, jeśli nie istnieje
                var directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Zapisywanie pliku
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                // Zwracanie URL do pliku
                return $"/images/{fileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas zapisywania obrazu w ProductService.");
                throw;  // Rzucanie wyjątku dalej po zalogowaniu błędu
            }
        }
    }
}
