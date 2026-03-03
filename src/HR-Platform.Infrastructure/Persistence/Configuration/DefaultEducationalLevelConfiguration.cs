using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultEducationalLevels;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultEducationalLevelConfiguration : IEntityTypeConfiguration<DefaultEducationalLevel>
{
    public void Configure(EntityTypeBuilder<DefaultEducationalLevel> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultEducationalLevelId => DefaultEducationalLevelId.Value,
            value => new DefaultEducationalLevelId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
