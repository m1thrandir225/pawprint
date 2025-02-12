using Domain;
using Domain.DTOs;
using Domain.DTOs.VeterinarianSpecialization;

namespace Service.Interface;

public interface IVeterinarianSpecializationService : ICRUDService<VeterinarianSpecilization, CreateVeterinarianSpecializationRequest, UpdateVeterinarianSpecializationRequest>
{
}