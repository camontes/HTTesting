using HR_Platform.Domain.InductionFiles;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class InductionFileConfiguration : IEntityTypeConfiguration<InductionFile>
{
    public void Configure(EntityTypeBuilder<InductionFile> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultInductionFileId => DefaultInductionFileId.Value,
            value => new InductionFileId(value)
        );

        builder.HasOne(p => p.Induction).WithMany(c => c.InductionFiles).HasForeignKey(c => c.InductionId);

        builder.Property(c => c.FileName).HasMaxLength(50).IsRequired();
        builder.Property(c => c.UrlFile).IsRequired();

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
