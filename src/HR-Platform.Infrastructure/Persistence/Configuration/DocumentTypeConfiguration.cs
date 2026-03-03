using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HR_Platform.Domain.DocumentTypes;

namespace HR_Platform.Infrastructure.Persistence.Configuration;

public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
{
    public void Configure(EntityTypeBuilder<DocumentType> builder)
    {
        string[] formats = ["MM/dd/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss"];

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            defaultRoleId => defaultRoleId.Value,
            value => new DocumentTypeId(value)
        );

        builder.Property(c => c.Name).HasMaxLength(100);

        builder.Property(c => c.NameEnglish).HasMaxLength(100);

        builder.HasMany(c => c.Collaborators).WithOne(d => d.DocumentType).HasForeignKey(c => c.DocumentTypeId);
    }
}
