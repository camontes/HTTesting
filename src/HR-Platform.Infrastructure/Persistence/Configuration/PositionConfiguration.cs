using HR_Platform.Domain.Positions;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasConversion(
            positionId => positionId.Value,
            value => new(value)
        );

        builder.HasOne(p => p.Company).WithMany(c => c.Positions).HasForeignKey(p => p.CompanyId);


        builder.Property(p => p.Name).HasMaxLength(50);

        builder.Property(p => p.NameEnglish).HasMaxLength(50);


        builder.Property(c => c.Description).HasMaxLength(200);

        builder.Property(c => c.DescriptionEnglish).HasMaxLength(200);


        builder.Property(p => p.PositionFile);

        builder.Property(p => p.PositionFileName);


        builder.Property(p => p.IsEditable);

        builder.Property(p => p.IsDeleteable);


        builder.Property(p => p.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(c => c.EditionDate).HasConversion(
           criteriaEditionDate => criteriaEditionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(c => c.CriteriasEditionDate).HasConversion(
           criteriasEditionDate => criteriasEditionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.HasMany(p => p.Collaborators).WithOne(c => c.Position).HasForeignKey(c => c.PositionId);
        builder.HasMany(p => p.EvaluationCriterias).WithOne(c => c.Position).HasForeignKey(c => c.PositionId);
    }
}
