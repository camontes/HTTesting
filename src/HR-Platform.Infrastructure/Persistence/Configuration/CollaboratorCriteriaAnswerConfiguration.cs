using HR_Platform.Domain.CollaboratorCriteriaAnswers;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class CollaboratorCriteriaAnswerConfiguration : IEntityTypeConfiguration<CollaboratorCriteriaAnswer>
{
    public void Configure(EntityTypeBuilder<CollaboratorCriteriaAnswer> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            brigadeInventoryFileId => brigadeInventoryFileId.Value,
            value => new CollaboratorCriteriaAnswerId(value)
        );

        builder.HasOne(cca => cca.EvaluationCriteriaType).WithMany(c => c.CollaboratorCriteriaAnswers).HasForeignKey(c => c.EvaluationCriteriaTypeId);

        builder.HasOne(cca => cca.CollaboratorCriteria).WithMany(c => c.CollaboratorCriteriaAnswers).HasForeignKey(c => c.CollaboratorCriteriaId);

        builder.Property(cca => cca.PriorityNoveltyId);

        builder.Property(cca => cca.GeneralSubjetiveCriteriaPercentage);

        builder.Property(cca => cca.GeneralSubjetiveCriteriaPercentage);

        builder.Property(cca => cca.CriteriaName).HasMaxLength(220); 
        builder.Property(cca => cca.CriteriaNameEnglish).HasMaxLength(220);

        builder.Property(cca => cca.CriteriaScoreName).HasMaxLength(220);
        builder.Property(cca => cca.CriteriaScoreNameEnglish).HasMaxLength(220);

        builder.Property(cca => cca.ReferenceNumber).HasMaxLength(14);

        builder.Property(cca => cca.CriteriaPercentage);

        builder.Property(cca => cca.CriteriaScorePercentage);

        builder.Property(cca => cca.CriteriaScoreIndexAnswerr);

        builder.Property(cca => cca.Position);

        builder.Property(cca => cca.PositionEnglish);

        builder.Property(cca => cca.Comments).HasMaxLength(2000);

        builder.Property(cca => cca.IsInHistorical);

        builder.Property(cca => cca.IsEditable);
        builder.Property(cca => cca.IsDeleteable);

        builder.Property(cca => cca.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(c => c.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

    }
}
