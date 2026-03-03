using HR_Platform.Domain.ImprovementPlanResponseFiles;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class ImprovementPlanResponseFileConfiguration : IEntityTypeConfiguration<ImprovementPlanResponseFile>
{
    public void Configure(EntityTypeBuilder<ImprovementPlanResponseFile> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(iprf => iprf.Id);
        builder.Property(iprf => iprf.Id).HasConversion(
            ImprovementPlanResponseFileId => ImprovementPlanResponseFileId.Value,
            value => new ImprovementPlanResponseFileId(value)
        );

        builder.HasOne(iprf => iprf.ImprovementPlanResponse).WithMany(ipr => ipr.ImprovementPlanResponseFiles).HasForeignKey(iprf => iprf.ImprovementPlanResponseId);

        builder.Property(iprf => iprf.FileName).IsRequired();
        builder.Property(iprf => iprf.UrlFile).IsRequired();

        builder.Property(iprf => iprf.EmailWhoChanged).HasMaxLength(50);
        builder.Property(iprf => iprf.NameWhoChanged).HasMaxLength(50);
        builder.Property(iprf => iprf.UrlPhotoWhoChanged);

        builder.Property(iprf => iprf.IsEditable);

        builder.Property(iprf => iprf.IsDeleteable);

        builder.Property(iprf => iprf.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(iprf => iprf.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );
    }
}
