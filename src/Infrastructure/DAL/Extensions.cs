using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.DAL
{
    public static class Extensions
    {
        private const string OptionsSectionName = "MariaDB";
        public static void AddMariaDB(this IServiceCollection services, IConfiguration configuration)
        {
            // komendy do dla KMP:
            // - usuwanie bazy danych:
            // Drop-Database -Context IdentityContext -Project Infrastructure
            // - tworzenia migracji:
            // Add-Migration InitIdentityData -Context IdentityContext -Project Infrastructure
            // - aktualizacji bazy danych na podstawie migracji:
            // Update-Database InitIdentityData -Context IdentityContext -Project Infrastructure
            // - usunięcia ostatniej migracji:
            // Add-Migration -Context IdentityContext -Project Infrastructure


            services.Configure<MariaDBOptions>(configuration.GetRequiredSection(OptionsSectionName));
            var mariaDBOptions = configuration.GetOptions<MariaDBOptions>(OptionsSectionName);
            var serverVersion = new MariaDbServerVersion(new Version(10, 11, 2));
            services.AddDbContext<IdentityContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(mariaDBOptions.ConnectionStringForIdentity, serverVersion)
                // The following three options help with debugging, but should
                // be changed or removed for production.
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
        );
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var options = new T();
            var section = configuration.GetRequiredSection(sectionName);
            section.Bind(options);

            return options;
        }
    }
}
