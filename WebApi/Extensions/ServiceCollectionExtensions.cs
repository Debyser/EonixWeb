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
            services.AddDbContext<EonixWebApiDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("WebApi")));
        public static void ConfigurePersonService(this IServiceCollection services) =>
            services.AddScoped<IPersonService, PersonService>();
        public static void ConfigurePersonRepository(this IServiceCollection services) =>
            services.AddScoped<IPersonRepository, PersonRepository>();

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerService, LoggerManager>();
    }
}