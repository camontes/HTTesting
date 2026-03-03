using HR_Platform.Domain.CollaboratorBrigadeInventories;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class CollaboratorBrigadeInventoryConfiguration : IEntityTypeConfiguration<CollaboratorBrigadeInventory>
{
    public void Configure(EntityTypeBuilder<CollaboratorBrigadeInventory> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            CollaboratorBrigadeInventoryId => CollaboratorBrigadeInventoryId.Value,
            value => new CollaboratorBrigadeInventoryId(value)
        );

        builder.HasOne(p => p.Company).WithMany(c => c.CollaboratorBrigadeInventories).HasForeignKey(c => c.CompanyId);
        builder.HasOne(p => p.UnitMeasure).WithMany(c => c.CollaboratorBrigadeInventories).HasForeignKey(c => c.UnitMeasureId);


        builder.Property(c => c.QuantityDelivered).IsRequired();

        builder.Property(c => c.SendForAll);

        builder.Property(r => r.DeliveryDate).HasConversion(
         purchaseDate => purchaseDate.Value,
         value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(r => r.ReturnDate).HasConversion(
         expirationDate => expirationDate.Value,
         value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(c => c.EmailWhoChangedByTH).HasMaxLength(50);
        builder.Property(c => c.NameWhoChangedByTH).HasMaxLength(50);

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

        builder.HasMany(x => x.CollaboratorBrigades).WithOne(z => z.CollaboratorBrigadeInventory).HasForeignKey(y => y.CollaboratorBrigadeInventoryId);
        builder.HasMany(x => x.CollaboratorBrigadeInventoryFiles).WithOne(z => z.CollaboratorBrigadeInventory).HasForeignKey(y => y.CollaboratorBrigadeInventoryId);
    }
}
