using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
// The line to execute for database first in the package Manager Console :
// Scaffold-DbContext "server=02-INF-W419\MSSQLSERVER2022;database=eonix;Integrated Security=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Project Infrastructure
// for specific tables :

//Scaffold-DbContext "server=02-INF-W419\MSSQLSERVER2022;database=eonix;Integrated Security=true;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Project Infrastructure -Tables contact
namespace Infrastructure.Data
{
    public partial class EonixDbContext : DbContext
    {
        public EonixDbContext()
        {
        }

        public EonixDbContext(DbContextOptions<EonixDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactRole> ContactRoles { get; set; }
        public virtual DbSet<Country> Countries { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("server=DELL-JASON\\MSSQLSERVER2019;database=EonixWebApi;Integrated Security=true");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyAllConfigurations(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        private void ApplyAllConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

    }
}
