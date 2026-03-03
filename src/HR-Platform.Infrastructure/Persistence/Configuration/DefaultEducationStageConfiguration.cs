using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultEducationStages;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultEducationStageConfiguration : IEntityTypeConfiguration<DefaultEducationStage>
{
    public void Configure(EntityTypeBuilder<DefaultEducationStage> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultEducationStageId => DefaultEducationStageId.Value,
            value => new DefaultEducationStageId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);
    }
}
