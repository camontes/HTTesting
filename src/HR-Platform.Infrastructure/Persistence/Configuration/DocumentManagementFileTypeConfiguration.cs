using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DocumentManagementFileTypes;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DocumentManagementFileTypeConfiguration : IEntityTypeConfiguration<DocumentManagementFileType>
{
    public void Configure(EntityTypeBuilder<DocumentManagementFileType> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            DocumentManagementFileTypeId => DocumentManagementFileTypeId.Value,
            value => new DocumentManagementFileTypeId(value)
        );

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.NameEnglish).HasMaxLength(50);

        builder.HasMany(c => c.DocumentManagements).WithOne(c => c.DocumentManagementFileType).HasForeignKey(c => c.DocumentManagementFileTypeId);

    }
}
