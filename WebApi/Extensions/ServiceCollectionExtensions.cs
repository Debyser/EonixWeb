using ApplicationCore.Repositories;
using ApplicationCore.Services;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using WebApi.Services;

namespace WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
           services.AddDbContext<EonixWebApiContext>(opts =>
           opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("WebApi")));

        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IAddressService, AddressService>();

            //services.AddScoped<IPersonService, PersonService>();
        }
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
        }

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerService, LoggerManager>();
    }
}