using Microsoft.EntityFrameworkCore;
using TatAisEnergo.DataAccess;
using TatAisEnergo.DataAccess.DbInitializer;

namespace TatAisEnergo.WebApi
{
    internal static class ApiInstaller
    {
        public static WebApplicationBuilder ConfigureAppConfiguration(this WebApplicationBuilder builder)
        {
            builder.Configuration.Sources.Clear();
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder;
        }

        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {

            return services;
        }

        public static WebApplication MigrateDb(this WebApplication app)
        {
            var factory = app.Services.GetRequiredService<IServiceScopeFactory>();
            using var scope = factory.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
            
            DbInitializer.SeedTestData(db);

            return app;
        }
    }
}