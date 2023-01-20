using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    internal class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("contact");
            builder.Property(e => e.Id).HasColumnName("id").HasConversion<int>();
            builder.Property(e => e.Active).IsRequired().HasColumnName("active").HasDefaultValueSql("((1))");
            builder.Property(e => e.AddressId).HasColumnName("contact2address");
            builder.Property(e => e.CreationTime).HasColumnType("datetime").HasColumnName("creation_time");
            builder.Property(e => e.Firstname).IsRequired().HasMaxLength(40).IsUnicode(false).HasColumnName("first_name");
            builder.Property(e => e.Lastname).IsRequired().HasMaxLength(40).IsUnicode(false).HasColumnName("last_name");
            builder.Property(e => e.PhoneNumber).HasMaxLength(30).IsUnicode(false).HasColumnName("phone_number");
            builder.HasOne(d => d.Address).WithMany().HasForeignKey(d => d.AddressId).HasConstraintName("contact_contact2address_fkey");
        }
    }
}