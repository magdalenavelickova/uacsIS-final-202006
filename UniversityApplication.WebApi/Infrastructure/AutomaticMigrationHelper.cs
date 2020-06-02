using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace UniversityApplication.WebApi.Infrastructure
{
    public static class AutomaticMigrationHelper
    {
        public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            var context = serviceScope.ServiceProvider.GetRequiredService<T>();
            context.Database.Migrate();
        }
    }
}
