using HR_Platform.Domain.Pensions;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class PensionConfiguration : IEntityTypeConfiguration<Pension>
{
    public void Configure(EntityTypeBuilder<Pension> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultPensionId => DefaultPensionId.Value,
            value => new PensionId(value)
        );

        builder.HasOne(p => p.Company).WithMany(c => c.Pensions).HasForeignKey(c => c.CompanyId);

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);

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

        builder.HasMany(p => p.Collaborators)
               .WithOne(c => c.Pension)
               .HasForeignKey(c => c.PensionId);
    }
}
