using HR_Platform.Domain.EvaluationCriterias;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class EvaluationCriteriaConfiguration : IEntityTypeConfiguration<EvaluationCriteria>
{
    public void Configure(EntityTypeBuilder<EvaluationCriteria> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultEvaluationCriteriaId => DefaultEvaluationCriteriaId.Value,
            value => new EvaluationCriteriaId(value)
        );

        builder.HasOne(c => c.Position).WithMany(p => p.EvaluationCriterias).HasForeignKey(c => c.PositionId);
        builder.HasOne(c => c.EvaluationCriteriaType).WithMany(t => t.EvaluationCriterias).HasForeignKey(c => c.EvaluationCriteriaTypeId);

        builder.Property(d => d.Name).HasMaxLength(100);

        builder.Property(d => d.NameEnglish).HasMaxLength(100);

        builder.Property(d => d.Description).HasMaxLength(200);

        builder.Property(d => d.DescriptionEnglish).HasMaxLength(200);

        builder.Property(d => d.Percentage);

        builder.Property(d => d.IsEditable);

        builder.Property(d => d.IsDeleteable);

        builder.Property(c => c.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(c => c.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.HasMany(c => c.EvaluationCriteriaScores).WithOne(s => s.EvaluationCriteria).HasForeignKey(s => s.EvaluationCriteriaId);
    }
}
