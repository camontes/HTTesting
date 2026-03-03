using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultRepeatEveryEvents;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultRepeatEveryEventConfiguration : IEntityTypeConfiguration<DefaultRepeatEveryEvent>
{
    public void Configure(EntityTypeBuilder<DefaultRepeatEveryEvent> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultRepeatEveryEventId => DefaultRepeatEveryEventId.Value,
            value => new DefaultRepeatEveryEventId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
