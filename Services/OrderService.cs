using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Services
{
    public class OrderService : CrudService<Order>, IOrderService
    {
        public OrderService(AppDbContext context, ILogger<CrudService<Order>> logger)
           : base(context, logger)
        {
           
        }



        // Dodawanie nowego zamówienia
        public async Task AddOrderAsync(Order order)
        {
            try
            {
                // Możliwe dodatkowe weryfikacje przed dodaniem zamówienia
                await AddAsync(order);
                _logger.LogInformation("Zamówienie zostało pomyślnie dodane.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas dodawania zamówienia.");
                throw;
            }
        }

        // Zmiana statusu istniejącego zamówienia
        public async Task ChangeStatusAsync(int orderId, Status newStatus)
        {
            try
            {
                var order = await GetByIdAsync(orderId);
                if (order == null)
                {
                    throw new InvalidOperationException($"Zamówienie o ID {orderId} nie zostało znalezione.");
                }

                order.Status = newStatus;
                await UpdateAsync(order);
                _logger.LogInformation($"Status zamówienia {orderId} został zmieniony na {newStatus}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Błąd podczas zmiany statusu zamówienia {orderId}.");
                throw;
            }
        }
        public async Task<Order> GeOrderWithDetailsByIdAsync(int id) 
        {
            var order = await _context.Orders.Include(x => x.OrderDetails).Where(x => x.OrderId == id).FirstOrDefaultAsync();
            if (order == null)
            {
                throw new InvalidOperationException($"Nie ma zamówienia dla ID {id} .");
            }

            return order;
        }
        public async Task<IEnumerable<Order>> GetAllOrderWithDetailsAsync() 
        {
            var orders = await _context.Orders.Include(x => x.OrderDetails).ToListAsync();
            if (orders == null)
            {
                throw new InvalidOperationException($"Nie ma żadnych zamówień.");
            }

            return orders;
        }
        public async Task<IEnumerable<Order>> GetAllOrderByClientAsync(int clientId) 
        {
            var orders = await _context.Orders.Include(x=>x.OrderDetails).Where(x => x.ClientId == clientId).ToListAsync();
            if (orders == null) 
            {
                throw new InvalidOperationException($"Nie ma zamówień dla klienta o ID {clientId} .");
            }

            return orders;
        }
    }
}
