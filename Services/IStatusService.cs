using WebApp.Models;

namespace WebApp.Services
{
    public interface IStatusService : ICrudService<Status>
    {
        Task AddStatusAsync(Status status);
        Task UpdateStatusAsync(Status status);
        Task DeleteStatusAsync(int id);
        Task<IEnumerable<Status>>GetAllStatusAsync();
        Task<Status> GetStatusByIdAsync(int id);
    }
}
