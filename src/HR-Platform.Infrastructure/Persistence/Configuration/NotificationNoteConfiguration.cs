using HR_Platform.Domain.NotificationNotes;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class NotificationNoteConfiguration : IEntityTypeConfiguration<NotificationNote>
{
    public void Configure(EntityTypeBuilder<NotificationNote> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id).HasConversion(
            NotificationNoteId => NotificationNoteId.Value,
            value => new NotificationNoteId(value)
        );

        builder.Property(n => n.IsRead);

        builder.Property(n => n.IsEditable);

        builder.Property(n => n.IsDeleteable);

        builder.HasOne(n => n.Collaborator).WithMany(c => c.NotificationNotes).HasForeignKey(n => n.CollaboratorId);

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
