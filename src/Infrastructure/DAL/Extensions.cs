using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DAL
{
    public static class Extensions
    {
        private const string OptionsSectionName = "MariaDB";
        public static void AddMariaDB(this IServiceCollection services, IConfiguration configuration)
        {
            var mariaDBOptions = configuration.GetOptions<MariaDBOptions>(OptionsSectionName);

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
