using Domain;
using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Implementations;

public class AdoptionRepository : CrudRepository<Adoption>, IAdoptionRepository
{
    private readonly ApplicationDbContext _context;
    public AdoptionRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public List<Adoption> GetAdoptionsForPet(Guid id)
    {
        return _context.Adoptions.Where(a => a.PetId == id).ToList();
    }

    public async Task<List<Adoption>> GetAdoptionsForUser(Guid id)
    {
        return await _context.Adoptions.Where(a => a.AdopterId == id).ToListAsync();
    }

    public List<MonthlyCreation> YearlyAdoptions()
    {
        var rawData = _context.Adoptions
            .Select(x => new { x.CreatedAt })
            .ToList();

        var groupedData = rawData
            .GroupBy(x => new { x.CreatedAt.Year, x.CreatedAt.Month })
            .Select(g => new
            {
                Month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM"),
                Total = g.Count()
            })
            .ToList();
        var allMonths = Enumerable.Range(1, 12)
            .Select(month => new DateTime(DateTime.UtcNow.Year, month, 1).ToString("MMM"))
            .ToList();

        var statistics = allMonths
            .GroupJoin(
                groupedData,
                month => month,
                data => data.Month,
                (month, data) => new MonthlyCreation
                {
                    Month = month,
                    Total = data.Select(x => x.Total).FirstOrDefault()
                })
            .OrderBy(x => DateTime.ParseExact(x.Month, "MMM", System.Globalization.CultureInfo.InvariantCulture))
            .ToList();
        return statistics;
    }
}