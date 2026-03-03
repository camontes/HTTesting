using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultProfessions;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultProfessionConfiguration : IEntityTypeConfiguration<DefaultProfession>
{
    public void Configure(EntityTypeBuilder<DefaultProfession> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultProfessionId => DefaultProfessionId.Value,
            value => new DefaultProfessionId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(100);
    }
}
