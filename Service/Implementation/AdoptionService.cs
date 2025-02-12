using System.Xml;
using Domain;
using Domain.DTOs;
using Domain.DTOs.Adoption;
using Domain.enums;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class AdoptionService : IAdoptionService
{
    private readonly IAdoptionRepository _repository;
    private readonly IPetRepository _petRepository;

    public AdoptionService(IAdoptionRepository repository, IPetRepository petRepository)
    {
        _repository = repository;
        _petRepository = petRepository;
    }

    public async Task<IEnumerable<Adoption>> GetAllAsync()
    {
        return await _repository.GetAll();
    }

    public List<Adoption> GetAdoptionsForPet(Guid id)
    {
        return _repository.GetAdoptionsForPet(id);
    }

    public async Task<Adoption> GetByIdAsync(Guid id)
    {
        return await _repository.Get(id);
    }

    public async Task<Adoption> CreateAsync(CreateAdoptionDTO dto)
    {
        var adoption = new Adoption{
            PetId = dto.PetId,
            AdopterId = dto.AdopterId,
            AdoptionDate = dto.AdoptionDate,
            FollowUpDate = dto.FollowUpDate,
            CounselorNotes = dto.CounselorNotes
        };

        return await _repository.Insert(adoption);
    }

    public async Task<Adoption> UpdateAsync(Guid id, UpdateAdoptionRequest dto)
    {
        var adoption = await _repository.Get(id);

        if (adoption == null)
        {
            return null;
        }

        if (dto.AdoptionDate is not null)
        {
            adoption.AdoptionDate = dto.AdoptionDate;
        }

        if (dto.FollowUpDate is not null)
        {
            adoption.FollowUpDate = dto.FollowUpDate;
        }

        if (dto.CounselorNotes is not null)
        {
            adoption.CounselorNotes = dto.CounselorNotes;
        }

        var pet = await _petRepository.Get(adoption.PetId);

        if (pet == null)
        {
            return null;
        }

        pet.AdoptionStatusId = dto.AdoptionStatusId;

        await _petRepository.Update(pet);

        return await _repository.Update(adoption);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var adoption = await _repository.Get(id);
        
        await _repository.Delete(adoption);
        return true;
    }

    public async Task<Adoption> UpdateApprovalStatus(Guid id, ApprovalStatus approvalStatus)
    {
        var adoption = await _repository.Get(id);
        if (adoption == null)
        {
            return null;
        }

        adoption.Approved = approvalStatus;
        return await _repository.Update(adoption);
    }

    public async Task<List<Adoption>> GetAdoptionsForUser(Guid id)
    {
        return await _repository.GetAdoptionsForUser(id);
    }

    public List<MonthlyCreation> YearlyAdoptions()
    {
        return _repository.YearlyAdoptions();
    }
}