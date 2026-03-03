using HR_Platform.Domain.CollaboratorTalentPools;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class CollaboratorTalentPoolConfiguration : IEntityTypeConfiguration<CollaboratorTalentPool>
{
    public void Configure(EntityTypeBuilder<CollaboratorTalentPool> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            CollaboratorTalentPoolId => CollaboratorTalentPoolId.Value,
            value => new CollaboratorTalentPoolId(value)
        );

        builder.HasOne(ct => ct.Collaborator).WithMany(c => c.CollaboratorTalentPools).HasForeignKey(ct => ct.CollaboratorId);

        builder.HasOne(ct => ct.TalentPool).WithMany(t => t.CollaboratorTalentPools).HasForeignKey(ct => ct.TalentPoolId);

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
