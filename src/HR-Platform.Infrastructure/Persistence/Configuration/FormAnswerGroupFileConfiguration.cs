using HR_Platform.Domain.FormAnswerGroupFiles;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class FormAnswerGroupFileConfiguration : IEntityTypeConfiguration<FormAnswerGroupFile>
{
    public void Configure(EntityTypeBuilder<FormAnswerGroupFile> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(fag => fag.Id);
        builder.Property(fag => fag.Id).HasConversion(
            FormAnswerGroupFileId => FormAnswerGroupFileId.Value,
            value => new FormAnswerGroupFileId(value)
        );

        builder.HasOne(fagf => fagf.FormAnswerGroup).WithMany(fag => fag.FormAnswerGroupFiles).HasForeignKey(fagf => fagf.FormAnswerGroupId);

        builder.Property(fagf => fagf.File).HasMaxLength(150).IsRequired();
        builder.Property(fagf => fagf.FileName).HasMaxLength(150).IsRequired();
        builder.Property(fagf => fagf.IsEditable);

        builder.Property(fagf => fagf.IsDeleteable);

        builder.Property(fagf => fagf.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(fagf => fagf.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );
    }
}
