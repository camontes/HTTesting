using HR_Platform.Domain.Contracts;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class CollaboratorContractConfiguration : IEntityTypeConfiguration<CollaboratorContract>
{
    public void Configure(EntityTypeBuilder<CollaboratorContract> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(x => x.Id);

        builder.Property(c => c.Id).HasConversion(
            DefaultContractId => DefaultContractId.Value,
            value => new CollaboratorContractId(value)
        );

        builder.HasOne(p => p.Company).WithMany(c => c.CollaboratorContracts).HasForeignKey(c => c.CompanyId);

        builder.HasOne(c => c.DefaultCurrencyTypes).WithMany(c => c.CollaboratorContracts).HasForeignKey(f => f.DefaultCurrencyTypeId);

        builder.HasOne(c => c.ContractTypes).WithMany(c => c.CollaboratorContracts).HasForeignKey(f => f.ContractTypeId);

        builder.Property(c => c.Arl).HasMaxLength(50);

        builder.Property(c => c.Salary).HasMaxLength(20);

        builder.Property(c => c.Bonus).HasMaxLength(200);

        builder.Property(c => c.EmailWhoChangedByTH).HasMaxLength(100);

        builder.Property(c => c.NameWhoChangedByTH).HasMaxLength(100);

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

        builder.HasMany(p => p.Collaborators)
               .WithOne(c => c.CollaboratorContract)
               .HasForeignKey(c => c.CollaboratorContractId);
    }
}
