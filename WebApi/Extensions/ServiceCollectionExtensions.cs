using ApplicationCore.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static readonly object _syncRoot = new object();

        private static ScopeLifeTime _defaultScore = ScopeLifeTime.Scoped;
        public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration) =>
           services.AddDbContext<EonixDbContext>(opts => opts.UseInMemoryDatabase("WebApi"));
          // opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("WebApi")));

        public static void ConfigureService(this IServiceCollection services)
        {
            var types = GetServices();
            foreach (var currService in types)
            {
                var serviceInterface = currService.GetInterfaces().Where(p => p.Namespace.Contains("ApplicationCore.Services")).FirstOrDefault();
                if (serviceInterface == null) continue;
                Bind(serviceInterface, currService, ScopeLifeTime.Scoped, services);
            }
        }
        public static void ConfigureRepository(this IServiceCollection services)
        {
            var types = GetRepositories();
            foreach (var currService in types)
            {
                var serviceInterface = currService.GetInterfaces()
                    .Where(p => p.Namespace.Contains("Repositories"))
                    .Where(p => !p.Name.Contains("IRepository"))
                    .FirstOrDefault();
                if (serviceInterface == null) continue;
                Bind(serviceInterface, currService, ScopeLifeTime.Scoped, services);
            }
        }

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerService, LoggerManager>();

        #region private methods
        private static List<Type> GetServices(string assemblyName = "Infrastructure")
        {
            var infraAssembly = Assembly.Load(assemblyName);
            return infraAssembly == null ? new List<Type>() : infraAssembly
                .GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.Contains("Services"))
                .Where(t => !t.IsAbstract && t.IsClass && t.GetInterfaces().Any()).ToList();
        }

        private static List<Type> GetRepositories(string assemblyName = "Infrastructure")
        {
            var infraAssembly = Assembly.Load(assemblyName);
            return infraAssembly == null ? new List<Type>() : infraAssembly
                .GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.Contains("Data"))
                .Where(t => !t.IsAbstract && t.IsClass && t.GetInterfaces().Any()).ToList();
        }

        private static void Bind(Type bindedInterface, Type bindedImplementation, ScopeLifeTime scope, IServiceCollection services)
        {
            lock (_syncRoot)
            {
                if (bindedImplementation == null) return;

                switch (scope)
                {
                    case ScopeLifeTime.Singleton:
                        services.AddSingleton(bindedInterface, bindedImplementation);
                        break;
                    case ScopeLifeTime.Transient:
                        services.AddTransient(bindedInterface, bindedImplementation);
                        break;
                    case ScopeLifeTime.Scoped:
                        services.AddScoped(bindedInterface, bindedImplementation);
                        break;
                    case ScopeLifeTime.NamedCallParent:
                    case ScopeLifeTime.Custom:
                        throw new NotImplementedException("Scope not yet supported.");
                    default:
                        Bind(bindedInterface, bindedImplementation, _defaultScore, services);
                        break;
                }
            }
        }

        #endregion   
    }
}