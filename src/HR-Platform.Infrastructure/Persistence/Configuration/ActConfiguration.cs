using HR_Platform.Domain.Acts;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class ActConfiguration : IEntityTypeConfiguration<Act>
{
    public void Configure(EntityTypeBuilder<Act> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            companyId => companyId.Value,
            value => new ActId(value)
        );

        builder.HasOne(a => a.Collaborator).WithMany(c => c.Acts).HasForeignKey(a => a.CollaboratorId);
        builder.HasOne(a => a.Company).WithMany(c => c.Acts).HasForeignKey(a => a.CompanyId);

        builder.Property(c => c.File);
        builder.Property(c => c.FileName).HasMaxLength(100);

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
