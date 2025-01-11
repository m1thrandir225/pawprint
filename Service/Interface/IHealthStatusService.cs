using Domain;
using Domain.DTOs;

namespace Service.Interface;

public interface IHealthStatusService : ICRUDService<HealthStatus, CreateHealthStatusRequest, UpdateHealthStatusRequest>
{

}