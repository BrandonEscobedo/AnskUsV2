using anskus.Infrestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace anskus.WebApi.ExtensionMigration
{
    public static class MigrationExtension
    {
        public static void ApplyMigration(this WebApplication application)
        {
            using var scope = application.Services.CreateScope();
            var dbcontext = scope.ServiceProvider.GetRequiredService<AnskusDbContext>();
            dbcontext.Database.Migrate();
        }
    }
}
