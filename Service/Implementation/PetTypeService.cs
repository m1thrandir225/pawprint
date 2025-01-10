using Domain;
using Repository.Implementations;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementations;

public class PetTypeService : IPetTypeService
{

    private readonly IPetTypeRepository _petTypeRepository;

    public PetTypeService(IPetTypeRepository petTypeRepository)
    {
        _petTypeRepository = petTypeRepository;
    }
    public IEnumerable<PetType> GetAll()
    {
        return _petTypeRepository.GetAll();
    }

    public PetType Get(Guid? id)
    {
        return _petTypeRepository.Get(id);
    }

    public void Insert(PetType entity)
    {
        _petTypeRepository.Insert(entity);
    }

    public void Update(PetType entity)
    {
        _petTypeRepository.Update(entity);
    }

    public void Delete(PetType entity)
    {
        _petTypeRepository.Delete(entity);
    }
}