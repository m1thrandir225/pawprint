using Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Implementations;

public class ShelterPetListingRepository : CrudRepository<ShelterPetListing>, IShelterPetListingRepository
{
    public ShelterPetListingRepository(ApplicationDbContext context) : base(context)
    {
    }
}