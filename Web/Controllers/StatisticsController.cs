using Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace Web.Controllers;

[Route("api/statistics")]
[ApiController]
[Authorize(Roles = $"{UserRole.Admin}")]
public class StatisticsController : Controller
{
    private readonly IAdoptionService _adoptionService;
    private readonly IShelterService _shelterService;

    public StatisticsController(IAdoptionService adoptionService, IShelterService shelterService)
    {
        _adoptionService = adoptionService;
        _shelterService = shelterService;
    }

    [HttpGet]
    public async Task<object> GetYearlyStatistics()
    {
        var shelterStats = _shelterService.YearlyShelterStatistics();

        var adoptionStats = _adoptionService.YearlyAdoptions();

        return new
        {
            shelter = shelterStats,
            adoption = adoptionStats,
        };
    }
}