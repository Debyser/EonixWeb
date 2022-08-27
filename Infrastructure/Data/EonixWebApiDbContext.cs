using EonixWebApi.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EonixWebApi.Infrastructure.Data
{
    public class EonixWebApiDbContext : DbContext
    {

        private readonly string _contextName;

        public EonixWebApiDbContext(DbContextOptions options) : base(options)
        {
            _contextName = options.ContextType.Name;
        }

        public virtual DbSet<Person> Persons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyAllConfigurations(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        private void ApplyAllConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
