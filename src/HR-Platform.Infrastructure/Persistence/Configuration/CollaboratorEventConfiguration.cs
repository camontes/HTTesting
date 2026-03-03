using HR_Platform.Domain.CollaboratorEvents;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class CollaboratorEventConfiguration : IEntityTypeConfiguration<CollaboratorEvent>
{
    public void Configure(EntityTypeBuilder<CollaboratorEvent> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            brigadeInventoryFileId => brigadeInventoryFileId.Value,
            value => new CollaboratorEventId(value)
        );

        builder.HasOne(p => p.Event).WithMany(c => c.CollaboratorEvents).HasForeignKey(c => c.EventId);
        builder.HasOne(p => p.Collaborator).WithMany(c => c.CollaboratorEvents).HasForeignKey(c => c.CollaboratorId);


        builder.Property(r => r.NotificationSent);

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
