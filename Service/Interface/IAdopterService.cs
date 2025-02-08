using Domain;
using Domain.DTOs;
using Domain.DTOs.Identity;
using Domain.identity;

namespace Service.Interface
{
    public interface IAdopterService : ICRUDService<User, CreateAdopterRequest, UpdateAdopterRequest>
    {

    }
}

