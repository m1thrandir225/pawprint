using Repository.Interface;

namespace Service.Interface;

using System.Collections.Generic;

public interface ICRUDService<TEntity,TCreateDto, TUpdateDto> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(Guid id);
    Task<TEntity> CreateAsync(TCreateDto dto);
    Task<TEntity> UpdateAsync(Guid id, TUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}
