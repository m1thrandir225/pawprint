using Domain;
using Domain.identity;
using Repository.Interface;

namespace Repository.Implementations;

public class UserRepository : CrudRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }
}