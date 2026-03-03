using HR_Platform.Domain.ImprovementPlanResponses;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class ImprovementPlanResponseConfiguration : IEntityTypeConfiguration<ImprovementPlanResponse>
{
    public void Configure(EntityTypeBuilder<ImprovementPlanResponse> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(ipr => ipr.Id);
        builder.Property(ipr => ipr.Id).HasConversion(
            ImprovementPlanTaskId => ImprovementPlanTaskId.Value,
            value => new ImprovementPlanResponseId(value)
        );

        builder.HasOne(ipr => ipr.ImprovementPlanTask).WithMany(c => c.ImprovementPlanResponse).HasForeignKey(c => c.ImprovementPlanTaskId);

        builder.Property(ipr => ipr.TaskResponse).HasMaxLength(3000);

        builder.Property(ipr => ipr.IsEditable);

        builder.Property(ipr => ipr.IsDeleteable);

        builder.Property(ipr => ipr.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(ipr => ipr.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.HasMany(ipr => ipr.ImprovementPlanResponseFiles).WithOne(iprf => iprf.ImprovementPlanResponse).HasForeignKey(iprf => iprf.ImprovementPlanResponseId);
    }
}
