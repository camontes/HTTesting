using HR_Platform.Domain.ActiveBreaks;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class ActiveBreakConfiguration : IEntityTypeConfiguration<ActiveBreak>
{
    public void Configure(EntityTypeBuilder<ActiveBreak> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(ab => ab.Id);
        builder.Property(ab => ab.Id).HasConversion(
            ActiveBreakId => ActiveBreakId.Value,
            value => new ActiveBreakId(value)
        );

        builder.HasOne(ab => ab.Company).WithMany(c => c.ActiveBreaks).HasForeignKey(ab => ab.CompanyId);

        builder.Property(ab => ab.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(ab => ab.Description)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(ab => ab.Image);
        builder.Property(ab => ab.ImageName);

        builder.Property(ab => ab.File);
        builder.Property(ab => ab.FileName);

        builder.Property(ab => ab.EmailWhoChangedByHR);
        builder.Property(ab => ab.NameWhoChangedByHR);

        builder.Property(ab => ab.IsVisible);
        builder.Property(ab => ab.IsPinned);

        builder.Property(ab => ab.IsEditable);
        builder.Property(ab => ab.IsDeleteable);

        builder.Property(ab => ab.CreationDateImage).HasConversion(
           creationDateImage => creationDateImage.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(ab => ab.CreationDateFile).HasConversion(
           creationDateFile => creationDateFile.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(ab => ab.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(ab => ab.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );
    }
}
