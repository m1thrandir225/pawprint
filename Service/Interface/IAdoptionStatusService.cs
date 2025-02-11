using Domain.DTOs.AdoptionStatus;

namespace Service.Interface;

using Domain;
using Domain.DTOs;

public interface IAdoptionStatusService : ICRUDService<AdoptionStatus, CreateAdoptionStatusRequest, UpdateAdoptionStatusRequest>
{
    public Task<AdoptionStatus> GetAdoptionStatusByName(string name);
}