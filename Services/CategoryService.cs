using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Services
{
    public class CategoryService: CrudService<Category>, ICategoryService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
       
        public CategoryService(AppDbContext context, ILogger<CrudService<Category>> logger, IWebHostEnvironment webHostEnvironment)
           : base(context, logger) 
        {
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<Category> GetCategoryByIdAsync(int id) 
        {
            var category = await GetByIdAsync(id);

            return category;
        }
        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            var category = await _context.Categories.Where(x=>x.NazwaUrl == name).FirstOrDefaultAsync();

            return category;
        }
        public async Task AddCategoryAsync(Category category, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                category.ImageUrl = await SaveImageAsync(imageFile);
            }

            await AddAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                category.ImageUrl = await SaveImageAsync(imageFile);
            }

            await UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                throw new InvalidOperationException($"Kategoria o ID {id} nie został znaleziony.");

            await DeleteAsync(category);
        }

        public async Task<IEnumerable<Category>> GetCategoryInMenuAsync()
        {
            return await _context.Categories
                                 .Where(p => p.IsMenu)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoryOnFirstPageAsync()
        {
            return await _context.Categories
                                 .Where(p => p.IsFirstPage)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoryIsProductPageAsync()
        {
            return await _context.Categories
                                 .Where(p => p.IsProduct)
                                 .ToListAsync();
        }
        public async Task<IEnumerable<Category>> GetCategoryIsInfoPageAsync()
        {
            return await _context.Categories
                                 .Where(p => p.IsInfo)
                                 .ToListAsync();
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
                _logger.LogError(ex, "Błąd podczas zapisywania obrazu w CategoryService.");
                throw;  // Rzucanie wyjątku dalej po zalogowaniu błędu
            }
        }
    }
}

