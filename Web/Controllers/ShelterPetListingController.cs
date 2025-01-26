using Domain;
using Domain.enums;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Domain.DTOs;

namespace Web.Controllers;

[Route("api/shelter-listings")]
[ApiController]
public class ShelterPetListingController : ControllerBase
{
    private readonly IShelterPetListingService _listingService;

    public ShelterPetListingController(IShelterPetListingService listingService)
    {
        _listingService = listingService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShelterPetListing>>> GetAllListings()
    {
        var listings = await _listingService.GetAllAsync();
        if (listings == null)
        {
            return BadRequest();
        }

        return Ok(listings);
    }

    //shelter-listings/shelter/{shelter-id}
    [HttpGet("shelter/{id:guid}")]
    public List<ShelterPetListing> GetListingsByShelter([FromRoute] Guid id)
    {
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
    public async Task<ActionResult<ShelterPetListing>> CreateListing(
        [FromBody] CreateShelterPetListingRequest request)
    {
        var listing = await _listingService.CreateAsync(request);
        return Ok(listing);
    }

    [HttpPut("{id:guid}")] 
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

    [HttpPatch("{id:guid}/approval-status")]
    public async Task<ActionResult<ShelterPetListing>> UpdateApprovalStatus(
        [FromRoute] Guid id, 
        [FromBody] ApprovalStatus status)
    {
        var updated = await _listingService.UpdateApprovalStatusAsync(id, status);
        if (updated == null)
        {
            return BadRequest();
        }
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ShelterPetListing>> DeleteListing([FromRoute] Guid id)
    {
        var deleted = await _listingService.DeleteAsync(id);

        if (!deleted)
        {
            return BadRequest();
        }
        return Ok(deleted);
    }
}