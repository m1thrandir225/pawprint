using Domain.integration;
using Service.Interface;

namespace Service.integration;

public interface IRestaurantService 
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
}