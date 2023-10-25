using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Services
{
    public class SliderItemService : CrudService<SliderItem>, ISliderItemService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SliderItemService(AppDbContext context, ILogger<CrudService<SliderItem>> logger, IWebHostEnvironment webHostEnvironment)
        : base(context, logger)
        {
            _webHostEnvironment = webHostEnvironment;   
        }


        public async Task AddSliderAsync(SliderItem slider, IFormFile imageFile) 
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                slider.ImageUrl = await SaveImageAsync(imageFile);
            }
            await AddAsync(slider);
        }
        public async Task UpdateSliderAsync(SliderItem slider, IFormFile imageFile) 
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                slider.ImageUrl = await SaveImageAsync(imageFile);
            }
            await UpdateAsync(slider);
        }
        public async Task DeleteSliderAsync(int id) 
        {
            var slider = await _context.SliderItems.FindAsync(id);
            if (slider == null)
                throw new InvalidOperationException($"Slider o ID {id} nie został znaleziony.");

            await DeleteAsync(slider);
        }
        public async Task<SliderItem> GetSliderFirstAsync() 
        {
            var slider = await _context.SliderItems.FirstOrDefaultAsync();
            if (slider == null)
                throw new InvalidOperationException($"Żaden Slider nie został znaleziony.");

            return slider;
        }
        //public async Task<IEnumerable<SliderItem>> GetAllSliderAsync() 
        //{

        //}
        public async Task<IEnumerable<SliderItem>> GetAllActiveSliderAsync() 
        {
            try
            {
                var sliders = await _context.SliderItems.Where(x => x.IsActive).ToListAsync();
                return sliders;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Błąd podczas pobierania Slidera.");
                throw;
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
                _logger.LogError(ex, "Błąd podczas zapisywania obrazu w Sliderze.");
                throw;  
            }
        }
    }
}
