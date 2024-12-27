namespace Repository.Interface;

using System.Collections.Generic;

public interface ICrudRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T Get(Guid? id);
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);
}