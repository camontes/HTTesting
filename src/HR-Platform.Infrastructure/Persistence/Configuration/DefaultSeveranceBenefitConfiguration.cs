using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultSeveranceBenefits;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultSeveranceBenefitConfiguration : IEntityTypeConfiguration<DefaultSeveranceBenefit>
{
    public void Configure(EntityTypeBuilder<DefaultSeveranceBenefit> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultSeveranceBenefitId => DefaultSeveranceBenefitId.Value,
            value => new DefaultSeveranceBenefitId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
