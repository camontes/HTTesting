using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultFamilyCompositions;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultFamilyCompositionConfiguration : IEntityTypeConfiguration<DefaultFamilyComposition>
{
    public void Configure(EntityTypeBuilder<DefaultFamilyComposition> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            defaultFamilyCompositionId => defaultFamilyCompositionId.Value,
            value => new DefaultFamilyCompositionId(value)
        );

        builder.Property(c => c.Name).HasMaxLength(100);

        builder.Property(c => c.NameEnglish).HasMaxLength(100);
    }
}
