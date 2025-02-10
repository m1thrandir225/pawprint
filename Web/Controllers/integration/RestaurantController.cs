using Microsoft.AspNetCore.Mvc;
using Service.integration;

namespace Web.Controllers.integration;

[Route("api/restaurant")]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRestaurants()
    {
        var restaurants = await _restaurantService.GetAllAsync();
        if (restaurants == null)
        {
            return BadRequest();
        }
        
        return Ok(restaurants);
    }
}