using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultMonths;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultMonthConfiguration : IEntityTypeConfiguration<DefaultMonth>
{
    public void Configure(EntityTypeBuilder<DefaultMonth> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultMonthId => DefaultMonthId.Value,
            value => new DefaultMonthId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
