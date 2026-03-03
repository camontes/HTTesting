using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DefaultFileTypes;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DefaultFileTypeConfiguration : IEntityTypeConfiguration<DefaultFileType>
{
    public void Configure(EntityTypeBuilder<DefaultFileType> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DefaultFileTypeId => DefaultFileTypeId.Value,
            value => new DefaultFileTypeId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);

        builder.HasMany(c => c.OccupationalTests).WithOne(c => c.DefaultFileType).HasForeignKey(c => c.DefaultFileTypeId);
    }
}
