using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class OwnerPetListingDocumentRepository : CrudRepository<OwnerPetListingDocument>, IOwnerPetListingDocumentRepository
{
    public OwnerPetListingDocumentRepository(ApplicationDbContext context) : base(context)
    {
    }
}