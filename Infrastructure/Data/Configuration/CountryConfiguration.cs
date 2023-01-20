using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {

            builder.ToTable("country");

            /* Warning
               By convention, a property named Id or <type name>Id will be configured as the primary key of an entity.
            */
            builder.Property(e => e.Id).HasColumnName("id").HasColumnType("SMALLINT");
            builder.HasKey(e => e.Id); // Define the PK 

            builder.Property(e => e.Iso2Code).IsRequired().HasMaxLength(2).IsUnicode(false).HasColumnName("iso_2_code");

            builder.Property(e => e.Iso3Code).IsRequired().HasMaxLength(3).IsUnicode(false).HasColumnName("iso_3_code");

            builder.Property(e => e.Name).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnName("name");
        }
    }
}
