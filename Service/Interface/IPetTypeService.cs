using Domain;
using Domain.DTOs;

namespace Service.Interface;

public interface IPetTypeService : ICRUDService<PetType, CreatePetTypeRequest, UpdatePetTypeRequest>
{

}