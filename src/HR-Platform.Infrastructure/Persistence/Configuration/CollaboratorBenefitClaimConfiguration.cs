using HR_Platform.Domain.CollaboratorBenefitClaims;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class CollaboratorBenefitClaimConfiguration : IEntityTypeConfiguration<CollaboratorBenefitClaim>
{
    public void Configure(EntityTypeBuilder<CollaboratorBenefitClaim> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(x => x.Id);

        builder.Property(c => c.Id).HasConversion(
            DefaultContractId => DefaultContractId.Value,
            value => new CollaboratorBenefitClaimId(value)
        );

        builder.HasOne(p => p.Company).WithMany(c => c.CollaboratorBenefitClaims).HasForeignKey(c => c.CompanyId);

        builder.HasOne(c => c.Benefit).WithMany(c => c.CollaboratorBenefitClaims).HasForeignKey(f => f.BenefitId);

        builder.HasOne(c => c.Collaborator).WithMany(c => c.CollaboratorBenefitClaims).HasForeignKey(f => f.CollaboratorId);

        builder.Property(r => r.IsAccepted);

        builder.Property(r => r.IsAnySelected);


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
