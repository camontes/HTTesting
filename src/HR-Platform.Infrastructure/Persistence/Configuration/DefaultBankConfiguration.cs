using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultBanks;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultBankConfiguration : IEntityTypeConfiguration<DefaultBank>
{
    public void Configure(EntityTypeBuilder<DefaultBank> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultBankId => DefaultBankId.Value,
            value => new DefaultBankId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
