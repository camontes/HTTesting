using HR_Platform.Domain.CollaboratorInductions;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class CollaboratorInductionConfiguration : IEntityTypeConfiguration<CollaboratorInduction>
{
    public void Configure(EntityTypeBuilder<CollaboratorInduction> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultCollaboratorInductionId => DefaultCollaboratorInductionId.Value,
            value => new CollaboratorInductionId(value)
        );

        //Collaborator
        builder.HasOne(cl => cl.Collaborator)
        .WithMany(c => c.CollaboratorInductions)
        .HasForeignKey(cl => cl.CollaboratorId);

        //Induction
        builder.HasOne(l => l.Induction)
        .WithMany(ln => ln.CollaboratorInductions)
        .HasForeignKey(l => l.InductionId);

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
