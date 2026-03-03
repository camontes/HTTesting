using HR_Platform.Domain.FormQuestionsTypes;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class FormQuestionsTypeConfiguration : IEntityTypeConfiguration<FormQuestionsType>
{
    public void Configure(EntityTypeBuilder<FormQuestionsType> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(x => x.Id);

        builder.Property(c => c.Id).HasConversion(
            DefaultContractId => DefaultContractId.Value,
            value => new FormQuestionsTypeId(value)
        );

        builder.HasOne(p => p.Form).WithMany(c => c.FormQuestionsTypes).HasForeignKey(c => c.FormId);

        builder.HasOne(c => c.QuestionType).WithMany(c => c.FormQuestionsTypes).HasForeignKey(f => f.QuestionTypeId);

        builder.Property(r => r.Question).HasMaxLength(200);

        builder.Property(r => r.IsRequired);

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

        builder.HasMany(fqt => fqt.FormAnswers).WithOne(fa => fa.FormQuestionsType).HasForeignKey(fa => fa.FormQuestionsTypeId);
    }
}
