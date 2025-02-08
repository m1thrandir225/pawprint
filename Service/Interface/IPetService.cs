using Domain;
using Domain.DTOs;
using Domain.DTOs.Pet;

namespace Service.Interface;

public interface IPetService : ICRUDService<Pet, CreatePetDTO, UpdatePetDTO>
{

}