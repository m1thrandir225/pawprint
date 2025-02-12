using Bogus.DataSets;
using Domain;
using Domain.enums;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Domain.DTOs;
using Domain.DTOs.MedicalCondition;
using Domain.DTOs.MedicalRecord;
using Domain.DTOs.Pet;
using Domain.DTOs.ShelterPetListing;
using Domain.DTOs.Vaccination;
using Domain.DTOs.Veterinarian;
using Domain.DTOs.VeterinarianSpecialization;
using Domain.identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Web.Filters;
using Web.Services.Interfaces;

namespace Web.Controllers;

[Route("api/shelter-listings")]
[ApiController]
public class ShelterPetListingController : ControllerBase
{
    private readonly IShelterPetListingService _listingService;
    private readonly IVeterinarianService _veterinarianService;
    private readonly IVeterinarianSpecializationService _veterinarianSpecializationService;
    private readonly IMedicalRecordService _medicalRecordService;
    private readonly IMedicalConditionService _medicalConditionService;
    private readonly IPetService _petService;
    private readonly IEmailService _emailService;
    private readonly IUploadService _uploadService;
    private readonly IAdoptionStatusService _adoptionStatusService;
    private readonly IUserContextService _userContextService;
    private readonly IVaccinationService _vaccinationService;

    public ShelterPetListingController(
        IShelterPetListingService listingService,
        IEmailService emailService,
        IVeterinarianService veterinarianService,
        IVeterinarianSpecializationService veterinarianSpecializationService,
        IMedicalRecordService medicalRecordService,
        IMedicalConditionService medicalConditionService,
        IPetService petService,
        IUploadService uploadService,
        IAdoptionStatusService adoptionStatusService,
        IUserContextService userContextService,
        IVaccinationService vaccinationService
    )
    {
        _listingService = listingService;
        _emailService = emailService;
        _veterinarianService = veterinarianService;
        _veterinarianSpecializationService = veterinarianSpecializationService;
        _medicalRecordService = medicalRecordService;
        _medicalConditionService = medicalConditionService;
        _petService = petService;
        _uploadService = uploadService;
        _adoptionStatusService = adoptionStatusService;
        _userContextService = userContextService;
        _vaccinationService = vaccinationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShelterPetListing>>> GetAllListings(
        [FromQuery(Name = "pet-type")] Guid? petTypeId,
        [FromQuery(Name = "pet-size")] Guid? petSizeId,
        [FromQuery(Name = "pet-gender")] Guid? petGenderId,
        [FromQuery(Name = "search")] string? search
    )
    {

        var listings = _listingService.FilterShelterPetListing(petSizeId, petTypeId, petGenderId, search);

        return Ok(listings);
    }


    [HttpGet("shelter/{id:guid}")]
    public ICollection<ShelterPetListing> GetListingsByShelter([FromRoute] Guid id,
        [FromQuery(Name = "adoption-status")] Guid? adoptionStatusId)
    {
        if (adoptionStatusId is not null)
        {
            return _listingService.FilterByStatus((Guid)adoptionStatusId, id);
        }

        return _listingService.GetListingsByShelter(id);
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ShelterPetListing>> GetListing([FromRoute] Guid id)
    {
        var listing = await _listingService.GetByIdAsync(id);

        if (listing == null)
        {
            return NotFound();
        }

        return Ok(listing);
    }

    [HttpPost]
    //[Authorize(Roles = $"{UserRole.Admin}, {UserRole.Shelter}")]
    public async Task<ActionResult<Guid>> CreateListing(
        [FromForm] CreateShelterPetListingRequest request, [FromForm] List<IFormFile> imageShowcase,
        [FromForm] IFormFile avatarImg)
    {
        /*
         * Steps to create:
         * 1. Upload images
         * 2. Create a veterinarian
         * 3. Create specializations for the veterinarian
         * 4. Create medical record
         * 5. Create pet
         * 6. Create listing
         * 7. return the id of the created listing
         */
        try
        {
            var shelterId = _userContextService.GetUserId();

            if (imageShowcase.Count == 0)
            {
                throw new BadHttpRequestException("Image showcase missing images.");
            }

            var avatarImgPath = await _uploadService.UploadFile(avatarImg);

            var imageShowcasePaths = new List<string>();
            foreach (var image in imageShowcase)
            {
                var path = await _uploadService.UploadFile(image);
                imageShowcasePaths.Add(path);
            }

            var veterinarian = await _veterinarianService.CreateAsync(new CreateVeterinarianRequest
            {
                Name = request.VeterinarianName,
                Email = request.VeterinarianEmail,
                ClinicName = request.VeterinarianClinicName,
                ContactNumber = request.VeterinarianContactNumber,
            });
            if (veterinarian == null)
            {
                throw new BadHttpRequestException("Veterinarian not created.");
            }

            foreach (var specialization in request.VeterinarianSpecializations)
            {
                await _veterinarianSpecializationService.CreateAsync(
                    new CreateVeterinarianSpecializationRequest
                    {
                        Specialization = specialization,
                        VeterinarianId = veterinarian.Id
                    });
            }

            var medicalRecord = await _medicalRecordService.CreateAsync(new CreateMedicalRecordRequest
            {
                MicrochipNumber = request.MicrochipNumber,
                LastMedicalCheckup = request.LastMedicalCheckup,
                SpayNeuterStatus = request.SpayNeuterStatus,
                VetId = veterinarian.Id,
            });

            if (medicalRecord == null)
            {
                throw new BadHttpRequestException("Medical record not created.");
            }

            if (request.MedicalConditions != null)
            {
                foreach (var condition in request.MedicalConditions)
                {
                    var medicalCondition = await _medicalConditionService.CreateAsync(new CreateMedicalConditionRequest
                    {
                        MedicalRecordId = medicalRecord.Id,
                        Notes = condition.Notes,
                        ConditionName = condition.ConditionName,
                    });
                }
            }

            foreach (var vaccine in request.Vaccinations)
            {
                await _vaccinationService.CreateAsync(new CreateVaccinationRequest
                {
                    MedicalRecordId = medicalRecord.Id,
                    VaccinationName = vaccine.Name,
                    VaccineDate = vaccine.Date,
                });
            }

            var pendingAdoptionStatus = await _adoptionStatusService.GetAdoptionStatusByName(AdoptionStatuses.Pending);
            if (pendingAdoptionStatus == null)
            {
                throw new BadHttpRequestException("Adoption status not found.");
            }

            var pet = await _petService.CreateAsync(new CreatePetDTO
            {
                Name = request.Name,
                Breed = request.Breed,
                AgeYears = request.AgeYears,
                BehaviorialNotes = request.BehaviorialNotes,
                EnergyLevel = request.EnergyLevel,
                AvatarImg = avatarImgPath,
                ImageShowcase = imageShowcasePaths.ToArray(),
                IntakeDate = request.IntakeDate?.ToDateTime(new TimeOnly(0, 0, 0)),
                SpecialRequirements = request.SpecialRequirements,
                GoodWithCats = request.GoodWithCats,
                GoodWithChildren = request.GoodWithChildren,
                GoodWithDogs = request.GoodWithDogs,
                HealthStatusId = request.HealthStatusId,
                PetGenderId = request.PetGenderId,
                PetSizeId = request.PetSizeId,
                PetTypeId = request.PetTypeId,
                AdoptionStatusId = pendingAdoptionStatus.Id
            });

            if (pet == null)
            {
                throw new BadHttpRequestException("Pet not created.");
            }

            var listing = await _listingService.CreateAsync(new CreateShelterPetListingDTO
            {
                IntakeDate = request.IntakeDate,
                AdoptionFee = request.Fee,
                PetId = pet.Id,
                MedicalRecordId = medicalRecord.Id,
                ShelterId = shelterId,
            });

            if (listing == null)
            {
                throw new BadHttpRequestException("Listing not created.");
            }

            return Ok(listing.Id);
        }
        catch (BadHttpRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (UnauthorizedAccessException e)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
        }
        catch (Exception e)
        {
            if (e.Message == "Internal Server Error")
            {
                return StatusCode(500);
            }

            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = $"{UserRole.Admin},{UserRole.Shelter}")]
    [AuthorizeWithUserId<UpdateShelterPetListingRequest>]
    public async Task<ActionResult<ShelterPetListing>> UpdateListing(
        [FromBody] UpdateShelterPetListingRequest request)
    {
        var updated = await _listingService.UpdateAsync(request.Id, request);
        if (updated == null)
        {
            return BadRequest();
        }

        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    [AuthorizeWithUserId<UserResourceId>]
    [Authorize(Roles = $"{UserRole.Admin},{UserRole.Shelter}")]
    public async Task<ActionResult<bool>> DeleteListing([FromRoute] Guid id)
    {
        var deleted = await _listingService.DeleteAsync(id);

        if (!deleted)
        {
            return BadRequest();
        }

        return Ok(deleted);
    }
}