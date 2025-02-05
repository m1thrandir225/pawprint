﻿using Domain;
using Domain.enums;
using Service.Interface;
using Domain.DTOs;
using Repository.Interface;

namespace Service.Implementation;

public class ShelterPetListingService : IShelterPetListingService
{
    private readonly IMedicalRecordService _medicalRecordService;
    private readonly IShelterPetListingRepository _repository;
    private readonly IEmailService _emailService;

    public ShelterPetListingService(IShelterPetListingRepository repository,  IEmailService emailService, IMedicalRecordService medicalRecordService)
    {
        _repository = repository;
        _emailService = emailService;
        _medicalRecordService = medicalRecordService;
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
        var medicalRecord = await _medicalRecordService.CreateAsync(dto.MedicalRecord);
        
        // Create new listing with PENDING status by default
        var listing = new ShelterPetListing(
            dto.PetId,
            medicalRecord.Id,
            dto.ShelterId,
            dto.IntakeDate,
            dto.AdoptionFee
        );

        var createdListing = _repository.Insert(listing);

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
        listing.AdoptionFee = dto.AdoptionFee;

        return _repository.Update(listing);
    }
    
    public Task<bool> DeleteAsync(Guid id)
    {
        var listing = _repository.Get(id);
        
        _repository.Delete(listing);
        return Task.FromResult(true);
    }

    public ICollection<ShelterPetListing> GetListingsByShelter(Guid id)
    {
        return _repository.GetListingByShelter(id);
    }

    public  ICollection<ShelterPetListing> FilterShelterPetListing(Guid? petSizeId, Guid? petTypeId, Guid? petGenderId,
        string? search)
    {
        return _repository.FilterListings(petTypeId, petSizeId, petGenderId, search);
    }

    public ICollection<ShelterPetListing> FilterByStatus(Guid adoptionStatusId, Guid shelterId)
    {
        return _repository.FilterByStatus(adoptionStatusId, shelterId);
    }
}