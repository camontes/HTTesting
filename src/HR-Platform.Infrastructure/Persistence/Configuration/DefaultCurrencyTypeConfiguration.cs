using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultCurrencyTypes;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultCurrencyTypeConfiguration : IEntityTypeConfiguration<DefaultCurrencyType>
{
    public void Configure(EntityTypeBuilder<DefaultCurrencyType> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultCurrencyTypeId => DefaultCurrencyTypeId.Value,
            value => new DefaultCurrencyTypeId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
