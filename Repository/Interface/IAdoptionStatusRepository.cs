using Domain;

namespace Repository.Interface;

public interface IAdoptionStatusRepository : ICrudRepository<AdoptionStatus>
{
    public Task<AdoptionStatus> GetAdoptionStatusByName(string name);
}