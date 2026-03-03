using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultEvaluationCriteriaScores;
using HR_Platform.Domain.ValueObjects;
using HR_Platform.Domain.DefaultEvaluationCriterias;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultEvaluationCriteriaScoreConfiguration : IEntityTypeConfiguration<DefaultEvaluationCriteriaScore>
{
    public void Configure(EntityTypeBuilder<DefaultEvaluationCriteriaScore> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasConversion(
           DefaultEvaluationCriteriaScoreId => DefaultEvaluationCriteriaScoreId.Value,
           value => new DefaultEvaluationCriteriaScoreId(value)
       );

        builder.Property(d => d.Description).HasMaxLength(200);

        builder.Property(d => d.DescriptionEnglish).HasMaxLength(200);

        builder.Property(d => d.LowerScore);

        builder.Property(d => d.UpperScore);

        builder.Property(d => d.IsEditable);

        builder.Property(d => d.IsDeleteable);
    }
}
