using HR_Platform.Domain.Benefits;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class BenefitConfiguration : IEntityTypeConfiguration<Benefit>
{
    public void Configure(EntityTypeBuilder<Benefit> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultBenefitId => DefaultBenefitId.Value,
            value => new BenefitId(value)
        );

        builder.HasOne(p => p.Company).WithMany(c => c.Benefits).HasForeignKey(c => c.CompanyId);

        builder.Property(c => c.Name).HasMaxLength(200).IsRequired();
        builder.Property(c => c.Description).HasMaxLength(2000).IsRequired();

        builder.Property(r => r.IsAvailableForAll);
        builder.Property(r => r.MinimumMonthsConstraint);
        builder.Property(c => c.AnotherContraint).HasMaxLength(50);

        builder.Property(c => c.IsAnotherContraint);

        builder.Property(r => r.FileName);
        builder.Property(r => r.FileURL);
        builder.Property(r => r.CreationDateFile).HasConversion(
           CreationDateFile => CreationDateFile.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(r => r.ImageName);
        builder.Property(r => r.ImageURL);

        builder.Property(r => r.IsAddedButton);
        builder.Property(r => r.ButtonName);

        builder.Property(r => r.IsSurveyInclude);

        builder.Property(c => c.EmailWhoChangedByTH).HasMaxLength(50);
        builder.Property(c => c.NameWhoChangedByTH).HasMaxLength(50);

        builder.Property(r => r.IsVisible);
        builder.Property(r => r.IsPinned);

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

        builder.HasMany(c => c.CollaboratorBenefitClaims).WithOne(ct => ct.Benefit).HasForeignKey(ct => ct.BenefitId);

    }
}
