using Domain;

namespace Repository.Interface;

public interface IPetRepository
{
    IEnumerable<Pet> GetAll();
    Pet GetById(Guid petId);
    void Insert(Pet pet);
    void Update(Pet pet);
    void Delete(Guid petId);
    void Save();
}