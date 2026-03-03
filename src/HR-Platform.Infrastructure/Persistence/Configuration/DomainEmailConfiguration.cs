using HR_Platform.Domain.DomainEmails;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DomainEmailConfiguration : IEntityTypeConfiguration<DomainEmail>
{
    public void Configure(EntityTypeBuilder<DomainEmail> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            collaboratorId => collaboratorId.Value,
            value => new DomainEmailId(value)
        );

        builder.HasOne(c => c.Company).WithMany(c => c.DomainEmails).HasForeignKey(c => c.CompanyId);

        builder.Property(c => c.Domain).HasConversion(
           domain => domain.Value,
           value => MailDomain.Create(value)!)
           .HasMaxLength(100);

        builder.Property(c => c.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(c => c.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(c => c.IsMainDomainEmail);
    }
}
