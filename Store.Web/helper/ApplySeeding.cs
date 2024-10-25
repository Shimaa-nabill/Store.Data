using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Data.Contexts;
using Store.Data.Entities.IdentityEntity;
using Store.Repository;

namespace Store.Web.helper
{
    public class ApplySeeding
    {
        public static async Task ApplySeedingAsync (WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerfactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = services.GetRequiredService<StoreDbContext>();
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();

                    await StoreIdentityContextSeed.SeedUserAsync(userManager);


                    await context.Database.MigrateAsync();

                    await StoreContextSeed.SeedAsync(context, loggerfactory);
                    await StoreIdentityContextSeed.SeedUserAsync(userManager);

                }
                catch (Exception ex)
                {

                    var logger = loggerfactory.CreateLogger<ApplySeeding>();
                    logger.LogError(ex.Message);


                }
            }
        }
            
    }
}
