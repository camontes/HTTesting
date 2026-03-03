using HR_Platform.Domain.TalentPools;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class TalentPoolConfiguration : IEntityTypeConfiguration<TalentPool>
{
    public void Configure(EntityTypeBuilder<TalentPool> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultTalentPoolId => DefaultTalentPoolId.Value,
            value => new TalentPoolId(value)
        );

        builder.HasOne(p => p.Company).WithMany(c => c.TalentPools).HasForeignKey(c => c.CompanyId);

        builder.Property(c => c.Tittle)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(500);
       

        builder.Property(r => r.IsArchived);

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

        builder.HasMany(t => t.CollaboratorTalentPools).WithOne(ct => ct.TalentPool).HasForeignKey(ct => ct.TalentPoolId);
    }
}
