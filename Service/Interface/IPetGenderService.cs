using Domain;
using Domain.DTOs;

namespace Service.Interface;

public interface IPetGenderService : ICRUDService<PetGender, CreatePetGenderRequest, UpdatePetGenderRequest>
{
    
}