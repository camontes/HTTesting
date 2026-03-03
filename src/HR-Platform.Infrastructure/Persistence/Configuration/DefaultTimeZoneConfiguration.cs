using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultTimeZones;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultTimeZoneConfiguration : IEntityTypeConfiguration<DefaultTimeZone>
{
    public void Configure(EntityTypeBuilder<DefaultTimeZone> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultTimeZoneId => DefaultTimeZoneId.Value,
            value => new DefaultTimeZoneId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
