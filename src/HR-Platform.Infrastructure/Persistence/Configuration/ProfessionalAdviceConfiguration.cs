using HR_Platform.Domain.ProfessionalAdvices;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class ProfessionalAdviceConfiguration : IEntityTypeConfiguration<ProfessionalAdvice>
{
    public void Configure(EntityTypeBuilder<ProfessionalAdvice> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultProfessionalAdviceId => DefaultProfessionalAdviceId.Value,
            value => new ProfessionalAdviceId(value)
        );

        builder.HasOne(p => p.Company).WithMany(c => c.ProfessionalAdvices).HasForeignKey(c => c.CompanyId);

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.NameEnglish)
            .HasMaxLength(100);
        
        builder.Property(c => c.NameAcronyms)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(c => c.NameAcronymsEnglish)
            .HasMaxLength(10);

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
               .WithOne(c => c.ProfessionalAdvice)
               .HasForeignKey(c => c.ProfessionalAdviceId);
    }
}
