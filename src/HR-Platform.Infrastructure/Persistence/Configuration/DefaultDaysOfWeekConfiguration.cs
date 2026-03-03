using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultDaysOfWeeks;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultDaysOfWeekConfiguration : IEntityTypeConfiguration<DefaultDaysOfWeek>
{
    public void Configure(EntityTypeBuilder<DefaultDaysOfWeek> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultDaysOfWeekId => DefaultDaysOfWeekId.Value,
            value => new DefaultDaysOfWeekId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
