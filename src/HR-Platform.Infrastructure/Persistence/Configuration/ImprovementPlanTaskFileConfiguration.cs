using HR_Platform.Domain.ImprovementPlanTaskFiles;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class ImprovementPlanTaskFileConfiguration : IEntityTypeConfiguration<ImprovementPlanTaskFile>
{
    public void Configure(EntityTypeBuilder<ImprovementPlanTaskFile> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultImprovementPlanFileId => DefaultImprovementPlanFileId.Value,
            value => new ImprovementPlanTaskFileId(value)
        );

        builder.HasOne(p => p.ImprovementPlanTask).WithMany(c => c.ImprovementPlanTaskFiles).HasForeignKey(c => c.ImprovementPlanTaskId);

        builder.Property(c => c.FileName).IsRequired();
        builder.Property(c => c.UrlFile).IsRequired();

        builder.Property(c => c.EmailWhoChangedByTH).HasMaxLength(50);
        builder.Property(c => c.NameWhoChangedByTH).HasMaxLength(50);
        builder.Property(c => c.UrlPhotoWhoChangedByTH);

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
