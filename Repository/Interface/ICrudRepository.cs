namespace Repository.Interface;

using System.Collections.Generic;

public interface ICrudRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> Get(Guid? id);
    Task<T> Insert(T entity);
    Task<T> Update(T entity);
    Task Delete(T entity);
}