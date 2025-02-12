using Domain;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Domain.DTOs;
using Domain.DTOs.OwnerPetListing;
using Domain.DTOs.OwnerPetListingDocument;
using Domain.DTOs.Pet;
using Domain.identity;
using Microsoft.AspNetCore.Authorization;
using Web.Filters;
using Web.Services.Interfaces;

namespace Web.Controllers;

[Route("api/owner-pet-listings")]
[ApiController]
public class OwnerPetListingController : ControllerBase
{
    private readonly IOwnerPetListingService _ownerPetListingService;
    private readonly IOwnerPetListingDocumentService _ownerPetListingDocumentService;
    private readonly IUploadService _uploadService;
    private readonly IEmailService _emailService;
    private readonly IPetService _petService;
    private readonly IAdoptionStatusService _adoptionStatusService;
    private readonly IUserContextService _userContextService;
    public OwnerPetListingController(
        IOwnerPetListingService ownerPetListingService,
        IEmailService emailService,
        IOwnerPetListingDocumentService ownerPetListingDocumentService,
        IUploadService uploadService,
        IPetService petService,
        IAdoptionStatusService adoptionStatusService,
        IUserContextService userContextService
        )
    {
        _ownerPetListingService = ownerPetListingService;
        _emailService = emailService;
        _ownerPetListingDocumentService = ownerPetListingDocumentService;
        _uploadService = uploadService;
        _petService = petService;
        _adoptionStatusService = adoptionStatusService;
        _userContextService = userContextService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OwnerPetListing>>> GetAllOwnerPetListings(
        [FromQuery(Name = "pet-type")] Guid? petTypeId,
        [FromQuery(Name = "pet-size")] Guid? petSizeId,
        [FromQuery(Name = "pet-gender")] Guid? petGenderId,
        [FromQuery(Name = "search")] string? search
        )
    {
        var ownerPetListings = _ownerPetListingService.FilterShelterPetListing(petTypeId, petSizeId, petGenderId, search);

        return Ok(ownerPetListings);
    }

    [HttpGet("owner/{id:guid}")]
    public ICollection<OwnerPetListing> GetListingsByOwner([FromRoute] Guid id, [FromQuery(Name = "adoption-status")] Guid? adoptionStatusId)
    {
        if (adoptionStatusId is not null)
        {
            return _ownerPetListingService.FilterByStatus((Guid) adoptionStatusId, id);
        }
        return _ownerPetListingService.FilterListingsByOwner(id);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<OwnerPetListing>> GetOwnerPetListing([FromRoute] Guid id)
    {
        var ownerPetListing = await _ownerPetListingService.GetByIdAsync(id);

        if (ownerPetListing == null)
        {
            return NotFound();
        }

        return Ok(ownerPetListing);
    }

    [HttpPost]

    public async Task<ActionResult<Guid>> CreateOwnerPetListing(
        [FromForm] CreateOwnerPetListingRequest request, [FromForm] List<IFormFile> documents, [FromForm] List<IFormFile> imageShowcase, [FromForm] IFormFile avatarImg)
    {
        /*
         * Steps
         * 1. Upload images
         * 2. Create Pet
         * 3. Create listing
         * 4. Upload documents for the listing
         * 5. Return id of created listing.
         */
        try
        {
            var userId = _userContextService.GetUserId();

            var avatarImgPath = await _uploadService.UploadFile(avatarImg);

            var imageShowcasePaths = new List<string>();
            foreach (var image in imageShowcase)
            {
                var path = await _uploadService.UploadFile(image);
                imageShowcasePaths.Add(path);
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
                throw new Exception("Pet not created.");
            }

            var listing = await _ownerPetListingService.CreateAsync(new CreateOwnerPetListingDTO
            {
                AdopterId = userId,
                AdoptionFee = request.Fee,
                PetId = pet.Id,
                SurrenderReasonId = request.SurrenderReasonId,
            });

            if (listing == null)
            {
                throw new Exception("Listing not created.");
            }

            foreach (var document in documents)
            {
                var documentPath = await _uploadService.UploadFile(document);
                await _ownerPetListingDocumentService.CreateAsync(new CreateOwnerPetListingDocumentRequest
                {
                    DocumentType = document.ContentType,
                    DocumentUrl = documentPath,
                    ListingId = listing.Id,
                });
            }

            // var emailSent = await _emailService.SendPetListingAdoptionNotificationAsync(
            //     listing.Adopter.Email,
            //     PetListingType.OwnerPetListing,
            //     listing
            // );

            // if (!emailSent)
            // {
            //     return StatusCode(StatusCodes.Status500InternalServerError);
            // }

            return Ok(listing.Id);
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }

    [HttpPut]
    [Route("{id:guid}")]
    [Authorize(Roles = $"{UserRole.Admin},{UserRole.User}")]
    [AuthorizeWithUserId<UpdateOwnerPetListingRequest>]
    public async Task<ActionResult<OwnerPetListing>> UpdateOwnerPetListing(
        [FromBody] UpdateOwnerPetListingRequest request)
    {
        var updated = await _ownerPetListingService.UpdateAsync(request.Id, request);
        if (updated == null)
        {
            return BadRequest();
        }

        return Ok(updated);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    [AuthorizeWithUserId<UserResourceId>]
    [Authorize(Roles = $"{UserRole.Admin},{UserRole.User}")]
    public async Task<ActionResult<OwnerPetListing>> DeleteOwnerPetListing([FromRoute] Guid id)
    {
        var deleted = await _ownerPetListingService.DeleteAsync(id);

        if (!deleted)
        {
            return BadRequest();
        }

        return Ok(deleted);
    }
}