using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultEvaluationCriterias;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultEvaluationCriteriaConfiguration : IEntityTypeConfiguration<DefaultEvaluationCriteria>
{
    public void Configure(EntityTypeBuilder<DefaultEvaluationCriteria> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasConversion(
           DefaultEvaluationCriteriaId => DefaultEvaluationCriteriaId.Value,
           value => new DefaultEvaluationCriteriaId(value)
       );

        builder.Property(d => d.Name).HasMaxLength(100);

        builder.Property(d => d.NameEnglish).HasMaxLength(100);

        builder.Property(d => d.Description).HasMaxLength(200);

        builder.Property(d => d.DescriptionEnglish).HasMaxLength(200);

        builder.Property(d => d.Percentage);

        builder.Property(d => d.IsEditable);

        builder.Property(d => d.IsDeleteable);
    }
}
