using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;
using Domain.DTOs;
using Domain.DTOs.Adoption;
using Domain.identity;
using Microsoft.AspNetCore.Authorization;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    [Route("api/adoptions")]
    [ApiController]
    public class AdoptionController : ControllerBase
    {
        private readonly IAdoptionService _adoptionService;
        private readonly IUserContextService _userContextService;

        public AdoptionController(IAdoptionService adoptionService, IUserContextService userContextService)
        {
            _adoptionService = adoptionService;
            _userContextService = userContextService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adoption>>> GetAllAdoptions()
        {
            var adoptions = await _adoptionService.GetAllAsync();
            if (adoptions == null)
            {
                return BadRequest();
            }

            return Ok(adoptions);
        }

        [HttpGet("pet/{id:guid}")]
        public List<Adoption> GetAdoptionsForPet([FromRoute] Guid id)
        {
            return _adoptionService.GetAdoptionsForPet(id);
        }

        [HttpGet("user/{id:guid}")]
        public async Task<ActionResult<List<Adoption>>> GetAdoptionsForUser([FromRoute] Guid id)
        {
            var userIdFromContext = _userContextService.GetUserId();

            if (id != userIdFromContext)
            {
                return Unauthorized("You can't view this resource");
            }

            var adoptions = await _adoptionService.GetAdoptionsForUser(id);

            return Ok(adoptions);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<Adoption>> GetAdoption([FromRoute] Guid id)
        {
            var adoption = await _adoptionService.GetByIdAsync(id);

            if (adoption == null)
            {
                return NotFound();
            }

            return Ok(adoption);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        public async Task<ActionResult<Adoption>> CreateAdoption([FromBody] CreateAdoptionRequest request)
        {
            try
            {
                var adopterId = _userContextService.GetUserId();

                var createDto = new CreateAdoptionDTO
                {
                    AdopterId = adopterId,
                    PetId = request.PetId,
                };

                var adoption = await _adoptionService.CreateAsync(createDto);

                return Ok(adoption);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut]
        [Route("{id:guid}/status")]
        public async Task<ActionResult<Adoption>> UpdateApprovalStatus([FromRoute] Guid id,
            [FromBody] UpdateApprovalStatusRequest request)
        {
            var updated = await _adoptionService.UpdateApprovalStatus(id, request.Status);

            if (updated == null)
            {
                return BadRequest();
            }
            return Ok(updated);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult<Adoption>> UpdateAdoption([FromBody] UpdateAdoptionRequest request,
            [FromRoute] Guid id)
        {
            var updated = await _adoptionService.UpdateAsync(id, request);
            if (updated == null)
            {
                return BadRequest();
            }

            return Ok(updated);
        }

        [HttpDelete]
        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.User}")]
        [Route("{id:guid}")]
        public async Task<ActionResult<Adoption>> DeleteAdoption([FromRoute] Guid id)
        {
            var deleted = await _adoptionService.DeleteAsync(id);

            if (!deleted)
            {
                return BadRequest();
            }

            return Ok(deleted);
        }
    }
}