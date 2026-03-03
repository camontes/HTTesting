using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultPositions;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultPositionConfiguration : IEntityTypeConfiguration<DefaultPosition>
{
    public void Configure(EntityTypeBuilder<DefaultPosition> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            defaultRoleId => defaultRoleId.Value,
            value => new(value)
        );

        builder.Property(c => c.Name).HasMaxLength(50);

        builder.Property(c => c.NameEnglish).HasMaxLength(50);


        builder.Property(c => c.Description).HasMaxLength(200);

        builder.Property(c => c.DescriptionEnglish).HasMaxLength(200);
    }
}
