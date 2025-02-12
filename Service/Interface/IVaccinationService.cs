using Domain;
using Domain.DTOs;
using Domain.DTOs.Vaccination;

namespace Service.Interface;

public interface IVaccinationService : ICRUDService<Vaccination, CreateVaccinationRequest, UpdateVaccinationRequest>
{
    
}