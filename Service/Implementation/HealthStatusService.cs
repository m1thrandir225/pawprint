using Domain;
using Domain.DTOs;
using Domain.DTOs.HealthStatus;
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
        return await _repository.GetAll();
    }

    public async Task<HealthStatus> GetByIdAsync(Guid id)
    {
        return await _repository.Get(id);
    }

    public async Task<HealthStatus> CreateAsync(CreateHealthStatusRequest dto)
    {
        var healthStatus = new HealthStatus(dto.Name);
        
        return await _repository.Insert(healthStatus);
    }

    public async Task<HealthStatus> UpdateAsync(Guid id, UpdateHealthStatusRequest dto)
    {
        var healthStatus = await _repository.Get(id);

        if (healthStatus == null)
        {
            return null;
        }
        
        healthStatus.Name = dto.Name;
        
        return await _repository.Update(healthStatus);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var healthStatus = await _repository.Get(id);
        
        await _repository.Delete(healthStatus);
        return true;
    }
}