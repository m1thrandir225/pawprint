using Domain;
using Domain.DTOs;
using Repository.Interface;

namespace Repository.Implementations;

public class ShelterRepository : CrudRepository<Shelter>, IShelterRepository
{
    private readonly ApplicationDbContext _context;
    public ShelterRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public List<MonthlyCreation> YearlyShelterStatistics()
    {
        var rawData = _context.Shelters
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