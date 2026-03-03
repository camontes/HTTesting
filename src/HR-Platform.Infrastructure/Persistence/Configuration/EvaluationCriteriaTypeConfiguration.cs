using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.EvaluationCriteriaTypes;
using HR_Platform.Domain.ValueObjects;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class EvaluationCriteriaTypeConfiguration : IEntityTypeConfiguration<EvaluationCriteriaType>
{
    public void Configure(EntityTypeBuilder<EvaluationCriteriaType> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasConversion(
           EvaluationCriteriaTypeId => EvaluationCriteriaTypeId.Value,
           value => new EvaluationCriteriaTypeId(value)
       );

        builder.Property(e => e.Name).HasMaxLength(100);

        builder.Property(e => e.NameEnglish).HasMaxLength(100);

        builder.Property(d => d.IsEditable);

        builder.Property(d => d.IsDeleteable);

        builder.HasMany(t => t.EvaluationCriterias).WithOne(c => c.EvaluationCriteriaType).HasForeignKey(c => c.EvaluationCriteriaTypeId);
        builder.HasMany(t => t.CollaboratorCriteriaAnswers).WithOne(c => c.EvaluationCriteriaType).HasForeignKey(c => c.EvaluationCriteriaTypeId);
    }
}
