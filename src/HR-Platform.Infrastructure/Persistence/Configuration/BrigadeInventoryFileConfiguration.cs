using HR_Platform.Domain.BrigadeInventoryFiles;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class BrigadeInventoryFileConfiguration : IEntityTypeConfiguration<BrigadeInventoryFile>
{
    public void Configure(EntityTypeBuilder<BrigadeInventoryFile> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            brigadeInventoryFileId => brigadeInventoryFileId.Value,
            value => new BrigadeInventoryFileId(value)
        );

        builder.HasOne(p => p.BrigadeInventory).WithMany(c => c.BrigadeInventoryFiles).HasForeignKey(c => c.BrigadeInventoryId);

        builder.Property(c => c.FileName).HasMaxLength(100).IsRequired();
        builder.Property(c => c.UrlFile).IsRequired();

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
