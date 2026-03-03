using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultEventTypes;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultEventTypeConfiguration : IEntityTypeConfiguration<DefaultEventType>
{
    public void Configure(EntityTypeBuilder<DefaultEventType> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultEventTypeId => DefaultEventTypeId.Value,
            value => new DefaultEventTypeId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
