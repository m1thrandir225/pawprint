using Domain;
using Domain.DTOs;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class HealthStatusService : IHealthStatusService
{
    private readonly IHealthStatusRepository _repository;

    public HealthStatusService(IHealthStatusRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<HealthStatus>> GetAllAsync()
    {
        return _repository.GetAll();
    }

    public async Task<HealthStatus> GetByIdAsync(Guid id)
    {
        return _repository.Get(id);
    }

    public async Task<HealthStatus> CreateAsync(CreateHealthStatusRequest dto)
    {
        var healthStatus = new HealthStatus(dto.Name);
        
        return _repository.Insert(healthStatus);
    }

    public async Task<HealthStatus> UpdateAsync(Guid id, UpdateHealthStatusRequest dto)
    {
        var healthStatus = _repository.Get(id);

        if (healthStatus == null)
        {
            return null;
        }
        
        healthStatus.Name = dto.Name;
        
        return _repository.Update(healthStatus);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var healthStatus = _repository.Get(id);
        
        _repository.Delete(healthStatus);
        return Task.FromResult(true);
    }
}