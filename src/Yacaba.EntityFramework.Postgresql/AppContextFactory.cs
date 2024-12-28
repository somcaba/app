using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Yacaba.EntityFramework.Postgresql {
    public class AppContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext> {
        public ApplicationDbContext CreateDbContext(String[] args) {
            IConfiguration configuration = new ConfigurationBuilder()
                           .SetBasePath($"{Directory.GetCurrentDirectory()}/../Yacaba.Web/")
                           .AddEnvironmentVariables("YACABA_")
                           .AddJsonFile("appsettings.json")
                           .AddJsonFile("appsettings.Development.json")
                           .Build();

            var manager = new ConfigurationManager();
            manager.AddConfiguration(configuration);

            var optionsBUilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            String? connectionString = manager.GetConnectionString("postgresdb");
            optionsBUilder.UseNpgsql(connectionString, p => p.MigrationsAssembly("Yacaba.EntityFramework.Postgresql"));
            optionsBUilder.EnableSensitiveDataLogging();
            optionsBUilder.UseOpenIddict();

            return new ApplicationDbContext(optionsBUilder.Options);
        }
    }
}
