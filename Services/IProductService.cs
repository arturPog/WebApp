using WebApp.Models;

namespace WebApp.Services
{
    public interface IProductService : ICrudService<Product>
    {
        Task<IEnumerable<Product>> GetAllAsync();
       // Task<IEnumerable<Product>> GetAllProductAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddProductAsync(Product product, IFormFile imageFile);
        Task UpdateProductAsync(Product product, IFormFile imageFile);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Product>> GetProductsOnFirstPageAsync();
    }
}
