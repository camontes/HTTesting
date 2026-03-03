using HR_Platform.Domain.BrigadeInventories;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class BrigadeInventoryConfiguration : IEntityTypeConfiguration<BrigadeInventory>
{
    public void Configure(EntityTypeBuilder<BrigadeInventory> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            BrigadeInventoryId => BrigadeInventoryId.Value,
            value => new BrigadeInventoryId(value)
        );

        builder.HasOne(p => p.Company).WithMany(c => c.BrigadeInventories).HasForeignKey(c => c.CompanyId);
        builder.HasOne(p => p.UnitMeasure).WithMany(c => c.BrigadeInventories).HasForeignKey(c => c.UnitMeasureId);

        builder.Property(c => c.Name).HasMaxLength(100).IsRequired();

        builder.Property(c => c.Description).HasMaxLength(500).IsRequired();
        builder.Property(c => c.CompanyLocation).HasMaxLength(200).IsRequired();

        builder.Property(c => c.Amount).IsRequired();
        builder.Property(c => c.AvailableAmount).IsRequired();
        builder.Property(c => c.Other).HasMaxLength(50);

        builder.Property(r => r.PurchaseDate).HasConversion(
         purchaseDate => purchaseDate.Value,
         value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(r => r.ExpirationDate).HasConversion(
         expirationDate => expirationDate.Value,
         value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(c => c.EmailWhoChangedByTH).HasMaxLength(50);
        builder.Property(c => c.NameWhoChangedByTH).HasMaxLength(50);

        builder.Property(r => r.IsEditable);

        builder.Property(r => r.IsDeleteable);

        builder.Property(r => r.IsDeleted);

        builder.Property(r => r.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(c => c.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.HasMany(x => x.BrigadeInventoryFiles).WithOne(z => z.BrigadeInventory).HasForeignKey(y => y.BrigadeInventoryId);
        builder.HasMany(x => x.CollaboratorBrigadeInventories).WithOne(z => z.BrigadeInventory).HasForeignKey(y => y.BrigadeInventoryId);
    }
}
