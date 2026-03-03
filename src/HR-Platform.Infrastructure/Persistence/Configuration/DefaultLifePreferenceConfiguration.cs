using HR_Platform.Domain.DefaultLifePreferences;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultLifePreferenceConfiguration : IEntityTypeConfiguration<DefaultLifePreference>
{
    public void Configure(EntityTypeBuilder<DefaultLifePreference> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultLifePreferenceId => DefaultLifePreferenceId.Value,
            value => new DefaultLifePreferenceId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
