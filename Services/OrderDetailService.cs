using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Services
{
    public class OrderDetailService : CrudService<OrderDetail>, IOrderDetailService
    {
        public OrderDetailService(AppDbContext context, ILogger<CrudService<OrderDetail>> logger)
           : base(context, logger)
        {

        }

        public async Task AddOrderDetaiolAsync(int orderId) 
        {

        }
        public async Task<IEnumerable<OrderDetail>> GetOrderDetaiolByOrderIdAsync(int orderId) 
        {
            var orderDetails = await _context.OrderDetails.Where(x => x.OrderId == orderId).ToListAsync();
            if (orderDetails == null)
            {
                throw new InvalidOperationException($"Nie ma produktów dla zamówienia o ID {orderId} .");
            }

            return orderDetails;
        }
    }
}
