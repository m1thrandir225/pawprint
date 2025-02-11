using Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Implementations;

public class AdoptionStatusRepository : CrudRepository<AdoptionStatus>, IAdoptionStatusRepository
{
    private readonly ApplicationDbContext _context;
    public AdoptionStatusRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<AdoptionStatus> GetAdoptionStatusByName(string name)
    {
        return await _context.AdoptionStatuses.Where(a => a.Name == name).FirstAsync();
    }
}