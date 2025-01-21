using Domain;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Domain.DTOs;

namespace Web.Controllers;

[Route("api/listing-documents")]
[ApiController]
public class OwnerPetListingDocumentController : ControllerBase
{
    private readonly IOwnerPetListingDocumentService _documentService;

    public OwnerPetListingDocumentController(IOwnerPetListingDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OwnerPetListingDocument>>> GetAllDocuments()
    {
        var documents = await _documentService.GetAllAsync();
        if (documents == null)
        {
            return BadRequest();
        }

        return Ok(documents);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<OwnerPetListingDocument>> GetDocument([FromRoute] Guid id)
    {
        var document = await _documentService.GetByIdAsync(id);

        if (document == null)
        {
            return NotFound();
        }
        return Ok(document);
    }

    [HttpPost]
    public async Task<ActionResult<OwnerPetListingDocument>> CreateDocument(
        [FromBody] CreateOwnerPetListingDocumentRequest request)
    {
        var document = await _documentService.CreateAsync(request);
        return Ok(document);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<OwnerPetListingDocument>> UpdateDocument(
        [FromBody] UpdateOwnerPetListingDocumentRequest request)
    {
        var updated = await _documentService.UpdateAsync(request.Id, request);
        if (updated == null)
        {
            return BadRequest();
        }
        return Ok(updated);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<OwnerPetListingDocument>> DeleteDocument([FromRoute] Guid id)
    {
        var deleted = await _documentService.DeleteAsync(id);

        if (!deleted)
        {
            return BadRequest();
        }
        return Ok(deleted);
    }
}