using HR_Platform.Domain.RiskTypeMains;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class RiskTypeMainConfiguration : IEntityTypeConfiguration<RiskTypeMain>
{
    public void Configure(EntityTypeBuilder<RiskTypeMain> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            RiskTypeMainId => RiskTypeMainId.Value,
            value => new RiskTypeMainId(value)
        );

        builder.HasOne(p => p.Company).WithMany(c => c.RiskTypeMains).HasForeignKey(c => c.CompanyId);
        builder.HasOne(p => p.EmergencyPlanType).WithMany(c => c.RiskTypeMains).HasForeignKey(c => c.EmergencyPlanTypeId);

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

        builder.HasMany(x => x.Risks).WithOne(c => c.RiskTypeMain).HasForeignKey(y => y.RiskTypeMainId);
    }
}
