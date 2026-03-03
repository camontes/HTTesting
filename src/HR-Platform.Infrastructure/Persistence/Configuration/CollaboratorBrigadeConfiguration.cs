using HR_Platform.Domain.CollaboratorBrigades;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class CollaboratorBrigadeConfiguration : IEntityTypeConfiguration<CollaboratorBrigade>
{
    public void Configure(EntityTypeBuilder<CollaboratorBrigade> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            brigadeInventoryFileId => brigadeInventoryFileId.Value,
            value => new CollaboratorBrigadeId(value)
        );

        builder.HasOne(p => p.CollaboratorBrigadeInventory).WithMany(c => c.CollaboratorBrigades).HasForeignKey(c => c.CollaboratorBrigadeInventoryId);
        builder.HasOne(p => p.Collaborator).WithMany(c => c.CollaboratorBrigades).HasForeignKey(c => c.CollaboratorId);
        builder.HasOne(p => p.BrigadeAdjustment).WithMany(c => c.CollaboratorBrigades).HasForeignKey(c => c.BrigadeAdjustmentId);


        builder.Property(r => r.AmountByBrigader);

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
