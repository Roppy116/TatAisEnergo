using Microsoft.EntityFrameworkCore.Design;
using TatAisEnergo.DataAccess;

namespace TatAisEnergo.WebApi.DesignTimeServices
{
    internal sealed class AppDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddDataAccessServices(configuration)
                .BuildServiceProvider();

            return serviceProvider.GetRequiredService<AppDbContext>();
        }
    }
}