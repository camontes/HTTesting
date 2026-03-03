using HR_Platform.Domain.EvaluationCriteriaScores;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class EvaluationCriteriaScoreConfiguration : IEntityTypeConfiguration<EvaluationCriteriaScore>
{
    public void Configure(EntityTypeBuilder<EvaluationCriteriaScore> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasConversion(
            EvaluationCriteriaScoreId => EvaluationCriteriaScoreId.Value,
            value => new EvaluationCriteriaScoreId(value)
        );

        builder.HasOne(s => s.EvaluationCriteria).WithMany(c => c.EvaluationCriteriaScores).HasForeignKey(s => s.EvaluationCriteriaId);

        builder.Property(d => d.Description).HasMaxLength(200);

        builder.Property(d => d.DescriptionEnglish).HasMaxLength(200);

        builder.Property(s => s.IndexScoreAnswer);

        builder.Property(s => s.IsEditable);

        builder.Property(s => s.IsDeleteable);

        builder.Property(s => s.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(c => c.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

    }
}
