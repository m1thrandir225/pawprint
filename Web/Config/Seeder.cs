using System.Drawing;
using Bogus;
using Domain;
using Domain.enums;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol;
using Repository;

public static class DatabaseSeeder
{
    const int numberOfUsers = 10;
    const int numberOfPetsAndRelated = 20;
    public static void SeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();



        // Seed Roles
        SeedRoles(roleManager).Wait();

        //Enum Types
        var petTypes = createPetTypes();
        var petSizes = createPetSizes();
        var petGenders  = createPetGenders();
        var healthStatuses = createHealthStatuses();
        var adoptionStatuses = createAdoptionStatuses();
        var surrenderReasons = createOwnerSurrenderReasons();

        // Seed Basic Data
        SeedPetTypes(context, petTypes);
        SeedPetSizes(context, petSizes);
        SeedPetGenders(context, petGenders);
        SeedAdoptionStatuses(context, adoptionStatuses);
        SeedHealthStatuses(context, healthStatuses);
        SeedOwnerSurrenderReasons(context, surrenderReasons);

        //Data Types
        //user 
        var users = new User[numberOfUsers];
        var shelters = new Shelter[numberOfUsers];
        // Seed Sample Users and Shelters
        SeedUsersAndShelters(userManager, context, shelters, users).Wait();
         
        //pet ( needs type, gender, health status, adoption status)
        var pets = new Pet[numberOfPetsAndRelated];
        SeedPets(context, petSizes, petGenders, petTypes, healthStatuses, adoptionStatuses, pets);
        //adoption (needs pet and adopter)
        SeedAdoptions(context, users, pets);
        //veterinarian, veterinarian specialization ( needs veterinarian )
        var veterinarians = new Veterinarian[numberOfPetsAndRelated];
        SeedVeterinariansAndSpecializations(context, veterinarians);
        //medical record (needs veterinarian), medical condition (needs medical record)
        var medicalRecords = new MedicalRecord[numberOfPetsAndRelated];
        SeedMedicalRecordsAndConditions(context, veterinarians, medicalRecords);
        //vaccinatio
        SeedVaccinactions(context, medicalRecords);
        //shelter listing (first half of pets and medical records)
        var firstHalfPets = pets.Take(10).ToArray();
        SeedShelterPetListings(context, shelters, firstHalfPets, medicalRecords);
        //owner listing (second half of pets and medical records)
        var secondHalfPets = pets.Skip(10).Take(10).ToArray();
        SeedOwnerPetListings(context, users, secondHalfPets, surrenderReasons);
        
        // Save changes to ensure IDs are generated
         context.SaveChanges();
    }

    public static PetGender[] createPetGenders(){
        var genders = new PetGender[]{
            new PetGender("Male"),
            new PetGender("Female")
        };
        return genders;
    }

    public static PetSize[] createPetSizes(){
        var sizes = new PetSize[]{
            new PetSize("Small"),
            new PetSize("Medium"),
            new PetSize("Large"),
            new PetSize("Extra Large")
        };
        return sizes;
    }

    public static PetType[] createPetTypes(){
        var type = new PetType[]{
            new PetType("Cat"),
            new PetType("Dog"),
            new PetType("Fish"),
            new PetType("Rodent"),
            new PetType("Bird"),
            new PetType("Horse"),
            new PetType("Turtle"),
            new PetType("Reptile"),
            new PetType("Hamster"),
        };
        return type;
    }

    public static AdoptionStatus[] createAdoptionStatuses(){
        var adoptionStatuses = new AdoptionStatus[]{
            new AdoptionStatus("Available"),
            new AdoptionStatus("Pending"),
            new AdoptionStatus("Adopted"),
            new AdoptionStatus("Not Available"),
            new AdoptionStatus("Trial Adoption"),

        };

        return adoptionStatuses;
    }
    
    public static HealthStatus[] createHealthStatuses(){
        var healthStatuses = new HealthStatus[]{
            new HealthStatus("Healthy"),
            new HealthStatus("Recovering"),
            new HealthStatus("Chronic Condition"),
            new HealthStatus("Senior Health"),
            new HealthStatus("Quarantined"),
            new HealthStatus("Sick"),
            new HealthStatus("Injured"),
        };
        return healthStatuses;
    }

    public static OwnerSurrenderReason[] createOwnerSurrenderReasons(){
        var ownerSurrenderReasons = new OwnerSurrenderReason[]{
            new OwnerSurrenderReason("Financial Constraints"),
            new OwnerSurrenderReason("Behavioral Issues"),
            new OwnerSurrenderReason("Allergies"),
            new OwnerSurrenderReason("Moving"),
            new OwnerSurrenderReason("Family Changes"),
            new OwnerSurrenderReason("Time Constraints"),
            new OwnerSurrenderReason("Compatibility Issues"),
            new OwnerSurrenderReason("Death of Owner"),
            
        };

        return ownerSurrenderReasons;
    }

    public static User createBasicUser()
    {
        var homeTypes = new[]
        {
            "Flat",
            "House"
        };

        var user = new Faker<User>()
        .RuleFor(x => x.Id, _ => Guid.NewGuid())
        .RuleFor(x => x.FirstName, f => f.Person.FirstName)
        .RuleFor(x => x.LastName, f => f.Person.LastName)
        .RuleFor(x => x.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
        .RuleFor(x => x.EmailConfirmed, _ => true)
        .RuleFor(x => x.HasChildren, f => f.Random.Bool())
        .RuleFor(x => x.HasOtherPets, f => f.Random.Bool())
        .RuleFor(x => x.HomeType, f => f.PickRandom(homeTypes))
        .RuleFor(x => x.CreatedAt, _ => DateTime.UtcNow)
        .RuleFor(x => x.Address, f => f.Address.StreetAddress())
        ;
        return user.Generate();
    }

    public static AdopterPetGenderPreference createPetGenderPreference(User u, PetGender gender)
    {
        var preference = new Faker<AdopterPetGenderPreference>()
        .RuleFor(x => x.Id, _ => Guid.NewGuid())
        .RuleFor(x => x.AdopterId, _ => u.Id)
        .RuleFor(x => x.PetGenderId, _ => gender.Id);

        return preference.Generate();
    }

       public static AdopterPetSizePreference createPetSizePreference(User u, PetSize size)
    {
        var preference = new Faker<AdopterPetSizePreference>()
        .RuleFor(x => x.Id, _ => Guid.NewGuid())
        .RuleFor(x => x.AdopterId, _ => u.Id)
        .RuleFor(x => x.PetSizeId, _ => size.Id);

        return preference.Generate();
    }
    
       public static AdopterPetTypePreference createPetTypePreference(User u, PetType type)
    {
        var preference = new Faker<AdopterPetTypePreference>()
        .RuleFor(x => x.Id, _ => Guid.NewGuid())
        .RuleFor(x => x.AdopterId, _ => u.Id)
        .RuleFor(x => x.PetTypeId, _ => type.Id);

        return preference.Generate();
    }
    
    private static Shelter createBasicShelter()
    {
        var shelter = new Faker<Shelter>()
        .RuleFor(x => x.Id, _ => Guid.NewGuid())
        .RuleFor(x => x.Name, f => f.Name.FirstName())
        .RuleFor(x => x.Email, (f, u) => f.Internet.Email(u.Name))
        .RuleFor(x => x.Website, f => f.Internet.UrlWithPath())
        .RuleFor( x => x.EmailConfirmed, _ => true)
        .RuleFor(x => x.CreatedAt, _ => DateTime.UtcNow)
        .RuleFor(x => x.capacity, f => f.Random.Number(0, 150))
        .RuleFor(x => x.Address, f => f.Address.StreetAddress())
        .RuleFor(x => x.isNoKill, f => f.Random.Bool())
        ;
        return shelter.Generate();
    }

    public static Veterinarian createBasicVeterinarian()
    {
        var veterinarian = new Faker<Veterinarian>()
        .RuleFor(x => x.Id, _ => Guid.NewGuid())
        .RuleFor(x => x.Name, f => f.Person.FullName)
        .RuleFor(x => x.Email, (f, u) => f.Internet.Email(u.Name))
        .RuleFor(x => x.ContactNumber, f => f.Phone.PhoneNumber())
        .RuleFor(x => x.ClinicName, f => f.Person.FirstName)
        .RuleFor(x => x.CreatedAt, _ => DateTime.UtcNow);


        return veterinarian.Generate();
    }

    public static VeterinarianSpecilization createBasicVeterinarianSpecialization(Veterinarian v)
    {
        var specialization = new Faker<VeterinarianSpecilization>()
        .RuleFor(x => x.Id, _ => Guid.NewGuid())
        .RuleFor(x => x.VeterinarianId, _ => v.Id)
        .RuleFor(x => x.Specialization, f => f.Person.UserName)
        ;

        return specialization.Generate();
    } 

    public static MedicalRecord createBasicMedicalRecord(Veterinarian v)
    {
        var medicalRecord = new Faker<MedicalRecord>()
        .RuleFor(x => x.Id, _ => Guid.NewGuid())
        .RuleFor(x => x.CreatedAt, _ => DateTime.UtcNow)
        .RuleFor(x => x.VetId, _  => v.Id)
        .RuleFor(x => x.SpayNeuterStatus, f => f.Random.Bool())
        ;

        return medicalRecord.Generate();
    }

    public static Vaccination createBasicVaccination(MedicalRecord mr)
    {
        var vaccination = new Faker<Vaccination>()
        .RuleFor(x => x.Id, _ => Guid.NewGuid())
        .RuleFor(x => x.MedicalRecordId, _ => mr.Id)
        .RuleFor(x => x.VaccineName, f => "example")
        .RuleFor(x => x.VaccineDate, f => DateOnly.FromDateTime(DateTime.Now))
        ;

        return vaccination.Generate();
    }

    public static MedicalCondition createBasicMedicalCondition(MedicalRecord mr)
    {
        var condition = new Faker<MedicalCondition>()
        .RuleFor(x => x.Id, _ => Guid.NewGuid())
        .RuleFor(x => x.MedicalRecordId, _ => mr.Id)
        .RuleFor(x => x.ConditionName, f => f.Lorem.Paragraph(1))
        .RuleFor(x => x.Notes, f => f.Lorem.Paragraph(5))
        ;

        return condition.Generate();
    }

    public static Pet createBasicPet(
        PetType type,
        PetGender gender,
        PetSize size,
        HealthStatus healthStatus,
        AdoptionStatus adoptionStatus
    )
    
    {
        var breeds = new string[] {
            "Breed 1",
            "Breed 2",
            "Breed 3",
            "Breed 4",
        };
        var pet = new Faker<Pet>()
        .RuleFor(x => x.Id, _ => Guid.NewGuid())
        .RuleFor(x => x.Name, f => f.Person.FullName)
        .RuleFor(x => x.Breed, f => f.PickRandom(breeds))
        .RuleFor(x => x.AvatarImg, f => "https://placehold.co/600x400")
        .RuleFor(x => x.ImageShowcase, f => new string[2]{
            "https://placehold.co/600x400",
            "https://placehold.co/600x400",
        })
        .RuleFor(x => x.AgeYears, f => f.Random.Int(1, 20))
        .RuleFor(x => x.AdoptionStatusId, _ => adoptionStatus.Id)
        .RuleFor(x => x.HealthStatusId, _ => healthStatus.Id)
        .RuleFor(x => x.PetSizeId, _ => size.Id)
        .RuleFor(x => x.PetGenderId, _ => gender.Id)
        .RuleFor(x => x.PetTypeId, _ => type.Id)
        .RuleFor(x => x.GoodWithCats, f => f.Random.Bool())
        .RuleFor(x => x.GoodWithChildren, f => f.Random.Bool())
        .RuleFor(x => x.GoodWithDogs, f => f.Random.Bool())
        .RuleFor(x => x.EnergyLevel, f => f.Random.Int(1, 5))
        .RuleFor(x => x.SpecialRequirements, f => f.Lorem.Paragraph(2))
        .RuleFor(x => x.BehaviorialNotes, f => f.Lorem.Paragraph(2))
        .RuleFor(x => x.IntakeDate, f => f.Date.Soon())
        ;

        return pet.Generate();
    }

    public static Adoption createBasicAdoption(Pet p, User adopter)
    {
        var adoption = new Faker<Adoption>()
        .RuleFor(x => x.Id, _ => Guid.NewGuid())
        .RuleFor(x => x.CreatedAt, _ => DateTime.UtcNow)
        .RuleFor(x => x.PetId, _ => p.Id)
        .RuleFor(x => x.AdopterId, _ => adopter.Id)
        .RuleFor(x => x.AdoptionDate, f => f.Date.Recent())
        .RuleFor(x => x.AdoptionFee, f => f.Random.Decimal(1, 100_000_000))
        .RuleFor(x => x.FollowUpDate, f => f.Date.Soon())
        .RuleFor(x => x.CounselorNotes, f => f.Lorem.Paragraph(1))
        .RuleFor(x => x.IsSuccessful, f => f.Random.Bool())
        ;

        return adoption.Generate();
    }

    public static ShelterPetListing createBasicShelterListing(
        Shelter s, 
        Pet p, 
        MedicalRecord mr
        )
    {
        var shelterListing = new Faker<ShelterPetListing>()
        .RuleFor(x => x.Id, _ => Guid.NewGuid())
        .RuleFor(x => x.MedicalRecordId, _ => mr.Id)
        .RuleFor(x => x.PetId, _ => p.Id)
        .RuleFor(x => x.ShelterId, _ => s.Id)
        .RuleFor(x => x.IntakeDate, f => DateOnly.FromDateTime(f.Date.Between(DateTime.Now, f.Date.Future())))
        .RuleFor(x => x.Approved, f => f.Random.Enum<ApprovalStatus>())
        ;

        return shelterListing.Generate();
    }

    public static OwnerPetListing createBasicOwnerPetListing(
        User adopter, 
        Pet p,
        OwnerSurrenderReason reason
    )
    {
        var ownerPetListing = new Faker<OwnerPetListing>()
        .RuleFor(x => x.Id, _ => Guid.NewGuid())
        .RuleFor(x => x.SubmissionDate, f => f.Date.Recent().ToUniversalTime())
        .RuleFor(x => x.ReviewDate, f => f.Date.Soon().ToUniversalTime())
        .RuleFor(x => x.SurrenderReasonId, _ => reason.Id)
        .RuleFor(x => x.PetId, _ => p.Id)
        .RuleFor(x => x.AdopterId, _ => adopter.Id)
        .RuleFor(x => x.Approved, f => f.Random.Enum<ApprovalStatus>())
        ;

        return ownerPetListing.Generate();
    }

    public static OwnerPetListingDocument createBasicOwnerPetListingDocument(
        OwnerPetListing listing
    )
    {
        var documentTypes = new string[] {
            "Image",
            "PDF",
            "DOCX",
            "MARKDOWN"
        };
        var document = new Faker<OwnerPetListingDocument>()
        .RuleFor(x => x.Id, _ => Guid.NewGuid())
        .RuleFor(x => x.ListingId, _ => listing.Id)
        .RuleFor(x => x.DocumentUrl, f => "https://randomurl.com")
        .RuleFor(x => x.DocumentType, f => f.PickRandom(documentTypes))
        .RuleFor(x => x.UploadedAt, _ => DateTime.UtcNow)
        ;

        return document.Generate();

    }

    private static async Task SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
    {
        string[] roles = { UserRole.Admin, UserRole.User };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }
    }

    private static void SeedPetTypes(ApplicationDbContext context, PetType[] types)
    {
        if (!context.PetTypes.Any())
        {
            context.PetTypes.AddRange(types);
            context.SaveChanges();
        }
    }

    private static void SeedPetSizes(ApplicationDbContext context, PetSize[] sizes)
    {
        if (!context.PetSizes.Any())
        {
            context.PetSizes.AddRange(sizes);
            context.SaveChanges();
        }
    }

    private static void SeedPetGenders(ApplicationDbContext context, PetGender[] genders)
    {
        if (!context.PetGenders.Any())
        {
            context.PetGenders.AddRange(genders);
            context.SaveChanges();
        }
    }

    private static void SeedAdoptionStatuses(ApplicationDbContext context, AdoptionStatus[] adoptionStatuses)
    {
        if (!context.AdoptionStatuses.Any())
        {
            context.AdoptionStatuses.AddRange(adoptionStatuses);
            context.SaveChanges();
        }
    }

    private static void SeedHealthStatuses(ApplicationDbContext context, HealthStatus[] healthStatuses)
    {
        if (!context.HealthStatuses.Any())
        {
            context.HealthStatuses.AddRange(healthStatuses);
            context.SaveChanges();
        }
    }

    private static void SeedOwnerSurrenderReasons(ApplicationDbContext context, OwnerSurrenderReason[] reasons)
    {
        if (!context.OwnerSurrenderReasons.Any())
        {
            context.OwnerSurrenderReasons.AddRange(reasons);
            context.SaveChanges();
        }
    }

    private static async Task SeedUsersAndShelters(
        UserManager<ApplicationUser> userManager, 
        ApplicationDbContext context,
        Shelter[] shelters,
        User[] users
        )
    {
        if (!context.Users.Any())
        {
            for(int i = 0; i < numberOfUsers; i++) {
                         
                var shelter = createBasicShelter();
                shelters[i] = shelter;
                await userManager.CreateAsync(shelter, "randomPassword123@!");
                await userManager.AddToRoleAsync(shelter, UserRole.User);

                await context.Shelters.AddAsync(shelter);
            }
  
            for(int i = 0; i < numberOfUsers; i++) {
                var user = createBasicUser();
                users[i] = user;
                await userManager.CreateAsync(user, "randomPassword123@");
                await userManager.AddToRoleAsync(user, i == 0 ? UserRole.Admin : UserRole.User);
                await context.Users.AddAsync(user);
            }
         context.SaveChanges();
        }
    }

    private static void SeedPets(
        ApplicationDbContext context,
        PetSize[] sizes,
        PetGender[] genders,
        PetType[] types,
        HealthStatus[] healthStatuses,
        AdoptionStatus[] adoptionStatuses,
        Pet[] pets
    )
    {
        if (!context.Pets.Any())
        {
            var faker = new Faker();
            for (int i = 0; i < numberOfPetsAndRelated; i++) {
                var gender = faker.PickRandom(genders);
                var type = faker.PickRandom(types);
                var size = faker.PickRandom(sizes);
                var healthStatus = faker.PickRandom(healthStatuses);
                var adoptionStatus = faker.PickRandom(adoptionStatuses);

                var pet = createBasicPet(type, gender, size, healthStatus, adoptionStatus);

                pets[i] = pet;

                context.Pets.Add(pet);
            }
            Console.WriteLine(pets.Length);
            context.SaveChanges();
        }
    }

    private static void SeedAdoptions(
        ApplicationDbContext context,
        User[] adopters, 
        Pet[] pets
    ) {
        if(!context.Adoptions.Any())
        {
            Console.WriteLine("Adoptions Pets Lenght: " + pets.Length);
            var faker = new Faker();
            for(int i = 0; i < 50; i++) {
                var adopter = faker.PickRandom(adopters);
                var pet = faker.PickRandom(pets);

                var adoption = createBasicAdoption(pet, adopter);

                context.Adoptions.Add(adoption);
            }
            context.SaveChanges();
        }
    }

    private static void SeedVeterinariansAndSpecializations(
        ApplicationDbContext context,
        Veterinarian[] veterinarians
    ) {
        if(!context.Veterinarians.Any()) {
            for (int i = 0; i < numberOfPetsAndRelated; i++) {
                var veterinarian = createBasicVeterinarian();

                veterinarians[i] = veterinarian;
                context.Veterinarians.Add(veterinarian);

                for (int j = 0; j < 3; j++) {
                    var specialization = createBasicVeterinarianSpecialization(veterinarian);

                    context.VeterinarianSpecilizations.Add(specialization);
                }
            }
            context.SaveChanges();
            
        }
    }

    private static void SeedMedicalRecordsAndConditions(
        ApplicationDbContext context,
        Veterinarian[] veterinarians,
        MedicalRecord[] records
    )
    {
        if(!context.MedicalRecords.Any())
        {
            for(int i = 0; i < numberOfPetsAndRelated; i++) {
                var veterinarian = veterinarians[i];

                var medicalRecord = createBasicMedicalRecord(veterinarian);

                records[i] = medicalRecord;

                context.MedicalRecords.Add(medicalRecord);

                for(int j = 0; j < 5; j++)
                {
                    var medicalCondition = createBasicMedicalCondition(medicalRecord);

                    context.MedicalConditions.Add(medicalCondition);
                }
                context.SaveChanges();

            }
        }
    }

    private static void SeedVaccinactions(
        ApplicationDbContext context,
        MedicalRecord[] records
    )
    {
        if(!context.Vaccinations.Any())
        {
            for(int i = 0; i < numberOfPetsAndRelated; i++)
            {
                var medicalRecord = records[i];

                var vaccination = createBasicVaccination(medicalRecord);

                context.Vaccinations.Add(vaccination);
            }
            context.SaveChanges();
        }
    }

    private static void SeedShelterPetListings(
        ApplicationDbContext context,
        Shelter[] shelters,
        Pet[] pets,
        MedicalRecord[] records
    )
    {
        if(!context.ShelterPetListings.Any())
        {
            for(int i = 0; i < numberOfUsers; i++) {
                var shelter = shelters[i];
                var pet = pets[i];
                var medicalRecord = records[i];

                var listing = createBasicShelterListing(shelter, pet, medicalRecord);

                context.ShelterPetListings.Add(listing);
            }
            context.SaveChanges();
        }
    }

    private static void  SeedOwnerPetListings(
        ApplicationDbContext context,
        User[] users,
        Pet[] pets,
        OwnerSurrenderReason[] reasons
    )
    {
        if(!context.OwnerPetListings.Any())
        {
            var faker = new Faker();
            for(int i = 0; i < numberOfUsers; i++) {
                var user = users[i];
                var pet = pets[i];
                var reason = faker.PickRandom(reasons);
                var listing  = createBasicOwnerPetListing(user, pet, reason);

                context.OwnerPetListings.Add(listing);

                for(int j = 0; j < 1; j++)
                {
                    var listingDocument = createBasicOwnerPetListingDocument(listing);

                    context.OwnerPetListingDocuments.Add(listingDocument);
                }
            }
            context.SaveChanges();
        }
    }
}