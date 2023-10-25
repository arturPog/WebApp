using WebApp.Models;

namespace WebApp.Services
{
    public interface IOrderService : ICrudService<Order>
    {
        Task<Order> GeOrderWithDetailsByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllOrderWithDetailsAsync();
        Task<IEnumerable<Order>> GetAllOrderByClientAsync(int clientId);
        Task AddOrderAsync(Order order);
        Task ChangeStatusAsync(int orderId, Status status);
    }
}
