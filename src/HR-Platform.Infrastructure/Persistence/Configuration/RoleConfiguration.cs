using HR_Platform.Domain.Roles;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasConversion(
            roleId => roleId.Value,
            value => new RoleId(value)
        );

        builder.HasOne(r => r.Company).WithMany(c => c.Roles).HasForeignKey(r => r.CompanyId);
        builder.HasOne(r => r.Area).WithMany(c => c.Roles).HasForeignKey(r => r.AreaId);

        builder.Property(r => r.Name).HasMaxLength(100);

        builder.Property(r => r.NameEnglish).HasMaxLength(100);

        builder.Property(r => r.IsEditable);

        builder.Property(r => r.IsDeleteable);

        builder.Property(r => r.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(c => c.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.HasMany(r => r.Collaborators).WithOne(c => c.Role).HasForeignKey(r => r.RoleId);
    }
}
