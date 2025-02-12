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
    
    public async Task<IEnumerable<T>> GetAll()
    {
        return await _entities.ToListAsync();
    }

    public async Task<T?> Get(Guid? id)
    {
        return await _entities.FindAsync(id);
    }

    public async Task<T> Insert(T entity)
    {
        await _entities.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<T> Update(T entity)
    {
        _entities.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(T entity)
    {
        _entities.Remove(entity);
        await _context.SaveChangesAsync();
    }
}