namespace Web.Controllers;

using Domain;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Domain.DTOs;

[Route("api/adoption-statuses")]
[ApiController]
public class AdoptionStatusController : ControllerBase
{
    private readonly IAdoptionStatusService _adoptionStatusService;

    public AdoptionStatusController(IAdoptionStatusService adoptionStatusService)
    {
        _adoptionStatusService = adoptionStatusService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AdoptionStatus>>> GetAllAdoptionStatuses()
    {
        var adoptionStatuses = await _adoptionStatusService.GetAllAsync();
        if (adoptionStatuses == null)
        {
            return BadRequest();
        }

        return Ok(adoptionStatuses);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<AdoptionStatus>> GetAdoptionStatus([FromRoute] Guid id)
    {
        var adoptionStatus = await _adoptionStatusService.GetByIdAsync(id);
        if (adoptionStatus == null)
        {
            return NotFound();
        }

        return Ok(adoptionStatus);
    }

    [HttpPost]
    public async Task<ActionResult<AdoptionStatus>> CreateAdoptionStatus([FromBody] CreateAdoptionStatusRequest request)
    {
        var adoptionStatus = await _adoptionStatusService.CreateAsync(request);
        return Ok(adoptionStatus);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<AdoptionStatus>> UpdateAdoptionStatus([FromBody] UpdateAdoptionStatusRequest request)
    {
        var updated = await _adoptionStatusService.UpdateAsync(request.Id, request);
        if (updated == null)
        {
            return BadRequest();
        }

        return Ok(updated);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<bool>> DeleteAdoptionStatus([FromRoute] Guid id)
    {
        var deleted = await _adoptionStatusService.DeleteAsync(id);
        if (!deleted)
        {
            return BadRequest();
        }

        return Ok(deleted);
    }
}
