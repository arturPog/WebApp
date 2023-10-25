using WebApp.Models;

namespace WebApp.Services
{
    public interface ISliderItemService : ICrudService<SliderItem>
    {
        Task AddSliderAsync(SliderItem slider, IFormFile imageFile);
        Task UpdateSliderAsync(SliderItem slider, IFormFile imageFile);
        Task DeleteSliderAsync(int id);
        Task<SliderItem> GetSliderFirstAsync();
        //Task<IEnumerable<SliderItem>> GetAllSliderAsync();
        Task<IEnumerable<SliderItem>> GetAllActiveSliderAsync();
    }
}
