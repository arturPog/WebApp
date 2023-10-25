using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Services
{
    public class StatusService : CrudService<Status>, IStatusService
    {
        public StatusService(AppDbContext context, ILogger<CrudService<Status>> logger)
         : base(context, logger)
        {

        }

        public async Task AddStatusAsync(Status status) 
        {
            await AddAsync(status);
        }
        public async Task UpdateStatusAsync(Status status) 
        {
            await UpdateAsync(status);
        }
        public async Task DeleteStatusAsync(int id) 
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
                throw new InvalidOperationException($"Status o ID {id} nie został znaleziony.");

            await DeleteAsync(status);
        }
        public async Task<IEnumerable<Status>> GetAllStatusAsync() 
        {
            var statuses = await _context.Statuses.ToListAsync();
            if (statuses == null || statuses.Count() == 0)
                throw new InvalidOperationException($"Nie ma żadnych statusów.");

            return statuses;
        }
        public async Task<Status> GetStatusByIdAsync(int id) 
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
                throw new InvalidOperationException($"Status o ID {id} nie został znaleziony.");
            return status;
        }
    }
}
