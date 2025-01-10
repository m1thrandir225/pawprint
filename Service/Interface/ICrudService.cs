using Repository.Interface;

namespace Service.Interface;

using System.Collections.Generic;

public interface ICrudService<Model> where Model : class
{
    IEnumerable<Model> GetAll();
    Model Get(Guid? id);
    void Insert(Model entity);
    void Update(Model entity);
    void Delete(Model entity);
}
