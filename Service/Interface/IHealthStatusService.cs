using Domain;
using Domain.DTOs;
using Domain.DTOs.HealthStatus;

namespace Service.Interface;

public interface IHealthStatusService : ICRUDService<HealthStatus, CreateHealthStatusRequest, UpdateHealthStatusRequest>
{

}