using WebApp.Models;

namespace WebApp.Services
{
    public interface IClientService : ICrudService<Client>
    {
        Task<Client> GetAsync(int id);
        Task<IEnumerable<Client>> GetAllAsync();
        Task AddClientAsync(Client client);
        Task UpdateClientAsync(Client client);
    }
}
