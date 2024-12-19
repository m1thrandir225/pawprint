using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ApplicationDbContext : DbContext
{

    /*
     * Tables
     */

    public DbSet<HealthStatus> HealthStatuses { get; set; }
    public DbSet<AdoptionStatus> AdoptionStatuses { get; set; }

    public DbSet<AdopterPetTypePreference> AdopterPetTypePreferences { get; set; }
    public DbSet<AdopterPetGenderPreference> AdopterPetGenderPreferences { get; set; }
    public DbSet<AdopterPetSizePreference> AdopterPetSizePreferences { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<ShelterPetListing> ShelterPetListings { get; set; }
    public DbSet<OwnerPetListing> OwnerPetListings { get; set; }


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

        modelBuilder.Entity<OwnerPetListing>()
            .Property(e => e.Approved)
            .HasConversion<int>();

        modelBuilder.Entity<ShelterPetListing>()
            .Property(e => e.Approved)
            .HasConversion<int>();

    }

}