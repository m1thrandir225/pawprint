using Domain.integration;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class IntegrationDbContext : DbContext
{
    public IntegrationDbContext(DbContextOptions<IntegrationDbContext> options) : base(options) { }

    public DbSet<Restaurant> Restaurants { get; set; }
}