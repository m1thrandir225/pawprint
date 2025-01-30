using Domain;
using Domain.enums;
using Service.Interface;
using Domain.DTOs;
using Repository.Interface;

namespace Service.Implementation;

public class ShelterPetListingService : IShelterPetListingService
{
    private readonly IShelterPetListingRepository _repository;
    private readonly IEmailService _emailService;

    public ShelterPetListingService(IShelterPetListingRepository repository,  IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    public async Task<IEnumerable<ShelterPetListing>> GetAllAsync()
    {
        // return _repository.GetAllWithJoins();
        return _repository.GetAll();
    }

    public async Task<ShelterPetListing> GetByIdAsync(Guid id)
    {
        return _repository.Get(id);
        // return _repository.GetWithJoins(id);
    }

    public async Task<ShelterPetListing> CreateAsync(CreateShelterPetListingRequest dto)
    {
        // Create new listing with PENDING status by default
        var listing = new ShelterPetListing(
            dto.PetId,
            dto.MedicalRecordId,
            dto.ShelterId,
            dto.IntakeDate
        );

        var createdListing = _repository.Insert(listing);
        try
        {
            await _emailService.SendPetListingAdoptionNotificationAsync(
                listing.Shelter.Email, 
                PetListingType.ShelterPetListing,
                listing
            );
        }
        catch (Exception e)
        {
            throw new Exception("Failed to send email.");
        }

        return createdListing;
    }

    public async Task<ShelterPetListing> UpdateAsync(Guid id, UpdateShelterPetListingRequest dto)
    {
        var listing = _repository.Get(id);

        if (listing == null)
        {
            return null;
        }

        // Only update mutable properties
        listing.IntakeDate = dto.IntakeDate;
        listing.Approved = dto.Approved;

        return _repository.Update(listing);
    }

    public async Task<ShelterPetListing> UpdateApprovalStatusAsync(Guid id, ApprovalStatus status)
    {
        var listing = _repository.Get(id);

        if (listing == null)
        {
            return null;
        }

        listing.Approved = status;
        return _repository.Update(listing);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var listing = _repository.Get(id);
        
        _repository.Delete(listing);
        return Task.FromResult(true);
    }
}