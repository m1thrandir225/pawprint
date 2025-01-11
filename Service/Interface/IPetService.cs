using Domain;
using Domain.DTOs;

namespace Service.Interface;

public interface IPetService : ICRUDService<Pet, CreatePetRequest, UpdatePetRequest>
{

}