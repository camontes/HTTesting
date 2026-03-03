using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultLanguageLevels;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultLanguageLevelConfiguration : IEntityTypeConfiguration<DefaultLanguageLevel>
{
    public void Configure(EntityTypeBuilder<DefaultLanguageLevel> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultLanguageLevelId => DefaultLanguageLevelId.Value,
            value => new DefaultLanguageLevelId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
