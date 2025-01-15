using Domain;
using Domain.DTOs;

namespace Service.Interface
{
    public interface IAdopterService : ICRUDService<User, CreateAdopterRequest, UpdateAdopterRequest>
    {

    }
}

