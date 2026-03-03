using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.EconomicLevels;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class EconomicLevelConfiguration : IEntityTypeConfiguration<EconomicLevel>
{
    public void Configure(EntityTypeBuilder<EconomicLevel> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            economicLevelId => economicLevelId.Value,
            value => new EconomicLevelId(value)
        );

        builder.Property(c => c.Name).HasMaxLength(20);

        builder.Property(c => c.NameEnglish).HasMaxLength(20);
        builder.HasMany(m => m.Collaborators).WithOne(c => c.EconomicLevel).HasForeignKey(c => c.EconomicLevelId);

    }
}
