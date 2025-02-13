using System.Text;
using Domain;
using Domain.identity;
using Domain.Stripe;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Implementations;
using Repository.integration;
using Repository.Interface;
using Repository.Stripe;
using Service.Implementation;
using Service.integration;
using Service.Interface;
using Service.Stripe;
using Stripe;
using Web.Config;
using Web.Services;
using Web.Services.Interfaces;
using Web.Extensions;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureEnvironment(builder);
        ConfigureKestrel(builder);
        ConfigureServices(builder.Services);
        ConfigureIdentity(builder.Services);
        ConfigureJWT(builder);
        RegisterRepositories(builder.Services);
        RegisterServices(builder.Services);
        ConfigureControllers(builder);

        builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));
        
        //For Restaurant
        Integration(builder);
        
        var app = builder.Build();
        ConfigureMiddleware(app);
        ConfigureEndpoints(app);
        SeedRoles(app).Wait();

        app.Run();


    }

    private static void ConfigureEnvironment(WebApplicationBuilder builder)
    {
        DotNetEnv.Env.Load();
        var requiredVars = new[]
        {
            "DB_CONNECTION_URL",
            "SMTP_HOST",
            "SMTP_PORT",
            "SMTP_USERNAME",
            "SMTP_PASSWORD",
            "SMTP_SENDER_NAME",
            "SMTP_SENDER_EMAIL",
            "STRIPE_PUBLIC_KEY",
            "STRIPE_SECRET_KEY",
        };

        foreach (var envVar in requiredVars)
        {
            if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(envVar)))
            {
                throw new Exception($"Missing required environment variable: {envVar}");
            }
        }

        builder.Configuration.AddEnvironmentVariables();

    }

    public static void ConfigureKestrel(WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = 50 * 1024 * 1024);
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_URL");
        // Configure EmailSettings
        services.AddTransient<EmailSettings>();
        // Register email services
        services.AddSingleton<IEmailTemplateService, EmailTemplateService>();
        services.AddSingleton<IEmailService, EmailService>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseLazyLoadingProxies().
                UseNpgsql(connectionString)
        );
        services.AddControllersWithViews();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHttpContextAccessor();
        services.AddCors();
    }

    public static void ConfigureIdentity(IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.User.AllowedUserNameCharacters = null;
                options.User.RequireUniqueEmail = true;

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
    }

    public static void ConfigureJWT(WebApplicationBuilder builder)
    {
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
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
       services.AddTransient<IAdopterPetGenderPreferenceRepository, AdopterPetGenderPreferenceRepository>();
       services.AddTransient<IAdopterPetSizePreferenceRepository, AdopterPetSizePreferenceRepository>();
       services.AddTransient<IAdopterPetTypePreferenceRepository, AdopterPetTypePreferenceRepository>();
       services.AddTransient<IAdoptionRepository, AdoptionRepository>();
       services.AddTransient<IAdoptionStatusRepository, AdoptionStatusRepository>();
       services.AddTransient<IHealthStatusRepository, HealthStatusRepository>();
       services.AddTransient<IMedicalConditionRepository, MedicalConditionRepository>();
       services.AddTransient<IMedicalRecordRepository, MedicalRecordRepository>();
       services.AddTransient<IOwnerPetListingDocumentRepository, OwnerPetListingDocumentRepository>();
       services.AddTransient<IOwnerPetListingRepository, OwnerPetListingRepository>();
       services.AddTransient<IOwnerSurrenderReasonRepository, OwnerSurrenderReasonRepository>();
       services.AddTransient<IPetGenderRepository, PetGenderRepository>();
       services.AddTransient<IPetRepository, PetRepository>();
       services.AddTransient<IPetSizeRepository, PetSizeRepository>();
       services.AddTransient<IPetTypeRepository, PetTypeRepository>();
       services.AddTransient<IShelterPetListingRepository, ShelterPetListingRepository>();
       services.AddTransient<IShelterRepository, ShelterRepository>();
       services.AddTransient<IUserRepository, UserRepository>();
       services.AddTransient<IVaccinationRepository, VaccinationRepository>();
       services.AddTransient<IVeterinarianRepository, VeterinarianRepository>();
       services.AddTransient<IVeterinarianSpecializationRepository, VeterinarianSpecializationRepository>();
       
       //Stripe
       services.AddTransient<IPaymentRepository, PaymentRepository>();
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<IPetTypeService, PetTypeService>();
        services.AddTransient<IAdoptionService, AdoptionService>();
        services.AddTransient<IPetService, PetService>();
        services.AddTransient<IVeterinarianService, VeterinarianService>();
        services.AddTransient<IVaccinationService, VaccinationService>();
        services.AddTransient<IPetSizeService, PetSizeService>();
        services.AddTransient<IPetGenderService, PetGenderService>();
        services.AddTransient<IHealthStatusService, HealthStatusService>();
        services.AddTransient<IMedicalConditionService, MedicalConditionService>();
        services.AddTransient<IMedicalRecordService, MedicalRecordService>();
        services.AddTransient<IOwnerPetListingService, OwnerPetListingService>();
        services.AddTransient<IOwnerPetListingDocumentService, OwnerPetListingDocumentService>();
        services.AddTransient<IAdoptionStatusService, AdoptionStatusService>();
        services.AddTransient<IOwnerSurrenderReasonService, OwnerSurrenderReasonService>();
        services.AddTransient<IVeterinarianSpecializationService, VeterinarianSpecializationService>();
        services.AddTransient<IShelterPetListingService, ShelterPetListingService>();
        services.AddTransient<IAdopterService, AdopterService>();
        services.AddTransient<IShelterService, ShelterService>();
        services.AddTransient<IUploadService, UploadService>();
        services.AddTransient<IUserContextService, UserContextService>();
        
        //Stripe
        services.AddSingleton(new StripeClient(Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY")));
        services.AddTransient<IPaymentService, PaymentService>();
    }

    private static void ConfigureControllers(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });
    }

    private static void ConfigureMiddleware(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(); // Enable Swagger
            app.UseSwaggerUI();
            app.ApplyMigrations();
            DatabaseSeeder.SeedData(app.Services);
        }



        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //app.UseHsts();
        }

        // app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials());

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

    }

    private static void ConfigureEndpoints(WebApplication app)
    {
        app.MapControllers();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}");
    }

    private static async Task SeedRoles(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var roles = new[] { UserRole.Admin, UserRole.User, UserRole.Shelter};

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }
    }

    
    private static void Integration(WebApplicationBuilder builder)
    {
        // The connection string for the integration (FoodDelivery) database.
        var integrationConnectionString = "Data Source=tcp:fooddelivery-webdbserver.database.windows.net,1433;Initial Catalog=FoodDelivery_db;User Id=korisnik@fooddelivery-webdbserver;Password=fooddelivery1*";
    
        // Register the IntegrationDbContext (using SQL Server here)
        builder.Services.AddDbContext<IntegrationDbContext>(options =>
            options.UseSqlServer(integrationConnectionString));

        // Register the integration-layer repositories and services.
        // (Make sure that these interfaces and classes exist in your project.)
        builder.Services.AddTransient<IRestaurantRepository, RestaurantRepository>();
        builder.Services.AddTransient<IRestaurantService, RestaurantService>();

        // Register a hosted service that will run at startup and apply any pending migrations 
        // on the Integration database.
        // builder.Services.AddHostedService<IntegrationDbInitializer>();
    }
}
