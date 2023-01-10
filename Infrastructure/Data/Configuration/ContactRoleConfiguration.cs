using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infrastructure.Data.Configuration
{
    internal class ContactRoleConfiguration : IEntityTypeConfiguration<ContactRole>
    {
        public void Configure(EntityTypeBuilder<ContactRole> builder)
        {
            builder.ToTable("contact_role");

            builder.HasIndex(e => new { e.CompanyId, e.ContactId, e.Name }, "contact_role_un").IsUnique();
            builder.Property(e => e.Id).HasColumnName("id");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Active).IsRequired().HasColumnName("active").HasDefaultValueSql("((1))");
            builder.Property(e => e.CompanyId).HasColumnName("contact_role2company");
            builder.Property(e => e.ContactId).HasColumnName("contact_role2contact");
            builder.Property(e => e.Name).IsRequired().HasMaxLength(40).IsUnicode(false).HasColumnName("name");

            // Fk
            builder.HasOne(d => d.Company).WithMany().HasForeignKey(d => d.CompanyId).HasConstraintName("contact_role_contact_role2company_fkey");
            builder.HasOne(d => d.Contact).WithMany().HasForeignKey(d => d.ContactId).HasConstraintName("contact_role_contact_role2contact_fkey");

        }
    }
}
