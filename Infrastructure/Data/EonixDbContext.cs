using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
// The line to execute for database first in the package Manager Console :
// Scaffold-DbContext "server=DELL-JASON\MSSQLSERVER2019;database=EonixWebApi;Integrated Security=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Project Infrastructure

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
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Address>(entity =>
        //    {
        //        entity.ToTable("address");

        //        entity.Property(e => e.Id).HasColumnName("id");

        //        entity.Property(e => e.Active)
        //            .IsRequired()
        //            .HasColumnName("active")
        //            .HasDefaultValueSql("((1))");

        //        entity.Property(e => e.CountryId).HasColumnName("address2country");

        //        entity.Property(e => e.BoxNumber)
        //            .IsRequired()
        //            .HasMaxLength(30)
        //            .IsUnicode(false)
        //            .HasColumnName("box_number");

        //        entity.Property(e => e.City)
        //            .HasMaxLength(40)
        //            .IsUnicode(false)
        //            .HasColumnName("city");

        //        entity.Property(e => e.Street)
        //            .IsRequired()
        //            .HasMaxLength(50)
        //            .IsUnicode(false)
        //            .HasColumnName("street");

        //        entity.Property(e => e.Zipcode)
        //            .IsRequired()
        //            .HasMaxLength(20)
        //            .IsUnicode(false)
        //            .HasColumnName("zipcode");

        //        entity.HasOne(d => d.Country)
        //            .WithMany(p => p.Addresses)
        //            .HasForeignKey(d => d.CountryId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("address_address2country_fkey");
        //    });

        //    modelBuilder.Entity<Company>(entity =>
        //    {
        //        entity.ToTable("company");

        //        entity.Property(e => e.Id).HasColumnName("id");

        //        entity.Property(e => e.Active)
        //            .IsRequired()
        //            .HasColumnName("active")
        //            .HasDefaultValueSql("((1))");

        //        entity.Property(e => e.AddressId).HasColumnName("company2address");

        //        entity.Property(e => e.Name)
        //            .IsRequired()
        //            .HasMaxLength(40)
        //            .IsUnicode(false)
        //            .HasColumnName("name");

        //        entity.HasOne(d => d.Address)
        //            .WithMany(p => p.Companies)
        //            .HasForeignKey(d => d.AddressId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("company_company2address_fkey");
        //    });

        //    modelBuilder.Entity<Contact>(entity =>
        //    {
        //        entity.ToTable("contact");

        //        entity.Property(e => e.Id).HasColumnName("id");

        //        entity.Property(e => e.Active)
        //            .IsRequired()
        //            .HasColumnName("active")
        //            .HasDefaultValueSql("((1))");

        //        entity.Property(e => e.Contact2address).HasColumnName("contact2address");

        //        entity.Property(e => e.CreationTime)
        //            .HasColumnType("datetime")
        //            .HasColumnName("creation_time");

        //        entity.Property(e => e.Firstname)
        //            .IsRequired()
        //            .HasMaxLength(40)
        //            .IsUnicode(false)
        //            .HasColumnName("firstname");

        //        entity.Property(e => e.Lastname)
        //            .IsRequired()
        //            .HasMaxLength(40)
        //            .IsUnicode(false)
        //            .HasColumnName("lastname");

        //        entity.HasOne(d => d.Address)
        //            .WithMany(p => p.Contacts)
        //            .HasForeignKey(d => d.Contact2address)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("contact_contact2address_fkey");
        //    });

        //    modelBuilder.Entity<ContactRole>(entity =>
        //    {
        //        entity.ToTable("contact_role");

        //        entity.HasIndex(e => new { e.CompanyId, e.ContactId, e.Name }, "contact_role_un")
        //            .IsUnique();

        //        entity.Property(e => e.Id).HasColumnName("id");

        //        entity.Property(e => e.Active)
        //            .IsRequired()
        //            .HasColumnName("active")
        //            .HasDefaultValueSql("((1))");

        //        entity.Property(e => e.CompanyId).HasColumnName("contact_role2company");

        //        entity.Property(e => e.ContactId).HasColumnName("contact_role2contact");

        //        entity.Property(e => e.Name)
        //            .IsRequired()
        //            .HasMaxLength(40)
        //            .IsUnicode(false)
        //            .HasColumnName("name");

        //        entity.HasOne(d => d.Company)
        //            .WithMany(p => p.ContactRoles)
        //            .HasForeignKey(d => d.CompanyId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("contact_role_contact_role2company_fkey");

        //        entity.HasOne(d => d.Contact)
        //            .WithMany(p => p.ContactRoles)
        //            .HasForeignKey(d => d.ContactId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("contact_role_contact_role2contact_fkey");
        //    });

        //    modelBuilder.Entity<Country>(entity =>
        //    {
        //        entity.ToTable("country");

        //        entity.Property(e => e.Id).HasColumnName("id");

        //        entity.Property(e => e.Active)
        //            .IsRequired()
        //            .HasColumnName("active")
        //            .HasDefaultValueSql("((1))");

        //        entity.Property(e => e.Iso3Code)
        //            .IsRequired()
        //            .HasMaxLength(3)
        //            .IsUnicode(false)
        //            .HasColumnName("iso_3_code");

        //        entity.Property(e => e.Name)
        //            .IsRequired()
        //            .HasMaxLength(50)
        //            .IsUnicode(false)
        //            .HasColumnName("name");
        //    });

        //}
    }
}
