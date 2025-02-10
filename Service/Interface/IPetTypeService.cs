using Domain;
using Domain.DTOs;
using Domain.DTOs.PetType;

namespace Service.Interface;

public interface IPetTypeService : ICRUDService<PetType, CreatePetTypeRequest, UpdatePetTypeRequest>
{

}