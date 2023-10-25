using WebApp.Models;

namespace WebApp.Services
{
    public interface ICategoryService: ICrudService<Category>
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> GetCategoryByNameAsync(string name);
        Task AddCategoryAsync(Category category, IFormFile imageFile);
        Task UpdateCategoryAsync(Category category, IFormFile imageFile);
        Task DeleteCategoryAsync(int id);
        Task<IEnumerable<Category>> GetCategoryInMenuAsync();
        Task<IEnumerable<Category>> GetCategoryOnFirstPageAsync();
        Task<IEnumerable<Category>> GetCategoryIsProductPageAsync();
        Task<IEnumerable<Category>> GetCategoryIsInfoPageAsync();

    }
}
