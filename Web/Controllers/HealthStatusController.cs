using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;
using Domain.DTOs;
using Domain.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Route("api/health-statuses")]
    [ApiController]
    public class HealthStatusController : ControllerBase
    {
        private readonly IHealthStatusService _healthStatusService;

        public HealthStatusController(IHealthStatusService healthStatusService)
        {
            _healthStatusService = healthStatusService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HealthStatus>>> GetAllHealthStatuss()
        {
            var healthStatuss = await _healthStatusService.GetAllAsync();
            if (healthStatuss == null)
            {
                return BadRequest();
            }

            return Ok(healthStatuss);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<HealthStatus>> GetHealthStatus([FromRoute] Guid id)
        {
            var healthStatus = await _healthStatusService.GetByIdAsync(id);

            if (healthStatus == null)
            {
                return NotFound();
            }
            return Ok(healthStatus);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.Shelter}")]
        public async Task<ActionResult<HealthStatus>> CreateHealthStatus([FromBody] CreateHealthStatusRequest request)
        {
            var healthStatus = await _healthStatusService.CreateAsync(request);

            return Ok(healthStatus);
        }

        [HttpPut]
        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.Shelter}")]
        [Route("{id:guid}")]
        public async Task<ActionResult<HealthStatus>> UpdateHealthStatus([FromBody] UpdateHealthStatusRequest request, [FromRoute] Guid id)
        {
            var updated = await _healthStatusService.UpdateAsync(id ,request);
            if (updated == null)
            {
                return BadRequest();
            }
            return Ok(updated);

        }

        [HttpDelete]
        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.Shelter}")]
        [Route("{id:guid}")]
        public async Task<ActionResult<HealthStatus>> DeleteHealthStatus([FromRoute] Guid id)
        {
            var deleted = await _healthStatusService.DeleteAsync(id);

            if (!deleted)
            {
                return BadRequest();
            }
            return Ok(deleted);
        }

    }
}
