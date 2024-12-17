using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ApplicationDbContext : DbContext
{
    public DbSet<HealthStatus> HealthStatuses { get; set; }
    public DbSet<AdoptionStatus> AdoptionStatuses { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


}