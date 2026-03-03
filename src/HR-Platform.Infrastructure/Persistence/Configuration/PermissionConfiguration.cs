using HR_Platform.Domain.RolesPermissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasConversion(
            permission => permission.Value,
            value => new RolePermissionId(value)
        );

        builder.HasOne(r => r.Role).WithMany(r => r.RolesPermissions).HasForeignKey(r => r.RoleId);

        builder.HasOne(r => r.Permission).WithMany(p => p.RolesPermissions).HasForeignKey(r => r.PermissionId);

        builder.Property(r => r.IsEditable);

        builder.Property(r => r.IsCheck);
    }
}
