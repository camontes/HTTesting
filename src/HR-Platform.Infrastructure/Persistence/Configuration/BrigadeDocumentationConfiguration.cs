using HR_Platform.Domain.BrigadeDocumentations;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class BrigadeDocumentationConfiguration : IEntityTypeConfiguration<BrigadeDocumentation>
{
    public void Configure(EntityTypeBuilder<BrigadeDocumentation> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultBrigadeDocumentationId => DefaultBrigadeDocumentationId.Value,
            value => new BrigadeDocumentationId(value)
        );

        builder.HasOne(p => p.Company).WithMany(c => c.BrigadeDocumentations).HasForeignKey(c => c.CompanyId);

        builder.Property(c => c.Name).HasMaxLength(50);
        builder.Property(c => c.NameEnglish).HasMaxLength(50);

        builder.Property(c => c.FileName).IsRequired();
        builder.Property(c => c.UrlFile).IsRequired();

        builder.Property(c => c.EmailWhoChangedByTH).HasMaxLength(50);
        builder.Property(c => c.NameWhoChangedByTH).HasMaxLength(50);
        builder.Property(c => c.UrlPhotoWhoChangedByTH);

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
