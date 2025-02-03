using Domain;
using Domain.enums;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Domain.DTOs;
using Domain.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers;

[Route("api/shelter-listings")]
[ApiController]
public class ShelterPetListingController : ControllerBase
{
    private readonly IShelterPetListingService _listingService;
    private readonly IEmailService _emailService;

    public ShelterPetListingController(IShelterPetListingService listingService, IEmailService emailService)
    {
        _listingService = listingService;
        _emailService = emailService;
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
    public ICollection<ShelterPetListing> GetListingsByShelter([FromRoute] Guid id, [FromQuery] ApprovalStatus? status)
    {
        if(status is not null)
        {
            return _listingService.FilterByStatus((ApprovalStatus) status, id);
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
    [Authorize(Roles = $"{UserRole.Admin}, {UserRole.Shelter}")]
    public async Task<ActionResult<ShelterPetListing>> CreateListing(
        [FromBody] CreateShelterPetListingRequest request)
    {
        var listing = await _listingService.CreateAsync(request);
        if (listing == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        var emailSent = await _emailService.SendPetListingAdoptionNotificationAsync(
            listing.Shelter.Email,
            PetListingType.ShelterPetListing,
            listing
        );

        if (!emailSent)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        return Ok(listing);
    }

    [HttpPut("{id:guid}")] 
    [Authorize(Roles = $"{UserRole.Admin}, {UserRole.Shelter}")]
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
    [Authorize(Roles = $"{UserRole.Admin}, {UserRole.Shelter}")]
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