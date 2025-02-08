using Domain;
using Domain.DTOs;
using Domain.DTOs.Veterinarian;

namespace Service.Interface;

public interface IVeterinarianService : ICRUDService<Veterinarian, CreateVeterinarianRequest, UpdateVeterinarianRequest>
{

}