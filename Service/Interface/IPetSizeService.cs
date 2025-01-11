using Domain;
using Domain.DTOs;

namespace Service.Interface;

public interface IPetSizeService : ICRUDService<PetSize, CreatePetSizeRequest, UpdatePetSizeRequest>
{
    
}