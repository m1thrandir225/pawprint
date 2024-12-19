using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ApplicationDbContext : DbContext
{
    public DbSet<HealthStatus> HealthStatuses { get; set; }
    public DbSet<AdoptionStatus> AdoptionStatuses { get; set; }
    public DbSet<AdopterPetTypePreference> AdopterPetTypePreferences { get; set; }
    public DbSet<AdopterPetGenderPreference> AdopterPetGenderPreferences { get; set; }
    public DbSet<AdopterPetSizePreference> AdopterPetSizePreferences { get; set; }
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 

        // Composite Key Configuration for AdopterPetTypePreference
        modelBuilder.Entity<AdopterPetTypePreference>()
            .HasKey(ap => new { ap.AdopterId, ap.PetTypeId });

        modelBuilder.Entity<AdopterPetGenderPreference>()
            .HasKey(entity => new { entity.AdopterId, entity.PetGenderId });

        modelBuilder.Entity<AdopterPetSizePreference>()
            .HasKey(entity => new {entity.AdopterId, entity.PetSizeId});
    }

}