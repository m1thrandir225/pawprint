using Domain.integration;
using Microsoft.EntityFrameworkCore;
using Repository.Implementations;

namespace Repository.integration;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly IntegrationDbContext _integrationDbContext;

    public RestaurantRepository(IntegrationDbContext integrationDbContext)
    {
        _integrationDbContext = integrationDbContext;
    }

    public async Task<IEnumerable<Restaurant>> GetAll()
    {
        return await _integrationDbContext.Restaurants.ToListAsync();
    }
}