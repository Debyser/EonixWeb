using Autofac;
using EonixWebApi.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EonixWebApi.Web.Composition
{
    public class DataModule : CompositionModule
    {
        public DataModule(IConfiguration configuration) : base(configuration) { }
        protected override void Register(ContainerBuilder builder)
        {
            RegisterRepositories(builder);
            RegisterDbContexts(builder);
        }

        private void RegisterDbContexts(ContainerBuilder builder)
        {
            RegisterDbContext<EonixWebApiDbContext>(builder, Configuration.GetConnectionString(nameof(EonixWebApiDbContext)));
        }
        private void RegisterRepositories(ContainerBuilder builder)
        {
            RegisterByEndName<ContactRepository>(builder, "Repository");
        }

        private void RegisterDbContext<TDbContext>(ContainerBuilder builder, string connectionString)
           where TDbContext : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            builder.RegisterType<TDbContext>()
                .WithParameter("options", optionsBuilder.Options)
                .As<TDbContext>()
                .InstancePerLifetimeScope()
                .OnRelease(context =>
                {
                    context.SaveChanges();
                    context.Dispose();
                });
        }

    }

}
