using HR_Platform.Domain.CollaboratorCriterias;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class CollaboratorCriteriaConfiguration : IEntityTypeConfiguration<CollaboratorCriteria>
{
    public void Configure(EntityTypeBuilder<CollaboratorCriteria> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            brigadeInventoryFileId => brigadeInventoryFileId.Value,
            value => new CollaboratorCriteriaId(value)
        );

        builder.HasOne(p => p.CollaboratorEvaluated).WithMany(c => c.CollaboratorCriterias).HasForeignKey(c => c.CollaboratorEvaluatedId);

        builder.HasOne(p => p.Evaluator).WithMany(c => c.Evaluators).HasForeignKey(c => c.EvaluatorId);

        builder.HasOne(p => p.Position).WithMany(c => c.CollaboratorCriterias).HasForeignKey(c => c.PositionId);

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

        builder.HasMany(c => c.CollaboratorCriteriaAnswers).WithOne(a => a.CollaboratorCriteria).HasForeignKey(a => a.CollaboratorCriteriaId);
    }
}
