using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultipleConnectionString.Data;
using MultipleConnectionString.Repository;

namespace MultipleConnectionString
{
    internal static class Startup
    {
        public static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            var environmentName =
                Environment.GetEnvironmentVariable("CORE_ENVIRONMENT");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .Build();

            services.AddDbContext<ApplicationContext>(db => db.UseSqlServer("DefaultConnection"));
            services
                .AddSingleton<IDbContextFactorySource<ApplicationContext>,
                    DbContextFactorySource<ApplicationContext>>();
            services.AddSingleton(typeof(DynamicDbContextFactory<>)); // register as open generic type and let DI close the type during resolution

            services.AddScoped<IApplication, ApplicationSqlServer>();
            services.AddTransient<App>();

            return services;
        }
    }
}
