using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TatAisEnergo.DataAccess
{
    public static class DataAccessInstaller
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                 .AddDbContext<AppDbContext>(options =>
                 {
                     options.UseNpgsql(
                         configuration.GetConnectionString("Main"),
                         builder =>
                         {
                             builder.SetPostgresVersion(new Version(15, 7));
                         });
                 });

            return services;
        }
    }
}