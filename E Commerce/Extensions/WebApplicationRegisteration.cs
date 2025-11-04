using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace E_Commerce.Extensions
{
    public static class WebApplicationRegisteration
    {
        public static WebApplication MigrateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var DbContextService = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            if (DbContextService.Database.GetPendingMigrations().Any())
                DbContextService.Database.Migrate();
            return app;

        }

        public static WebApplication SeedDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var DataIntializerService = scope.ServiceProvider.GetRequiredService<IDataIntializer>();
            DataIntializerService.Intialize();
            return app;
        }
    }
}
