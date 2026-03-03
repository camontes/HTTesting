using HR_Platform.Domain.Companies;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            companyId => companyId.Value,
            value => new CompanyId(value)
        );

        builder.Property(c => c.Email).HasConversion(
           email => email.Value,
           value => Email.Create(value)!)
           .HasMaxLength(100);
        builder.HasIndex(c => c.Email).IsUnique();

        builder.Property(c => c.RequestsEmail).HasConversion(
           email => email.Value,
           value => Email.Create(value)!)
           .HasMaxLength(100);

        builder.Property(c => c.Name).HasMaxLength(100);
        builder.Property(c => c.MenuName).HasMaxLength(100);

        builder.OwnsOne(c => c.Address, addressBuilder =>
        {

            addressBuilder.Property(a => a.StreetAddress).HasMaxLength(100);

            addressBuilder.Property(a => a.CountryCode);
            addressBuilder.Property(a => a.Country).HasMaxLength(100);

            addressBuilder.Property(a => a.StateCode);
            addressBuilder.Property(a => a.State).HasMaxLength(100).IsRequired(false);

            addressBuilder.Property(a => a.CityCode);
            addressBuilder.Property(a => a.City).HasMaxLength(100).IsRequired(false);

            addressBuilder.Property(a => a.ZipCode).HasMaxLength(100).IsRequired(false);
        });

        builder.Property(c => c.PhoneNumber).HasConversion(
           phoneNumber => phoneNumber.Value,
           value => PhoneNumber.Create(value)!)
           .HasMaxLength(50);

        builder.Property(c => c.Logo);
        builder.Property(c => c.LogoName).HasMaxLength(100);

        builder.Property(c => c.URL).HasMaxLength(250);

        builder.Property(c => c.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(c => c.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.HasMany(c => c.ActiveBreaks).WithOne(ab => ab.Company).HasForeignKey(ab => ab.CompanyId);
        builder.HasMany(c => c.Acts).WithOne(a => a.Company).HasForeignKey(a => a.CompanyId);
        builder.HasMany(c => c.Areas).WithOne(a => a.Company).HasForeignKey(a => a.CompanyId);
        builder.HasMany(c => c.Banks).WithOne(a => a.Company).HasForeignKey(a => a.CompanyId);
        builder.HasMany(c => c.Benefits).WithOne(a => a.Company).HasForeignKey(a => a.CompanyId);
        builder.HasMany(c => c.BirthdayTemplateSettings).WithOne(a => a.Company).HasForeignKey(a => a.CompanyId);
        builder.HasMany(c => c.BrigadeAdjustments).WithOne(a => a.Company).HasForeignKey(a => a.CompanyId);
        builder.HasMany(c => c.BrigadeInventories).WithOne(a => a.Company).HasForeignKey(a => a.CompanyId);
        builder.HasMany(c => c.CoexistenceCommitteeMinutes).WithOne(c => c.Company).HasForeignKey(c => c.CompanyId);
        builder.HasMany(c => c.Collaborators).WithOne(c => c.Company).HasForeignKey(c => c.CompanyId);
        builder.HasMany(c => c.CollaboratorBenefitClaims).WithOne(c => c.Company).HasForeignKey(c => c.CompanyId);
        builder.HasMany(c => c.CollaboratorBrigadeInventories).WithOne(c => c.Company).HasForeignKey(c => c.CompanyId);
        builder.HasMany(c => c.CollaboratorContracts).WithOne(c => c.Company).HasForeignKey(c => c.CompanyId);
        builder.HasMany(c => c.DomainEmails).WithOne(d => d.Company).HasForeignKey(d => d.CompanyId);
        builder.HasMany(c => c.EducationalLevels).WithOne(d => d.Company).HasForeignKey(d => d.CompanyId);
        builder.HasMany(c => c.EmergencyPlanTypes).WithOne(d => d.Company).HasForeignKey(d => d.CompanyId);
        builder.HasMany(c => c.EvidenceCoexistenceCommitteeVotes).WithOne(d => d.Company).HasForeignKey(d => d.CompanyId);
        builder.HasMany(c => c.Events).WithOne(d => d.Company).HasForeignKey(d => d.CompanyId);
        builder.HasMany(c => c.EventTypes).WithOne(d => d.Company).HasForeignKey(d => d.CompanyId);
        builder.HasMany(c => c.HealthEntities).WithOne(h => h.Company).HasForeignKey(h => h.CompanyId);
        builder.HasMany(c => c.Inductions).WithOne(h => h.Company).HasForeignKey(h => h.CompanyId);
        builder.HasMany(c => c.NewCommunications).WithOne(h => h.Company).HasForeignKey(h => h.CompanyId);
        builder.HasMany(c => c.Pensions).WithOne(p => p.Company).HasForeignKey(p => p.CompanyId);
        builder.HasMany(c => c.ProfessionalAdvices).WithOne(p => p.Company).HasForeignKey(p => p.CompanyId);
        builder.HasMany(c => c.Positions).WithOne(p => p.Company).HasForeignKey(p => p.CompanyId);
        builder.HasMany(c => c.QuestionTypes).WithOne(p => p.Company).HasForeignKey(p => p.CompanyId);
        builder.HasMany(c => c.Roles).WithOne(r => r.Company).HasForeignKey(r => r.CompanyId);
        builder.HasMany(c => c.RiskTypeMains).WithOne(r => r.Company).HasForeignKey(r => r.CompanyId);
        builder.HasMany(c => c.SeveranceBenefits).WithOne(r => r.Company).HasForeignKey(r => r.CompanyId);
        builder.HasMany(c => c.Surveys).WithOne(s => s.Company).HasForeignKey(s => s.CompanyId);
        builder.HasMany(c => c.Tags).WithOne(r => r.Company).HasForeignKey(r => r.CompanyId);
        builder.HasMany(c => c.TalentPools).WithOne(r => r.Company).HasForeignKey(r => r.CompanyId);
        builder.HasMany(c => c.TypeAccounts).WithOne(r => r.Company).HasForeignKey(r => r.CompanyId);
    }
}
