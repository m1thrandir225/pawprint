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
        return _entities.AsEnumerable();
    }

    public T Get(Guid? id)
    {
        return _entities.Find(id);
    }

    public void Insert(T entity)
    {
        _entities.Add(entity);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _entities.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        _entities.Remove(entity);
        _context.SaveChanges();
    }
}

