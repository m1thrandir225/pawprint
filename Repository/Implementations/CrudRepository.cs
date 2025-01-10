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
    
    public IEnumerable<T> GetAll()
    {
        return _entities.ToList(); // Synchronous method
    }

    public T? Get(Guid? id)
    {
        return _entities.Find(id); // Synchronous method
    }

    public T Insert(T entity)
    {
        _entities.Add(entity); // Synchronous method
        _context.SaveChanges(); // Synchronous method

        return entity;
    }

    public T Update(T entity)
    {
        _entities.Update(entity); // Synchronous method
        _context.SaveChanges(); // Synchronous method
        return entity;
    }

    public void Delete(T entity)
    {
        _entities.Remove(entity); // Synchronous method
        _context.SaveChanges(); // Synchronous method
    }
}