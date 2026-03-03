using HR_Platform.Domain.EmergencyPlanTypes;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class EmergencyPlanTypeConfiguration : IEntityTypeConfiguration<EmergencyPlanType>
{
    public void Configure(EntityTypeBuilder<EmergencyPlanType> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            EmergencyPlanTypeId => EmergencyPlanTypeId.Value,
            value => new EmergencyPlanTypeId(value)
        );

        builder.HasOne(p => p.Company).WithMany(c => c.EmergencyPlanTypes).HasForeignKey(c => c.CompanyId);

        builder.Property(c => c.EmergencyPlanMainName).HasMaxLength(50);

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);

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

        builder.HasMany(e => e.EmergencyPlans).WithOne(c => c.EmergencyPlanType).HasForeignKey(c => c.EmergencyPlanTypeId);
        builder.HasMany(e => e.RiskTypeMains).WithOne(c => c.EmergencyPlanType).HasForeignKey(c => c.EmergencyPlanTypeId);
    }
}
