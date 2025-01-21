using Domain;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Domain.DTOs;

namespace Web.Controllers;

[Route("api/owner-pet-listings")]
[ApiController]
public class OwnerPetListingController : ControllerBase
{
    private readonly IOwnerPetListingService _ownerPetListingService;

    public OwnerPetListingController(IOwnerPetListingService ownerPetListingService)
    {
        _ownerPetListingService = ownerPetListingService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OwnerPetListing>>> GetAllOwnerPetListings()
    {
        var ownerPetListings = await _ownerPetListingService.GetAllAsync();
        if (ownerPetListings == null)
        {
            return BadRequest();
        }
        return Ok(ownerPetListings);
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
    public async Task<ActionResult<OwnerPetListing>> CreateOwnerPetListing([FromBody] CreateOwnerPetListingRequest request)
    {
        var ownerPetListing = await _ownerPetListingService.CreateAsync(request);
        return Ok(ownerPetListing);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<ActionResult<OwnerPetListing>> UpdateOwnerPetListing([FromBody] UpdateOwnerPetListingRequest request)
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
