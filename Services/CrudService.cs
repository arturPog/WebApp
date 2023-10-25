using Microsoft.EntityFrameworkCore;
using WebApp.Data;

namespace WebApp.Services
{
    public class CrudService<T> : ICrudService<T> where T : class
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _entities;
        protected readonly ILogger<CrudService<T>> _logger;

        public CrudService(AppDbContext context, ILogger<CrudService<T>> logger)
        {
            _context = context;
            _entities = context.Set<T>();
            _logger = logger;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {

            try
            {
                return await _entities.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Wystąpił błąd podczas pobierania wszystkich encji typu {typeof(T).Name}.");
               
                throw; 
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            
            try
            {
                return await _entities.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Wystąpił błąd podczas pobierania encji typu {typeof(T).Name} o id {id}.");

                throw;
            }
        }

        public async Task AddAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                await _entities.AddAsync(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Encja typu {typeof(T).Name} została pomyślnie dodana.");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Błąd podczas dodawania encji typu {typeof(T).Name}.");

                throw;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _entities.Update(entity);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                _logger.LogInformation($"Encja typu {typeof(T).Name} została pomyślnie aktualizowana.");
            }
            catch (Exception ex)
            {
                // Wycofanie transakcji
                await transaction.RollbackAsync();

                _logger.LogError(ex, $"Błąd podczas aktualizacji encji typu {typeof(T).Name}.");

                throw;
            }
        }

       
        public async Task DeleteAsync(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Encja typu {typeof(T).Name} została pomyślnie usunięta.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Błąd podczas usuwania encji typu {typeof(T).Name}.");
                throw;
            }
        }
    }
}
