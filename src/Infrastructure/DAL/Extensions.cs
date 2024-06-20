using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using Pomelo.EntityFrameworkCore.MySql.Internal;

namespace Infrastructure.DAL
{
    public static class Extensions
    {
        private const string OptionsSectionName = "MariaDB";
        public static void AddMariaDB(this IServiceCollection services, IConfiguration configuration)
        {
            // komendy do dla EF tools:
            // - usuwanie bazy danych:
            // Drop-Database -Context IdentityContext -Project Infrastructure
            // - tworzenie migracji:
            // Add-Migration InitIdentityData -Context IdentityContext -Project Infrastructure
            // - aktualizacja bazy danych na podstawie migracji:
            // Update-Database InitIdentityData -Context IdentityContext -Project Infrastructure
            // - usunięcie ostatniej migracji:
            // Add-Migration -Context IdentityContext -Project Infrastructure
            services.Configure<MariaDBOptions>(configuration.GetRequiredSection(OptionsSectionName));
            var mariaDBOptions = configuration.GetOptions<MariaDBOptions>(OptionsSectionName);
            var serverVersion = new MariaDbServerVersion(new Version(mariaDBOptions.VersionMajor, mariaDBOptions.VersionMinor, mariaDBOptions.VersionBuild));
            //services.AddDbContext<ApplicationDbContext>(
            //    dbContextOptions => dbContextOptions
            //    .UseMySql(mariaDBOptions.ConnectionStringForApplicationDb, serverVersion)
            //    // The following three options help with debugging, but should
            //    // be changed or removed for production.
            //    .LogTo(Console.WriteLine, LogLevel.Information)
            //    .EnableSensitiveDataLogging()
            //    .EnableDetailedErrors()
            //);

            services.AddDbContext<IdentityContext>(            
                dbContextOptions => dbContextOptions
                .UseMySql(mariaDBOptions.ConnectionStringForIdentity, serverVersion/*,
                mySqlOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null
                        );
                }*/)
                // The following three options help with debugging, but should
                // be changed or removed for production.
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
            );
            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.Stores.ProtectPersonalData = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<IdentityContext>();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddSession();
        }

        public static void UseInfrastructure(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseAuthorization();
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
