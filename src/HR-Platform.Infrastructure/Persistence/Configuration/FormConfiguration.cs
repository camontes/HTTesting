using HR_Platform.Domain.Forms;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class FormConfiguration : IEntityTypeConfiguration<Form>
{
    public void Configure(EntityTypeBuilder<Form> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultFormId => DefaultFormId.Value,
            value => new FormId(value)
        );

        builder.HasOne(p => p.Company).WithMany(c => c.Forms).HasForeignKey(c => c.CompanyId);
        builder.HasOne(p => p.NoveltyType).WithMany(c => c.Forms).HasForeignKey(c => c.NoveltyTypeId);

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Description).HasMaxLength(200).IsRequired();

        builder.Property(r => r.IsVisible);

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

        builder.HasMany(f => f.FormQuestionsTypes).WithOne(fqt => fqt.Form).HasForeignKey(fqt => fqt.FormId);
        builder.HasMany(f => f.FormAnswerGroups).WithOne(fag => fag.Form).HasForeignKey(fag => fag.FormId);
    }
}
