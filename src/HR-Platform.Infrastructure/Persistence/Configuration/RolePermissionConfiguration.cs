using HR_Platform.Domain.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasConversion(
            permission => permission.Value,
            value => new PermissionId(value)
        );

        builder.HasOne(p => p.PermissionGroup).WithMany(g => g.Permissions).HasForeignKey(p => p.PermissionGroupId);

        builder.Property(p => p.Name).HasMaxLength(100);

        builder.Property(p => p.NameEnglish).HasMaxLength(100);

        builder.Property(p => p.Description).HasMaxLength(150);

        builder.Property(p => p.DescriptionEnglish).HasMaxLength(150);

        builder.Property(p => p.ValidationString).HasMaxLength(50);
    }
}
