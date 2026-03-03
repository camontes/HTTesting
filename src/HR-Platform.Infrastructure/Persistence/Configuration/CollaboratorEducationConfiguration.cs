using HR_Platform.Domain.CollaboratorEducations;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class CollaboratorEducationConfiguration : IEntityTypeConfiguration<CollaboratorEducation>
{
    public void Configure(EntityTypeBuilder<CollaboratorEducation> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasConversion(
            DefaultCollaboratorEducationId => DefaultCollaboratorEducationId.Value,
            value => new CollaboratorEducationId(value)
        );

        builder.HasOne(cl => cl.Collaborator)
        .WithMany(c => c.CollaboratorEducations)
        .HasForeignKey(cl => cl.CollaboratorId);

        builder.Property(c => c.InstitutionName)
            .HasMaxLength(50);


        //Default Profession Id
        builder.HasOne(l => l.DefaultProfession)
        .WithMany(ln => ln.CollaboratorEducations)
        .HasForeignKey(l => l.DefaultProfessionId);

        //OtherProfessionName
        builder.Property(c => c.OtherProfessionName)
            .HasMaxLength(150);

        //Education Level
        builder.HasOne(l => l.EducationalLevel)
        .WithMany(ln => ln.CollaboratorEducations)
        .HasForeignKey(l => l.EducationalLevelId);


        //Default Study Type
        builder.HasOne(l => l.DefaultStudyType)
        .WithMany(ln => ln.CollaboratorEducations)
        .HasForeignKey(l => l.DefaultStudyTypeId);

        //Default Study Type
        builder.HasOne(l => l.DefaultStudyArea)
        .WithMany(ln => ln.CollaboratorEducations)
        .HasForeignKey(l => l.DefaultStudyAreaId);


        //Default Education Stage
        builder.HasOne(l => l.DefaultEducationStage)
        .WithMany(ln => ln.CollaboratorEducations)
        .HasForeignKey(l => l.DefaultEducationStageId);

        builder.Property(c => c.IsCompletedStudy);

        builder.Property(r => r.StartEducationDate)
        .IsRequired(false)
        .HasConversion(
            startEducationDate => startEducationDate != null ? startEducationDate.Value : (DateTime?)null,
            value => value.HasValue ? TimeDate.Create(value.Value.ToString("MM/dd/yyyy HH:mm:ss")) : null
        );


        builder.Property(r => r.EndEducationDate)
        .IsRequired(false)
        .HasConversion(
            endEducationDate => endEducationDate != null ? endEducationDate.Value : (DateTime?)null,
            value => value.HasValue ? TimeDate.Create(value.Value.ToString("MM/dd/yyyy HH:mm:ss")) : null
        );


        builder.Property(c => c.EducationFileName).HasMaxLength(100);

        builder.Property(c => c.EducationFileURL).HasMaxLength(400);


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
