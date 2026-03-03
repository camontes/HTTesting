using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultLanguageTypes;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultLanguageTypeConfiguration : IEntityTypeConfiguration<DefaultLanguageType>
{
    public void Configure(EntityTypeBuilder<DefaultLanguageType> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultLanguageTypeId => DefaultLanguageTypeId.Value,
            value => new DefaultLanguageTypeId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
