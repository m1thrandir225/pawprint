using Domain;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementations;

public class PetGenderService : IPetGenderService
{
    private readonly IPetGenderRepository _petGenderRepository;

    public PetGenderService(IPetGenderRepository petGenderRepository)
    {
        _petGenderRepository = petGenderRepository;
    }
    
    public IEnumerable<PetGender> GetAll()
    {
        return _petGenderRepository.GetAll();
    }

    public PetGender Get(Guid? id)
    {
        return _petGenderRepository.Get(id);
    }

    public void Insert(PetGender entity)
    {
        _petGenderRepository.Insert(entity);
    }

    public void Update(PetGender entity)
    {
        _petGenderRepository.Update(entity);
    }

    public void Delete(PetGender entity)
    {
        _petGenderRepository.Delete(entity);
    }
}