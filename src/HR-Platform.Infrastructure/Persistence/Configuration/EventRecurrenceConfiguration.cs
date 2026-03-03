using HR_Platform.Domain.EventRecurrences;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class EventRecurrenceConfiguration : IEntityTypeConfiguration<EventRecurrence>
{
    public void Configure(EntityTypeBuilder<EventRecurrence> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            brigadeFileId => brigadeFileId.Value,
            value => new EventRecurrenceId(value)
        );

        builder.HasOne(p => p.Event).WithMany(c => c.EventRecurrences).HasForeignKey(c => c.EventId);
        builder.HasOne(p => p.EventReplayType).WithMany(c => c.EventRecurrences).HasForeignKey(c => c.EventReplayTypeId);


        builder.Property(r => r.Interval);

        builder.Property(r => r.RecurrenceEndDate).HasConversion(
         recurrenceEndDate => recurrenceEndDate.Value,
         value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
      );

        builder.Property(r => r.IsEditable);
        builder.Property(r => r.IsDeleteable);

        builder.Property(r => r.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(c => c.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );
    }
}
