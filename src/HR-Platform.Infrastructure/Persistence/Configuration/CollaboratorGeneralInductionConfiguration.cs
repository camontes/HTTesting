using HR_Platform.Domain.CollaboratorGeneralInductions;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class CollaboratorGeneralInductionConfiguration : IEntityTypeConfiguration<CollaboratorGeneralInduction>
{
    public void Configure(EntityTypeBuilder<CollaboratorGeneralInduction> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultCollaboratorGeneralInductionId => DefaultCollaboratorGeneralInductionId.Value,
            value => new CollaboratorGeneralInductionId(value)
        );

        //Collaborator
        builder.HasOne(cl => cl.Collaborator)
        .WithMany(c => c.CollaboratorGeneralInductions)
        .HasForeignKey(cl => cl.CollaboratorId);

        //Induction
        builder.HasOne(l => l.Induction)
        .WithMany(ln => ln.CollaboratorGeneralInductions)
        .HasForeignKey(l => l.InductionId);        

        builder.Property(r => r.HasInductionBeenDeletedWhenHasCompleted);

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
