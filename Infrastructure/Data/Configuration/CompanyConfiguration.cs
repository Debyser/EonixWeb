using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {

            builder.ToTable("company");

            builder.Property(e => e.Id).HasColumnName("id").HasConversion<int>();
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Active).IsRequired().HasColumnName("active").HasDefaultValueSql("((1))");
            builder.Property(e => e.AddressId).HasColumnName("company2address");
            builder.Property(e => e.Name).IsRequired().HasMaxLength(40).IsUnicode(false).HasColumnName("name");

            // Fk
            builder.HasOne(d => d.Address).WithMany().HasForeignKey(d => d.AddressId).HasConstraintName("company_company2address_fkey");
        }
    }
}