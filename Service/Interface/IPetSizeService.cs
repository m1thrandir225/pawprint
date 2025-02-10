using Domain;
using Domain.DTOs;
using Domain.DTOs.PetSize;

namespace Service.Interface;

public interface IPetSizeService : ICRUDService<PetSize, CreatePetSizeRequest, UpdatePetSizeRequest>
{
    
}