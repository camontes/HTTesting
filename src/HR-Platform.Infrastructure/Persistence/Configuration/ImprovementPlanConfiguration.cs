using HR_Platform.Domain.ImprovementPlans;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class ImprovementPlanConfiguration : IEntityTypeConfiguration<ImprovementPlan>
{
    public void Configure(EntityTypeBuilder<ImprovementPlan> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            ImprovementPlanTaskId => ImprovementPlanTaskId.Value,
            value => new ImprovementPlanId(value)
        );

        builder.HasOne(p => p.CollaboratorCriteriaAnswer).WithMany(c => c.ImprovementPlans).HasForeignKey(c => c.CollaboratorCriteriaAnswerId);

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

        builder.HasMany(x => x.ImprovementPlanTasks).WithOne(s => s.ImprovementPlan).HasForeignKey(t => t.ImprovementPlanId);
    }
}
