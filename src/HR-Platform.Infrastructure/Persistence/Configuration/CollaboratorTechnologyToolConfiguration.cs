using HR_Platform.Domain.CollaboratorTechnologyTools;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class CollaboratorTechnologyToolConfiguration : IEntityTypeConfiguration<CollaboratorTechnologyTool>
{
    public void Configure(EntityTypeBuilder<CollaboratorTechnologyTool> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultCollaboratorTechnologyToolId => DefaultCollaboratorTechnologyToolId.Value,
            value => new CollaboratorTechnologyToolId(value)
        );

        builder.HasOne(cl => cl.Collaborator)
        .WithMany(c => c.CollaboratorTechnologyTools)
        .HasForeignKey(cl => cl.CollaboratorId);


        //Technology Name
        builder.HasOne(l => l.DefaultTechnologyName)
        .WithMany(ln => ln.CollaboratorTechnologyTools)
        .HasForeignKey(l => l.DefaultTechnologyNameId);

        //Knowledge Level Name
        builder.HasOne(l => l.DefaultKnowledgeLevel)
        .WithMany(ll => ll.CollaboratorTechnologyTools)
        .HasForeignKey(l => l.DefaultKnowledgeLevelId);

        builder.Property(c => c.OtherTechnologyName).HasMaxLength(50);

        builder.Property(c => c.OtherTechnologyNameEnglish).HasMaxLength(50);

        builder.Property(c => c.OtherKnowledgeLevelName).HasMaxLength(50);

        builder.Property(c => c.OtherKnowledgeLevelNameEnglish).HasMaxLength(50);

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
