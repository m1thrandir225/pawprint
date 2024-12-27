namespace Repository.Interface;

using System.Collections.Generic;

public interface ICrudRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsync(Guid? id);
    Task InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}