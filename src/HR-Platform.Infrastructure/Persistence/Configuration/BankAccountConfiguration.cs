using HR_Platform.Domain.BankAccounts;
using HR_Platform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultBankAccountId => DefaultBankAccountId.Value,
            value => new BankAccountId(value)
        );

        builder.HasOne(p => p.Bank)
               .WithMany(c => c.BankAccounts)
               .HasForeignKey(c => c.BankId);

        builder.HasOne(p => p.TypeAccount)
               .WithMany(c => c.BankAccounts)
               .HasForeignKey(c => c.TypeAccountId);

        builder.Property(c => c.AccountNumber)
            .IsRequired();

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
               .WithOne(c => c.BankAccount)
               .HasForeignKey(c => c.BankAccountId);
    }
}
