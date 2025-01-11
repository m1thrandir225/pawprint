using Domain;
using Domain.DTOs;

namespace Service.Interface;

public interface IVeterinarianService : ICRUDService<Veterinarian, CreateVeterinarianRequest, UpdateVeterinarianRequest>
{

}