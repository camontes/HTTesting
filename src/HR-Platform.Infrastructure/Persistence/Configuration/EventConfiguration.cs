using HR_Platform.Domain.Events;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultEventId => DefaultEventId.Value,
            value => new EventId(value)
        );

        builder.HasOne(p => p.Company).WithMany(c => c.Events).HasForeignKey(c => c.CompanyId);
        builder.HasOne(p => p.TimeZone).WithMany(c => c.Events).HasForeignKey(c => c.TimeZoneId);
        builder.HasOne(p => p.EventType).WithMany(c => c.Events).HasForeignKey(c => c.EventTypeId);

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.StartDate).HasConversion(
           startDate => startDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(r => r.StartTime);

        builder.Property(c => c.EndDate).HasConversion(
           endDate => endDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(r => r.EndTime);

        builder.Property(c => c.Description)
            .HasMaxLength(50);

        builder.Property(c => c.EmailCreatedBy)
            .HasMaxLength(50);

        builder.Property(r => r.IsDeletedEvent);

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

        builder.HasMany(c => c.CollaboratorEvents).WithOne(ct => ct.Event).HasForeignKey(ct => ct.EventId);
        builder.HasMany(c => c.EventRecurrences).WithOne(ct => ct.Event).HasForeignKey(ct => ct.EventId);

    }
}
