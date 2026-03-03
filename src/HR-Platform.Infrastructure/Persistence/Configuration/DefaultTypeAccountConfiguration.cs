using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultTypeAccounts;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultTypeAccountConfiguration : IEntityTypeConfiguration<DefaultTypeAccount>
{
    public void Configure(EntityTypeBuilder<DefaultTypeAccount> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultTypeAccountId => DefaultTypeAccountId.Value,
            value => new DefaultTypeAccountId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
