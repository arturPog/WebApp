using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Services
{
    public class ClientService : CrudService<Client>, IClientService
    {
        public ClientService(AppDbContext context, ILogger<CrudService<Client>> logger)
          : base(context, logger)
        {
           
        }
        public async Task<Client> GetAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
                throw new InvalidOperationException($"Klient o ID {id} nie został znaleziony.");

            
            return client;
        }
        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            var clients = await _context.Clients.ToListAsync();
            if (clients == null || clients.Count() == 0)
                throw new InvalidOperationException($"Nie ma żadnych klientów.");

           return clients;
        }
        public async Task AddClientAsync(Client client) 
        {
            await AddAsync(client);
        }
        public async Task UpdateClientAsync(Client client) 
        {
            await UpdateAsync(client);
        }
    }
}
