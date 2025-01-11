using Domain;
using Domain.DTOs;

namespace Service.Interface;

public interface IVaccinationService : ICRUDService<Vaccination, CreateVaccinationRequest, UpdateVaccinationRequest>
{
    
}