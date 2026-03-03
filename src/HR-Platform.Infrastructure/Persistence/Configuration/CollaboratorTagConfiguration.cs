using HR_Platform.Domain.CollaboratorTags;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class CollaboratorTagConfiguration : IEntityTypeConfiguration<CollaboratorTag>
{
    public void Configure(EntityTypeBuilder<CollaboratorTag> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            CollaboratorTagId => CollaboratorTagId.Value,
            value => new CollaboratorTagId(value)
        );

        builder.HasOne(ct => ct.Collaborator).WithMany(c => c.CollaboratorTags).HasForeignKey(ct => ct.CollaboratorId);

        builder.HasOne(ct => ct.Tag).WithMany(t => t.CollaboratorTags).HasForeignKey(ct => ct.TagId);

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
