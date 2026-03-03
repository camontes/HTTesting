using HR_Platform.Domain.Notifications;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id).HasConversion(
            NotificationId => NotificationId.Value,
            value => new NotificationId(value)
        );

        builder.Property(n => n.Message)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(n => n.MessageEnglish)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(n => n.SourceEmail)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(n => n.SourceName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(n => n.SourceInitials)
            .HasMaxLength(5)
            .IsRequired();

        builder.Property(n => n.SourcePhoto)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(n => n.IsRead);

        builder.Property(n => n.IsEditable);

        builder.Property(n => n.IsDeleteable);

        builder.HasOne(n => n.NotificationType).WithMany(nt => nt.Notifications).HasForeignKey(n => n.NotificationTypeId);

        builder.HasOne(n => n.Collaborator).WithMany(c => c.Notifications).HasForeignKey(n => n.CollaboratorId);

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
