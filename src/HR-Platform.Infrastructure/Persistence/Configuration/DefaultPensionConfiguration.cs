using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultPensions;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultPensionConfiguration : IEntityTypeConfiguration<DefaultPension>
{
    public void Configure(EntityTypeBuilder<DefaultPension> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultPensionId => DefaultPensionId.Value,
            value => new DefaultPensionId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
