using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultEventReplays;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultEventReplayConfiguration : IEntityTypeConfiguration<DefaultEventReplay>
{
    public void Configure(EntityTypeBuilder<DefaultEventReplay> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultEventReplayId => DefaultEventReplayId.Value,
            value => new DefaultEventReplayId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);

        builder.HasMany(c => c.EventRecurrences).WithOne(ct => ct.EventReplayType).HasForeignKey(ct => ct.EventReplayTypeId);

    }
}
