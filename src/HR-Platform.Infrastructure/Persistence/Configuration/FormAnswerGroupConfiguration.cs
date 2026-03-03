using HR_Platform.Domain.FormAnswerGroups;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class FormAnswerGroupConfiguration : IEntityTypeConfiguration<FormAnswerGroup>
{
    public void Configure(EntityTypeBuilder<FormAnswerGroup> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(fag => fag.Id);
        builder.Property(fag => fag.Id).HasConversion(
            FormAnswerGroupId => FormAnswerGroupId.Value,
            value => new FormAnswerGroupId(value)
        );

        builder.HasOne(fag => fag.Form).WithMany(f => f.FormAnswerGroups).HasForeignKey(fag => fag.FormId);
        builder.HasOne(fag => fag.Collaborator).WithMany(c => c.FormAnswerGroups).HasForeignKey(fag => fag.CollaboratorId);
        builder.HasOne(fag => fag.FormAnswerGroupState).WithMany(fags => fags.FormAnswerGroups).HasForeignKey(fags => fags.FormAnswerGroupStateId);

        builder.Property(fag => fag.ReferenceNumber).HasMaxLength(18).IsRequired();

        builder.Property(fag => fag.IsEditable);

        builder.Property(fag => fag.IsDeleteable);

        builder.Property(fag => fag.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(fag => fag.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.HasMany(fag => fag.FormAnswers).WithOne(fa => fa.FormAnswerGroup).HasForeignKey(fa => fa.FormAnswerGroupId);
        builder.HasMany(fag => fag.FormAnswerGroupFiles).WithOne(fagf => fagf.FormAnswerGroup).HasForeignKey(fagf => fagf.FormAnswerGroupId);
    }
}
