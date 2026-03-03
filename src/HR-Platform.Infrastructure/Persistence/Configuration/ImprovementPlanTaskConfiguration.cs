using HR_Platform.Domain.ImprovementPlanResponses;
using HR_Platform.Domain.ImprovementPlanTasks;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moq;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class ImprovementPlanTaskConfiguration : IEntityTypeConfiguration<ImprovementPlanTask>
{
    public void Configure(EntityTypeBuilder<ImprovementPlanTask> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(ipt => ipt.Id);
        builder.Property(ipt => ipt.Id).HasConversion(
            ImprovementPlanTaskId => ImprovementPlanTaskId.Value,
            value => new ImprovementPlanTaskId(value)
        );

        builder.HasOne(ipt => ipt.ImprovementPlan).WithMany(ip => ip.ImprovementPlanTasks).HasForeignKey(ipt => ipt.ImprovementPlanId);

        builder.Property(ipt => ipt.TaskDescription).HasMaxLength(3000);

        builder.Property(ipt => ipt.IsEditable);

        builder.Property(ipt => ipt.IsDeleteable);

        builder.Property(ipt => ipt.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(ipt => ipt.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.HasMany(ipt => ipt.ImprovementPlanResponse).WithOne(iptf => iptf.ImprovementPlanTask).HasForeignKey(iptf => iptf.ImprovementPlanTaskId);
        builder.HasMany(ipt => ipt.ImprovementPlanTaskFiles).WithOne(iptf => iptf.ImprovementPlanTask).HasForeignKey(iptf => iptf.ImprovementPlanTaskId);
    }
}
