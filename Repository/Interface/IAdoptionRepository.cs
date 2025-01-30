using Domain;

namespace Repository.Interface;

public interface IAdoptionRepository: ICrudRepository<Adoption>
{
    public List<Adoption> GetAdoptionsForPet(Guid id);
}