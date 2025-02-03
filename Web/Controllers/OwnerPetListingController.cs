using Domain;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Domain.DTOs;
using Domain.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers;

[Route("api/owner-pet-listings")]
[ApiController]
public class OwnerPetListingController : ControllerBase
{
    private readonly IOwnerPetListingService _ownerPetListingService;
    private readonly IEmailService _emailService;

    public OwnerPetListingController(IOwnerPetListingService ownerPetListingService, IEmailService emailService)
    {
        _ownerPetListingService = ownerPetListingService;
        _emailService = emailService;
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
    [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
    public async Task<ActionResult<OwnerPetListing>> CreateOwnerPetListing(
        [FromBody] CreateOwnerPetListingRequest request)
    {
        var ownerPetListing = await _ownerPetListingService.CreateAsync(request);

        if (ownerPetListing == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        var emailSent = await _emailService.SendPetListingAdoptionNotificationAsync(
            ownerPetListing.Adopter.Email,
            PetListingType.OwnerPetListing,
            ownerPetListing
        );

        if (!emailSent)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(ownerPetListing);
    }

    [HttpPut]
    [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
    [Route("{id:guid}")]
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
    [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
    [Route("{id:guid}")]
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