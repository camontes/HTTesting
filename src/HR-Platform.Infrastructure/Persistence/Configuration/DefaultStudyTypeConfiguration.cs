using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultStudyTypes;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultStudyTypeConfiguration : IEntityTypeConfiguration<DefaultStudyType>
{
    public void Configure(EntityTypeBuilder<DefaultStudyType> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultStudyTypeId => DefaultStudyTypeId.Value,
            value => new DefaultStudyTypeId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
