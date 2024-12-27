using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Implementations;

public class CrudRepository<T> : ICrudRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private DbSet<T> _entities;

    public CrudRepository(ApplicationDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _entities.ToListAsync();
    }

    public async Task<T> GetAsync(Guid? id)
    {
        return await _entities.FindAsync(id);
    }

    public async Task InsertAsync(T entity)
    {
        await _entities.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _entities.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _entities.Remove(entity);
        await _context.SaveChangesAsync();
    }
}