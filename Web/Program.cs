using System.Text;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Implementations;
using Repository.Interface;
using Service.Implementation;
using Service.Interface;
using Web.Config;
using Web.Services;
using Web.Services.Interfaces;
using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseLazyLoadingProxies().
        UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Add this here


builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
    {
        // Disable username requirements (will still exist but won't be validated)
        options.User.AllowedUserNameCharacters = null;
        options.User.RequireUniqueEmail = true;  // Use email as primary identifier instead

        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;

        // Disable two-factor authentication
        options.Tokens.ProviderMap.Remove("Default");
        options.Tokens.ProviderMap.Remove("Email");
        options.Tokens.ProviderMap.Remove("Phone");

        options.User.RequireUniqueEmail = true;

        // Disable phone number confirmation
        options.SignIn.RequireConfirmedPhoneNumber = false;

        // Disable lockout
        options.Lockout.AllowedForNewUsers = false;

        // Disable email confirmation requirement
        options.SignIn.RequireConfirmedEmail = false;

        // Disable account confirmation
        options.SignIn.RequireConfirmedAccount = false;



    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


//JWT Config
builder.Services.Configure<JWTConfig>(builder.Configuration.GetSection("JWTConfig"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWTConfig:Issuer"],
        ValidAudience =  builder.Configuration["JWTConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWTConfig:SecretKey"]))
    };
});
builder.Services.AddScoped<JWTService>();
builder.Services.AddScoped<AuthenticationService>();



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
builder.Services.AddScoped<IVeterinarianSpecializationRepository, VeterinarianSpecializationRepository>();


builder.Services.AddScoped<IPetTypeService, PetTypeService>();
builder.Services.AddScoped<IAdoptionService, AdoptionService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IVeterinarianService, VeterinarianService>();
builder.Services.AddScoped<IVaccinationService, VaccinationService>();
builder.Services.AddScoped<IPetSizeService, PetSizeService>();
builder.Services.AddScoped<IPetGenderService, PetGenderService>();
builder.Services.AddScoped<IHealthStatusService, HealthStatusService>();
builder.Services.AddScoped<IMedicalConditionService, MedicalConditionService>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
builder.Services.AddScoped<IOwnerPetListingService, OwnerPetListingService>();
builder.Services.AddScoped<IOwnerPetListingDocumentService, OwnerPetListingDocumentService>();
builder.Services.AddScoped<IAdoptionStatusService, AdoptionStatusService>();
builder.Services.AddScoped<IOwnerSurrenderReasonService, OwnerSurrenderReasonService>();
builder.Services.AddScoped<IVeterinarianSpecializationService, VeterinarianSpecializationService>();
builder.Services.AddScoped<IShelterPetListingService, ShelterPetListingService>();
builder.Services.AddScoped<IAdopterService, AdopterService>();
builder.Services.AddScoped<IShelterService, ShelterService>();

// File Service
builder.Services.AddScoped<IUploadService, UploadService>();

builder.Services.AddCors();

// Controllers 
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

DatabaseSeeder.SeedData(app.Services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

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