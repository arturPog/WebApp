using WebApp.Models;

namespace WebApp.Services
{
    public interface IOrderDetailService : ICrudService<OrderDetail>
    {
        
        Task AddOrderDetaiolAsync(int orderId);
        Task<IEnumerable<OrderDetail>> GetOrderDetaiolByOrderIdAsync(int orderId);
    }
}
