using HR_Platform.Domain.BenefitClaimAnswers;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class BenefitClaimAnswerConfiguration : IEntityTypeConfiguration<BenefitClaimAnswer>
{
    public void Configure(EntityTypeBuilder<BenefitClaimAnswer> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasConversion(
            roleId => roleId.Value,
            value => new BenefitClaimAnswerId(value)
        );

        builder.HasOne(r => r.Company).WithMany(c => c.BenefitClaimAnswers).HasForeignKey(r => r.CompanyId);
        builder.HasOne(r => r.Collaborator).WithMany(c => c.BenefitClaimAnswers).HasForeignKey(r => r.CollaboratorId);

        builder.Property(r => r.BenefitName).HasMaxLength(200).IsRequired();
        builder.Property(r => r.Details).HasMaxLength(500);
        builder.Property(r => r.ReferenceNumber).HasMaxLength(14).IsRequired();
        builder.Property(r => r.IsBenefitAccepeted);

        //New Fields
        builder.Property(r => r.IsAvailableForAll);
        builder.Property(r => r.MinimumMonthsConstraint);
        builder.Property(c => c.AnotherContraint).HasMaxLength(50);
        builder.Property(c => c.IsAnotherContraint);
        builder.Property(c => c.NameWhoManagedClaim).HasMaxLength(50);
        builder.Property(c => c.EmailWhoManagedClaim).HasMaxLength(50);
        
        //New Fields 2 
        builder.Property(r => r.HasDeleted);
        builder.Property(c => c.NameWhoDeletedBenefitClaim).HasMaxLength(50);
        builder.Property(c => c.EmailWhoDeletedBenefitClaim).HasMaxLength(50);
        builder.Property(r => r.DeletedDate).HasConversion(
           deletedDate => deletedDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );


        builder.Property(r => r.ManagementDate).HasConversion(
          managemmentDate => managemmentDate.Value,
          value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
       );

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
