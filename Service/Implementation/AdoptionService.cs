using System.Xml;
using Domain;
using Domain.DTOs;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class AdoptionService : IAdoptionService
{
    private readonly IAdoptionRepository _repository;

    public AdoptionService(IAdoptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Adoption>> GetAllAsync()
    {
        return _repository.GetAll();
    }

    public List<Adoption> GetAdoptionsForPet(Guid id)
    {
        return _repository.GetAdoptionsForPet(id);
    }

    public async Task<Adoption> GetByIdAsync(Guid id)
    {
        return _repository.Get(id);
    }

    public async Task<Adoption> CreateAsync(CreateAdoptionRequest dto)
    {
        var adoption = new Adoption(
            dto.PetId,
            dto.AdopterId,
            dto.AdoptionDate,
            dto.FollowUpDate,
            dto.CounselorNotes);

        return _repository.Insert(adoption);
    }

    public async Task<Adoption> UpdateAsync(Guid id, UpdateAdoptionRequest dto)
    {
        var adoption = _repository.Get(id);

        if (adoption == null)
        {
            return null;
        }
        
        adoption.PetId = dto.PetId;
        adoption.AdopterId = dto.AdopterId;
        adoption.AdoptionDate = dto.AdoptionDate;
        adoption.FollowUpDate = dto.FollowUpDate;
        adoption.CounselorNotes = dto.CounselorNotes;
        adoption.Approved = dto.Approved;
        
        return _repository.Update(adoption);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var adoption = _repository.Get(id);
        
        _repository.Delete(adoption);
        return Task.FromResult(true);
    }

    public List<MonthlyCreation> YearlyAdoptions()
    {
        return _repository.YearlyAdoptions();
    }
}