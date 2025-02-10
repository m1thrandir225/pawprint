using Domain.integration;
using Repository.Interface;

namespace Repository.integration;

public interface IRestaurantRepository 
{
    Task<IEnumerable<Restaurant>> GetAll();
    
}