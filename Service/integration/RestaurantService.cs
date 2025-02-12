using Domain.integration;
using Repository.integration;

namespace Service.integration;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;

    public RestaurantService(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        return await _restaurantRepository.GetAll();
    }
}