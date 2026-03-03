using HR_Platform.Domain.EmergencyPlans;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class EmergencyPlanConfiguration : IEntityTypeConfiguration<EmergencyPlan>
{
    public void Configure(EntityTypeBuilder<EmergencyPlan> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultEmergencyPlanId => DefaultEmergencyPlanId.Value,
            value => new EmergencyPlanId(value)
        );

        builder.HasOne(p => p.EmergencyPlanType).WithMany(c => c.EmergencyPlans).HasForeignKey(c => c.EmergencyPlanTypeId);

        builder.Property(c => c.Name).HasMaxLength(100);
        builder.Property(c => c.Description).HasMaxLength(1000);

        builder.Property(c => c.ImageName);
        builder.Property(c => c.ImageURL);

        builder.Property(r => r.ImageCreationTime).HasConversion(
          imageDate => imageDate.Value,
          value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(c => c.VideoName);
        builder.Property(c => c.VideoURL);

        builder.Property(c => c.IsVisible);

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
