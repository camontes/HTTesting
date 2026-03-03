using HR_Platform.Domain.BrigadeAdjustments;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class BrigadeAdjustmentConfiguration : IEntityTypeConfiguration<BrigadeAdjustment>
{
    public void Configure(EntityTypeBuilder<BrigadeAdjustment> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultBrigadeAdjustmentId => DefaultBrigadeAdjustmentId.Value,
            value => new BrigadeAdjustmentId(value)
        );

        builder.HasOne(p => p.Company).WithMany(c => c.BrigadeAdjustments).HasForeignKey(c => c.CompanyId);

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);

        builder.Property(c => c.IconId).IsRequired();

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

        builder.HasMany(c => c.BrigadeMembers).WithOne(ct => ct.BrigadeAdjustment).HasForeignKey(ct => ct.BrigadeAdjustmentId);
        builder.HasMany(c => c.CollaboratorBrigades).WithOne(ct => ct.BrigadeAdjustment).HasForeignKey(ct => ct.BrigadeAdjustmentId);

    }
}
