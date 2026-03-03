using HR_Platform.Domain.FamilyCompositions;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class FamilyCompositionConfiguration : IEntityTypeConfiguration<FamilyComposition>
{
    public void Configure(EntityTypeBuilder<FamilyComposition> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            familyCompositionId => familyCompositionId.Value,
            value => new FamilyCompositionId(value)
        );

        builder.HasOne(f => f.Collaborator).WithMany(f => f.FamilyCompositions).HasForeignKey(f => f.CollaboratorId);

        builder.Property(f => f.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(f => f.NameEnglish).HasMaxLength(50);

        builder.Property(f => f.IsEditable);

        builder.Property(f => f.IsDeleteable);

        builder.Property(f=> f.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(f => f.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );
    }
}
