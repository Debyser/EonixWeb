using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("address");

            builder.Property(e => e.Id).HasColumnName("id").HasConversion<int>();

            builder.HasKey(p => p.Id);
            builder.Property(e => e.Active).IsRequired().HasColumnName("active").HasDefaultValueSql("((1))");
            builder.Property(e => e.CountryId).HasColumnName("address2country").IsRequired();
            builder.Property(e => e.BoxNumber).IsRequired().HasMaxLength(30).IsUnicode(false).HasColumnName("box_number");
            builder.Property(e => e.City).HasMaxLength(40).IsUnicode(false).HasColumnName("city");
            builder.Property(e => e.Street).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnName("street");
            builder.Property(e => e.Zipcode).IsRequired().HasMaxLength(20).IsUnicode(false).HasColumnName("zipcode");

            // Fk
            builder.HasOne(d => d.Country).WithMany().HasForeignKey(d => d.CountryId).HasConstraintName("address_address2country_fkey");
        }
    }
}
