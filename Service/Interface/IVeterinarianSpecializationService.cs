using Domain;
using Domain.DTOs;

namespace Service.Interface;

public interface IVeterinarianSpecializationService : ICRUDService<VeterinarianSpecilization, CreateVeterinarianSpecializationRequest, UpdateVeterinarianSpecializationRequest>
{
}