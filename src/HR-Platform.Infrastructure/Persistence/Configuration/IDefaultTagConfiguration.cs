using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultTags;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultTagConfiguration : IEntityTypeConfiguration<DefaultTag>
{
    public void Configure(EntityTypeBuilder<DefaultTag> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultTagId => DefaultTagId.Value,
            value => new DefaultTagId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
