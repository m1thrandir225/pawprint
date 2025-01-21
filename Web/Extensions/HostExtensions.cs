using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace Web.Extensions
{
    public static class HostExtensions
    {
        public static IHost ApplyMigrations(this IHost host)
        {
         using(var scope = host.Services.CreateScope())
         {
            var services = scope.ServiceProvider;
            try 
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }catch (Exception ex)
            {
                    throw new Exception("An error occurred while applying migrations.", ex);
            }
         }   
         return host;
        }
    }
}