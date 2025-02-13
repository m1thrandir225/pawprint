using Domain;
using Domain.identity;
using Domain.Stripe;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    /**
     * Read Only Tables
     */
    public DbSet<AdoptionStatus> AdoptionStatuses { get; set; }
    public DbSet<HealthStatus> HealthStatuses { get; set; }
    public DbSet<MedicalCondition> MedicalConditions { get; set; }
    public DbSet<PetGender> PetGenders { get; set; }
    public DbSet<PetSize> PetSizes { get; set; }
    public DbSet<PetType> PetTypes { get; set; }

    /**
     * Adopter Preferences
     */
    public DbSet<AdopterPetGenderPreference> AdopterPetGenderPreferences { get; set; }
    public DbSet<AdopterPetSizePreference> AdopterPetSizePreferences { get; set; }
    public DbSet<AdopterPetTypePreference> AdopterPetTypePreferences { get; set; }
    /**
     * Owner Listings
     */
    public DbSet<OwnerPetListing> OwnerPetListings { get; set; }
    public DbSet<OwnerPetListingDocument> OwnerPetListingDocuments { get; set; }
    public DbSet<OwnerSurrenderReason> OwnerSurrenderReasons { get; set; }
    /**
     * Shelter Listings
     */
    public DbSet<Adoption> Adoptions { get; set; }
    public DbSet<MedicalRecord> MedicalRecords { get; set; }
    public DbSet<ShelterPetListing> ShelterPetListings { get; set; }
    public DbSet<Vaccination> Vaccinations { get; set; }
    public DbSet<Veterinarian> Veterinarians { get; set; }
    public DbSet<VeterinarianSpecilization> VeterinarianSpecilizations { get; set; }

    /**
    * Pets
    */
    public DbSet<Pet> Pets { get; set; }

    /**
     * Identity Classes
     */
    public DbSet<User> Users { get; set; }
    public DbSet<Shelter> Shelters { get; set; }

    /**
     * Payment 
     */
    public DbSet<Payment> Payments { get; set; } 
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<Shelter>().ToTable("shelters");

        modelBuilder.Entity<ApplicationUser>()
            .UseTptMappingStrategy();
    }
}

