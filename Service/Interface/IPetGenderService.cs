using Domain;
using Domain.DTOs;
using Domain.DTOs.PetGender;

namespace Service.Interface;

public interface IPetGenderService : ICRUDService<PetGender, CreatePetGenderRequest, UpdatePetGenderRequest>
{
    
}