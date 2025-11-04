using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace E_Commerce.Extensions
{
    public static class WebApplicationRegisteration
    {
        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();

            var DbContextService = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            var pendingMigrations = await DbContextService.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
               await DbContextService.Database.MigrateAsync();
            return app;

        }

        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var DataIntializerService = scope.ServiceProvider.GetRequiredService<IDataIntializer>();
            await DataIntializerService.IntializeAsync();
            return app;
        }
    }
}
