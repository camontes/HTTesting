using HR_Platform.Domain.NotificationTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class NotificationTypeConfiguration : IEntityTypeConfiguration<NotificationType>
{
    public void Configure(EntityTypeBuilder<NotificationType> builder)
    {
        builder.HasKey(nt => nt.Id);
        builder.Property(nt => nt.Id).HasConversion(
            NotificationTypeId => NotificationTypeId.Value,
            value => new NotificationTypeId(value)
        );

        builder.Property(nt => nt.Message)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(nt => nt.MessageEnglish)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasMany(nt => nt.Notifications)
               .WithOne(n => n.NotificationType)
               .HasForeignKey(n => n.NotificationTypeId);
    }
}
