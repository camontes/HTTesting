using HR_Platform.Domain.ChildrenNamespace;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class ChildrenConfiguration : IEntityTypeConfiguration<Children>
{
    public void Configure(EntityTypeBuilder<Children> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            childrenId => childrenId.Value,
            value => new ChildrenId(value)
        );

        builder.HasOne(c => c.Collaborator).WithMany(c => c.Children).HasForeignKey(c => c.CollaboratorId);

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Age);

        builder.Property(c => c.IsEditable);

        builder.Property(c => c.IsDeleteable);

        builder.Property(c => c.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(c => c.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );
    }
}
