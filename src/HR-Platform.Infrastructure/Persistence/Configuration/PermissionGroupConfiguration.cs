using HR_Platform.Domain.PermissionGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class PermissionGroupConfiguration : IEntityTypeConfiguration<PermissionGroup>
{
    public void Configure(EntityTypeBuilder<PermissionGroup> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id).HasConversion(
            permissionGroup => permissionGroup.Value,
            value => new PermissionGroupId(value)
        );

        builder.Property(g => g.Name).HasMaxLength(100);

        builder.Property(g => g.NameEnglish).HasMaxLength(100);

        builder.HasMany(g => g.Permissions).WithOne(p => p.PermissionGroup).HasForeignKey(g => g.PermissionGroupId);
    }
}
