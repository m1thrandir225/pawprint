using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Implementations;
using Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies());

builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Repos
builder.Services.AddScoped<IAdopterPetGenderPreferenceRepository, AdopterPetGenderPreferenceRepository>();
builder.Services.AddScoped<IAdopterPetSizePreferenceRepository, AdopterPetSizePreferenceRepository>();
builder.Services.AddScoped<IAdopterPetTypePreferenceRepository, AdopterPetTypePreferenceRepository>();
builder.Services.AddScoped<IAdoptionRepository, AdoptionRepository>(); // THIS IS THE LINE I MESSED UP
builder.Services.AddScoped<IAdoptionStatusRepository, AdoptionStatusRepository>();
builder.Services.AddScoped<IHealthStatusRepository, HealthStatusRepository>();
builder.Services.AddScoped<IMedicalConditionRepository, MedicalConditionRepository>();
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
builder.Services.AddScoped<IOwnerPetListingDocumentRepository, OwnerPetListingDocumentRepository>();
builder.Services.AddScoped<IOwnerPetListingRepository, OwnerPetListingRepository>();
builder.Services.AddScoped<IOwnerSurrenderReasonRepository, OwnerSurrenderReasonRepository>();
builder.Services.AddScoped<IPetGenderRepository, PetGenderRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IPetSizeRepository, PetSizeRepository>();
builder.Services.AddScoped<IPetTypeRepository, PetTypeRepository>();
builder.Services.AddScoped<IShelterPetListingRepository, ShelterPetListingRepository>();
builder.Services.AddScoped<IShelterRepository, ShelterRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVaccinationRepository, VaccinationRepository>();
builder.Services.AddScoped<IVeterinarianRepository, VeterinarianRepository>();
builder.Services.AddScoped<IVeterinarianSpecilizationRepository, VeterinarianSpecilizationRepository>();


// Controllers 

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    var roles = new[] { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(role));
        }
    }

}

app.Run();