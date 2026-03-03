using HR_Platform.Domain.Inductions;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class InductionConfiguration : IEntityTypeConfiguration<Induction>
{
    public void Configure(EntityTypeBuilder<Induction> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultInductionId => DefaultInductionId.Value,
            value => new InductionId(value)
        );

        builder.HasOne(p => p.Company).WithMany(c => c.Inductions).HasForeignKey(c => c.CompanyId);

        builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Description).HasMaxLength(5000).IsRequired();

        builder.Property(c => c.EmailWhoChangedByTH).HasMaxLength(50);
        builder.Property(c => c.NameWhoChangedByTH).HasMaxLength(50);

        builder.Property(c => c.IsVisible);
        builder.Property(c => c.AllowForAllCollaborators);


        builder.Property(r => r.EmailWhoDeletedByTH).HasMaxLength(50);
        builder.Property(r => r.IsInductionDeleted);
        builder.Property(r => r.DeleteDate).HasConversion(
           deleteDate => deleteDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );


        builder.Property(r => r.IsEditable);

        builder.Property(r => r.IsDeleteable);

        builder.Property(r => r.IsVisibleChangeDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(r => r.CreationDate).HasConversion(
           creationDate => creationDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        builder.Property(c => c.EditionDate).HasConversion(
           editionDate => editionDate.Value,
           value => TimeDate.Create(value.ToString("MM/dd/yyyy HH:mm:ss"))!
        );

        //Relations
        builder.HasMany(x => x.CollaboratorInductions).WithOne(z => z.Induction).HasForeignKey(y => y.InductionId);
        builder.HasMany(x => x.InductionFiles).WithOne(z => z.Induction).HasForeignKey(y => y.InductionId);

    }
}
