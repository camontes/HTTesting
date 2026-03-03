using HR_Platform.Domain.FormAnswers;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class FormAnswerConfiguration : IEntityTypeConfiguration<FormAnswer>
{
    public void Configure(EntityTypeBuilder<FormAnswer> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            FormAnswerId => FormAnswerId.Value,
            value => new FormAnswerId(value)
        );

        builder.HasOne(fa => fa.FormQuestionsType).WithMany(fqt => fqt.FormAnswers).HasForeignKey(fa => fa.FormQuestionsTypeId);
        builder.HasOne(fa => fa.FormAnswerGroup).WithMany(fag => fag.FormAnswers).HasForeignKey(fa => fa.FormAnswerGroupId);
        builder.HasOne(fa => fa.Collaborator).WithMany(c => c.FormAnswers).HasForeignKey(fa => fa.CollaboratorId);

        builder.Property(c => c.Answer)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(c => c.ReferenceNumber).HasMaxLength(18).IsRequired();

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
