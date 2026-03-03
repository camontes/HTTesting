using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultStudyAreas;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultStudyAreaConfiguration : IEntityTypeConfiguration<DefaultStudyArea>
{
    public void Configure(EntityTypeBuilder<DefaultStudyArea> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultStudyAreaId => DefaultStudyAreaId.Value,
            value => new DefaultStudyAreaId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
