using HR_Platform.Domain.Assignations;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class AssignationConfiguration : IEntityTypeConfiguration<Assignation>
{
    public void Configure(EntityTypeBuilder<Assignation> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasConversion(
            assignationId => assignationId.Value,
            value => new AssignationId(value)
        );

        builder.HasOne(a => a.Company).WithMany(c => c.Assignations).HasForeignKey(a => a.CompanyId);

        builder.Property(a => a.Name).HasMaxLength(100);

        builder.Property(a => a.NameEnglish).HasMaxLength(100);

        builder.Property(a => a.IsEditable);

        builder.Property(a => a.IsDeleteable);

        builder.Property(a => a.IsInternalAssignation);

        builder.Property(a => a.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(a => a.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.HasMany(c => c.Collaborators).WithOne(a => a.Assignation).HasForeignKey(c => c.AssignationId);
    }
}
