using Domain;

namespace Repository.Interface;

public interface IPetTypeRepository
{
    IEnumerable<PetType> GetAll();
    PetType GetById(Guid petTypeId);
    void Insert(PetType petType);
    void Update(PetType petType);
    void Delete(Guid petTypeId);
    void Save();
}