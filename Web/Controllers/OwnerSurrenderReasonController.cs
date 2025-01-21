namespace Web.Controllers;

using Domain;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Domain.DTOs;

[Route("api/owner-surrender-reasons")]
[ApiController]
public class OwnerSurrenderReasonController : ControllerBase
{
    private readonly IOwnerSurrenderReasonService _ownerSurrenderReasonService;

    public OwnerSurrenderReasonController(IOwnerSurrenderReasonService ownerSurrenderReasonService)
    {
        _ownerSurrenderReasonService = ownerSurrenderReasonService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OwnerSurrenderReason>>> GetAllOwnerSurrenderReasons()
    {
        var ownerSurrenderReasons = await _ownerSurrenderReasonService.GetAllAsync();
        if (ownerSurrenderReasons == null)
        {
            return BadRequest();
        }

        return Ok(ownerSurrenderReasons);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<OwnerSurrenderReason>> GetOwnerSurrenderReason([FromRoute] Guid id)
    {
        var ownerSurrenderReason = await _ownerSurrenderReasonService.GetByIdAsync(id);
        if (ownerSurrenderReason == null)
        {
            return NotFound();
        }

        return Ok(ownerSurrenderReason);
    }

    [HttpPost]
    public async Task<ActionResult<OwnerSurrenderReason>> CreateOwnerSurrenderReason([FromBody] CreateOwnerSurrenderReasonRequest request)
    {
        var ownerSurrenderReason = await _ownerSurrenderReasonService.CreateAsync(request);
        return Ok(ownerSurrenderReason);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<OwnerSurrenderReason>> UpdateOwnerSurrenderReason([FromBody] UpdateOwnerSurrenderReasonRequest request)
    {
        var updated = await _ownerSurrenderReasonService.UpdateAsync(request.Id, request);
        if (updated == null)
        {
            return BadRequest();
        }

        return Ok(updated);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<bool>> DeleteOwnerSurrenderReason([FromRoute] Guid id)
    {
        var deleted = await _ownerSurrenderReasonService.DeleteAsync(id);
        if (!deleted)
        {
            return BadRequest();
        }

        return Ok(deleted);
    }
}
